using Colso.Xrm.WebResourceAutoUpdater.AppCode;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Colso.Xrm.WebResourceAutoUpdater
{
    public partial class AutoUpdater : MultipleConnectionsPluginControlBase, IXrmToolBoxPluginControl, IGitHubPlugin, IHelpPlugin, IStatusBarMessenger, IPayPalPlugin
    {
        #region Variables

        private MonitorChanges monitor;
        private bool workingstate = false;

        #endregion Variables

        public AutoUpdater()
        {
            InitializeComponent();

            monitor = new MonitorChanges();
            monitor.OnMonitorMessage += UpdateStatusMessage;

            // Set defauts
            string settingspath;
            SettingsManager.Instance.TryLoad<string>(this.GetType(), out settingspath, "folderpath");
            txtFolderPath.AppendText(settingspath);

            string settingsfilter;
            SettingsManager.Instance.TryLoad<string>(this.GetType(), out settingsfilter, "filefilter");
            txtFilter.AppendText(settingsfilter);
        }

        #region XrmToolbox

        //public event EventHandler OnCloseTool;
        //public event EventHandler OnRequestConnection;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public Image PluginLogo
        {
            get { return null; }
        }

        //public IOrganizationService Service
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public string HelpUrl
        {
            get
            {
                return "https://github.com/MscrmTools/Colso.Xrm.WebResourceAutoUpdater/wiki";
            }
        }

        public string RepositoryName
        {
            get
            {
                return "Colso.Xrm.WebResourceAutoUpdater";
            }
        }

        public string UserName
        {
            get
            {
                return "MscrmTools";
            }
        }

        public string DonationDescription
        {
            get
            {
                return "Donation for Webresource Auto-Updater - XrmToolBox";
            }
        }

        public string EmailAccount
        {
            get
            {
                return "bramcolpaert@outlook.com";
            }
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        #endregion XrmToolbox

        #region Form events

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            // This is not working?! -> use AutoUpdater_ConnectionUpdated
        }

        protected void AutoUpdater_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            // Stop current action
            monitor.Stop();
            ManageWorkingState(false);

            SetConnectionLabel(e.ConnectionDetail.ConnectionName);
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbExecute_Click(object sender, EventArgs e)
        {
            if (monitor.IsRunning())
            {
                monitor.Stop();
                ManageWorkingState(false);
            }
            else
            {
                if (!CheckConnection())
                    return;

                if (string.IsNullOrEmpty(txtFolderPath.Text))
                {
                    MessageBox.Show("You must select a folder to monitor!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(txtFolderPath.Text))
                {
                    MessageBox.Show("The selected folder doesn't exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var worker = new BackgroundWorker { WorkerReportsProgress = true };
                worker.DoWork += (s, args) =>
                {
                    ManageWorkingState(true);
                    monitor.Start(this.Service, txtFolderPath.Text, txtFilter.Text);
                };

                worker.RunWorkerAsync();
            }
        }

        private void donateInUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDonationPage("USD");
        }

        private void donateInEURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDonationPage("EUR");
        }

        private void donateInGBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDonationPage("GBP");
        }

        protected void DataTransporter_OnCloseTool(object sender, EventArgs e)
        {
            // First save settings file
        }

        #endregion Form events

        #region Methods

        private void SetConnectionLabel(string name)
        {
            lbSourceValue.Text = name;
            lbSourceValue.ForeColor = Color.Green;
        }

        private void ManageWorkingState(bool working)
        {
            workingstate = working;
            txtFolderPath.Enabled = !working;
            txtFilter.Enabled = !working;
            if (working)
            {
                // Save settings
                SettingsManager.Instance.Save(this.GetType(), txtFolderPath.Text, "folderpath");
                SettingsManager.Instance.Save(this.GetType(), txtFilter.Text, "filefilter");
            }
            tsbExecute.Text = working ? tsbExecute.Text.Replace("Start", "Stop") : tsbExecute.Text.Replace("Stop", "Start");
        }

        private bool CheckConnection()
        {
            if (this.Service == null)
            {

                var args = new RequestConnectionEventArgs { ActionName = "Load", Control = this };
                RaiseRequestConnectionEvent(args);

                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateStatusMessage(object sender, EventArgs e)
        {
            var msg = string.Format("{0:HH:mm:ss.fff} - {1}", DateTime.Now, ((StatusMessageEventArgs)e).Message);
            txtLog.AppendText(msg);
            txtLog.AppendText(Environment.NewLine);
        }


        private void OpenDonationPage(string currency)
        {
            var url = string.Format("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc=GB&item_name={1}&currency_code={2}&no_note=0&bn=PP-DonationsBF:btn_donateCC_LG.gif:NonHostedGuest", EmailAccount, DonationDescription, currency);
            System.Diagnostics.Process.Start(url);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = txtFolderPath.Text; // set default path

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                }
            }
        }

        #endregion Methods

    }
}