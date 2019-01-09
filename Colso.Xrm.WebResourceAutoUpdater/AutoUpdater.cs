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

            // Make sure to get the connection updates:
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.AutoUpdater_ConnectionUpdated);

            monitor = new MonitorChanges();
            monitor.OnMonitorMessage += UpdateStatusMessage;

            // Set defauts
            RestoreSettings();
        }

        private void RestoreSettings()
        {
            string settingspath;
            SettingsManager.Instance.TryLoad<string>(this.GetType(), out settingspath, "folderpath");
            txtFolderPath.AppendText(settingspath);

            string settingsfilter;
            SettingsManager.Instance.TryLoad<string>(this.GetType(), out settingsfilter, "filefilter");
            txtFilter.AppendText(settingsfilter);

            decimal? uploaddelay;
            SettingsManager.Instance.TryLoad<decimal?>(this.GetType(), out uploaddelay, "uploaddelay");
            if (uploaddelay != null && uploaddelay.HasValue) txtDelay.Value = uploaddelay.Value;
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
            LoadSolutions();
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            monitor.Stop();
            CloseTool();
        }

        private void tsbExecute_Click(object sender, EventArgs e)
        {
            ToggleMonitor();
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

        #endregion Form events

        #region Methods

        private void LoadSolutions()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                if (!dlSolutions.InvokeRequired) dlSolutions.Items.Clear();
                else dlSolutions.Invoke((MethodInvoker)(() => { dlSolutions.Items.Clear(); }));

                var solutions = Service.GetUnmanagedSolutions();
                var items = solutions
                    .Select(s => new ComboboxItem() {
                        Text = s.GetAttributeValue<string>("friendlyname"),
                        Value = s.GetAttributeValue<string>("uniquename")
                    })
                    .ToArray();

                if (!dlSolutions.InvokeRequired) dlSolutions.Items.AddRange(items);
                else dlSolutions.Invoke((MethodInvoker)(() => { dlSolutions.Items.AddRange(items); }));
            };

            worker.RunWorkerAsync();
        }

        private void ToggleMonitor()
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

                var selectedsolution = dlSolutions.SelectedItem == null ? null : (string)((ComboboxItem)dlSolutions.SelectedItem).Value;
                var worker = new BackgroundWorker { WorkerReportsProgress = true };
                worker.DoWork += (s, args) =>
                {
                    ManageWorkingState(true);
                    monitor.Start(this.Service, (int)txtDelay.Value, txtFolderPath.Text, selectedsolution, txtFilter.Text.Split(',',';'));
                };

                worker.RunWorkerAsync();
            }
        }

        private void SetConnectionLabel(string name)
        {
            lbSourceValue.Text = name;
            lbSourceValue.ForeColor = Color.Green;
        }

        private void ManageWorkingState(bool working)
        {
            workingstate = working;
            txtFolderPath.Enabled = !working;
            dlSolutions.Enabled = !working;
            txtFilter.Enabled = !working;
            txtDelay.Enabled = !working;
            if (working)
            {
                // Save settings
                SettingsManager.Instance.Save(this.GetType(), txtFolderPath.Text, "folderpath");
                SettingsManager.Instance.Save(this.GetType(), txtFilter.Text, "filefilter");
                SettingsManager.Instance.Save(this.GetType(), txtDelay.Value, "uploaddelay");
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
            if (txtLog.InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { UpdateStatusMessage(sender, e); }));
            }
            else
            {
                var msg = string.Format("{0:HH:mm:ss.fff} - {1}", DateTime.Now, ((StatusMessageEventArgs)e).Message);
                txtLog.AppendText(msg);
                txtLog.AppendText(Environment.NewLine);
            }
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