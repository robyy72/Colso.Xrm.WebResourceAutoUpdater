namespace Colso.Xrm.WebResourceAutoUpdater
{
    partial class AutoUpdater
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdater));
            this.viewImageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbEnvironment = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.lbSourceValue = new System.Windows.Forms.Label();
            this.lblOrganisation = new System.Windows.Forms.Label();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDonate = new System.Windows.Forms.ToolStripDropDownButton();
            this.donateInUSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInEURToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInGBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.gbEnvironment.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewImageList
            // 
            this.viewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("viewImageList.ImageStream")));
            this.viewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.viewImageList.Images.SetKeyName(0, "dashboard.gif");
            this.viewImageList.Images.SetKeyName(1, "dashboard_user.png");
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.gbSettings);
            this.pnlHeader.Controls.Add(this.gbEnvironment);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 25);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 85);
            this.pnlHeader.TabIndex = 103;
            // 
            // gbSettings
            // 
            this.gbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettings.Controls.Add(this.comboBox1);
            this.gbSettings.Controls.Add(this.txtFilter);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Location = new System.Drawing.Point(516, 3);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(281, 72);
            this.gbSettings.TabIndex = 102;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(104, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(171, 21);
            this.comboBox1.TabIndex = 106;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(104, 45);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(171, 20);
            this.txtFilter.TabIndex = 105;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Filefilter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Add to solution";
            // 
            // gbEnvironment
            // 
            this.gbEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnvironment.Controls.Add(this.btnBrowse);
            this.gbEnvironment.Controls.Add(this.txtFolderPath);
            this.gbEnvironment.Controls.Add(this.lblFolder);
            this.gbEnvironment.Controls.Add(this.lbSourceValue);
            this.gbEnvironment.Controls.Add(this.lblOrganisation);
            this.gbEnvironment.Location = new System.Drawing.Point(3, 4);
            this.gbEnvironment.Name = "gbEnvironment";
            this.gbEnvironment.Size = new System.Drawing.Size(507, 71);
            this.gbEnvironment.TabIndex = 101;
            this.gbEnvironment.TabStop = false;
            this.gbEnvironment.Text = "Environment";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(434, 42);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(67, 23);
            this.btnBrowse.TabIndex = 103;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(117, 44);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(311, 20);
            this.txtFolderPath.TabIndex = 102;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 47);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 101;
            this.lblFolder.Text = "Folder";
            // 
            // lbSourceValue
            // 
            this.lbSourceValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSourceValue.AutoSize = true;
            this.lbSourceValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbSourceValue.ForeColor = System.Drawing.Color.Red;
            this.lbSourceValue.Location = new System.Drawing.Point(114, 24);
            this.lbSourceValue.Name = "lbSourceValue";
            this.lbSourceValue.Size = new System.Drawing.Size(64, 13);
            this.lbSourceValue.TabIndex = 97;
            this.lbSourceValue.Text = "Unselected";
            // 
            // lblOrganisation
            // 
            this.lblOrganisation.AutoSize = true;
            this.lblOrganisation.Location = new System.Drawing.Point(6, 24);
            this.lblOrganisation.Name = "lblOrganisation";
            this.lblOrganisation.Size = new System.Drawing.Size(66, 13);
            this.lblOrganisation.TabIndex = 100;
            this.lblOrganisation.Text = "Organisation";
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbExecute,
            this.toolStripSeparator1,
            this.tsbDonate});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(800, 25);
            this.tsMain.TabIndex = 90;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(55, 22);
            this.tsbCloseThisTab.Text = "Close";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExecute
            // 
            this.tsbExecute.Image = global::Colso.Xrm.WebResourceAutoUpdater.Properties.Resources.export;
            this.tsbExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExecute.Name = "tsbExecute";
            this.tsbExecute.Size = new System.Drawing.Size(137, 22);
            this.tsbExecute.Text = "Start update monitor";
            this.tsbExecute.Click += new System.EventHandler(this.tsbExecute_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDonate
            // 
            this.tsbDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateInUSDToolStripMenuItem,
            this.donateInEURToolStripMenuItem,
            this.donateInGBPToolStripMenuItem});
            this.tsbDonate.Image = global::Colso.Xrm.WebResourceAutoUpdater.Properties.Resources.paypal;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(74, 22);
            this.tsbDonate.Text = "Donate";
            // 
            // donateInUSDToolStripMenuItem
            // 
            this.donateInUSDToolStripMenuItem.Name = "donateInUSDToolStripMenuItem";
            this.donateInUSDToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.donateInUSDToolStripMenuItem.Text = "Donate in USD";
            this.donateInUSDToolStripMenuItem.Click += new System.EventHandler(this.donateInUSDToolStripMenuItem_Click);
            // 
            // donateInEURToolStripMenuItem
            // 
            this.donateInEURToolStripMenuItem.Name = "donateInEURToolStripMenuItem";
            this.donateInEURToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.donateInEURToolStripMenuItem.Text = "Donate in EUR";
            this.donateInEURToolStripMenuItem.Click += new System.EventHandler(this.donateInEURToolStripMenuItem_Click);
            // 
            // donateInGBPToolStripMenuItem
            // 
            this.donateInGBPToolStripMenuItem.Name = "donateInGBPToolStripMenuItem";
            this.donateInGBPToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.donateInGBPToolStripMenuItem.Text = "Donate in GBP";
            this.donateInGBPToolStripMenuItem.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Location = new System.Drawing.Point(3, 6);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(794, 319);
            this.gbLog.TabIndex = 102;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(9, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(779, 294);
            this.txtLog.TabIndex = 0;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.gbLog);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 110);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(800, 328);
            this.pnlBody.TabIndex = 104;
            // 
            // AutoUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.tsMain);
            this.Name = "AutoUpdater";
            this.Size = new System.Drawing.Size(800, 438);
            this.pnlHeader.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbEnvironment.ResumeLayout(false);
            this.gbEnvironment.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExecute;
        private System.Windows.Forms.ImageList viewImageList;
        private System.Windows.Forms.Label lbSourceValue;
        private System.Windows.Forms.GroupBox gbEnvironment;
        private System.Windows.Forms.Label lblOrganisation;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.ToolStripDropDownButton tsbDonate;
        private System.Windows.Forms.ToolStripMenuItem donateInUSDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateInEURToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateInGBPToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
