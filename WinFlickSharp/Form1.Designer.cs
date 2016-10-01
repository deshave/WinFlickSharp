namespace WinFlickSharp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flickrMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.authenticateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enterCodeMenuItem = new System.Windows.Forms.ToolStripTextBox();
			this.uploadSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uploadAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.regenerateThumbnailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxHiddenFromSearch = new System.Windows.Forms.ComboBox();
			this.comboBoxSafetyLevel = new System.Windows.Forms.ComboBox();
			this.comboBoxContentType = new System.Windows.Forms.ComboBox();
			this.checkBoxPublic = new System.Windows.Forms.CheckBox();
			this.checkBoxFriends = new System.Windows.Forms.CheckBox();
			this.checkBoxFamily = new System.Windows.Forms.CheckBox();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxTags = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.uploadWorker = new System.ComponentModel.BackgroundWorker();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.populateWorker = new System.ComponentModel.BackgroundWorker();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButtonFile = new System.Windows.Forms.ToolStripDropDownButton();
			this.chooseFilesToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseFolderToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButtonImages = new System.Windows.Forms.ToolStripDropDownButton();
			this.selectAllToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSelectedToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButtonFlickr = new System.Windows.Forms.ToolStripDropDownButton();
			this.authenticateToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.enterCodeToolStrip = new System.Windows.Forms.ToolStripTextBox();
			this.uploadSelectedToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.uploadAllToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButtonHelp = new System.Windows.Forms.ToolStripDropDownButton();
			this.aboutToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxLocation = new System.Windows.Forms.ToolStripTextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.generateThumbsWorker = new System.ComponentModel.BackgroundWorker();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.regenerateThumbnailsToolStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.regenerateThumbnailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imagesMenuItem,
            this.flickrMenuItem,
            this.helpMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1126, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFilesMenuItem,
            this.chooseFolderMenuItem,
            this.exitMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// chooseFilesMenuItem
			// 
			this.chooseFilesMenuItem.Name = "chooseFilesMenuItem";
			this.chooseFilesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.chooseFilesMenuItem.Size = new System.Drawing.Size(199, 22);
			this.chooseFilesMenuItem.Text = "Choose Files...";
			this.chooseFilesMenuItem.Click += new System.EventHandler(this.chooseFilesMenuItem_Click);
			// 
			// chooseFolderMenuItem
			// 
			this.chooseFolderMenuItem.Name = "chooseFolderMenuItem";
			this.chooseFolderMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.chooseFolderMenuItem.Size = new System.Drawing.Size(199, 22);
			this.chooseFolderMenuItem.Text = "Choose Folder...";
			this.chooseFolderMenuItem.Click += new System.EventHandler(this.chooseFolderMenuItem_Click);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.exitMenuItem.Size = new System.Drawing.Size(199, 22);
			this.exitMenuItem.Text = "Exit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// imagesMenuItem
			// 
			this.imagesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllMenuItem,
            this.clearAllMenuItem,
            this.clearSelectedMenuItem,
            this.regenerateThumbnailsMenuItem});
			this.imagesMenuItem.Name = "imagesMenuItem";
			this.imagesMenuItem.Size = new System.Drawing.Size(57, 20);
			this.imagesMenuItem.Text = "Images";
			// 
			// selectAllMenuItem
			// 
			this.selectAllMenuItem.Enabled = false;
			this.selectAllMenuItem.Name = "selectAllMenuItem";
			this.selectAllMenuItem.ShortcutKeyDisplayString = "";
			this.selectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllMenuItem.Size = new System.Drawing.Size(199, 22);
			this.selectAllMenuItem.Text = "Select &All";
			this.selectAllMenuItem.ToolTipText = "Selects all the images in the window.";
			this.selectAllMenuItem.Click += new System.EventHandler(this.selectAllMenuItem_Click);
			// 
			// clearAllMenuItem
			// 
			this.clearAllMenuItem.Enabled = false;
			this.clearAllMenuItem.Name = "clearAllMenuItem";
			this.clearAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.clearAllMenuItem.Size = new System.Drawing.Size(199, 22);
			this.clearAllMenuItem.Text = "Clear All";
			this.clearAllMenuItem.Click += new System.EventHandler(this.clearAllMenuItem_Click);
			// 
			// clearSelectedMenuItem
			// 
			this.clearSelectedMenuItem.Enabled = false;
			this.clearSelectedMenuItem.Name = "clearSelectedMenuItem";
			this.clearSelectedMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.clearSelectedMenuItem.Size = new System.Drawing.Size(199, 22);
			this.clearSelectedMenuItem.Text = "Clear Selected";
			this.clearSelectedMenuItem.Click += new System.EventHandler(this.clearSelectedMenuItem_Click);
			// 
			// flickrMenuItem
			// 
			this.flickrMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.authenticateMenuItem,
            this.enterCodeMenuItem,
            this.uploadSelectedMenuItem,
            this.uploadAllMenuItem});
			this.flickrMenuItem.Name = "flickrMenuItem";
			this.flickrMenuItem.Size = new System.Drawing.Size(47, 20);
			this.flickrMenuItem.Text = "Flickr";
			// 
			// authenticateMenuItem
			// 
			this.authenticateMenuItem.Enabled = false;
			this.authenticateMenuItem.Name = "authenticateMenuItem";
			this.authenticateMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.authenticateMenuItem.Size = new System.Drawing.Size(201, 22);
			this.authenticateMenuItem.Text = "Au&thenticate";
			this.authenticateMenuItem.Click += new System.EventHandler(this.authenticateMenuItem_Click);
			// 
			// enterCodeMenuItem
			// 
			this.enterCodeMenuItem.Enabled = false;
			this.enterCodeMenuItem.Name = "enterCodeMenuItem";
			this.enterCodeMenuItem.Size = new System.Drawing.Size(100, 23);
			this.enterCodeMenuItem.Text = "Enter Code";
			this.enterCodeMenuItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.enterCodeMenuItem_KeyUp);
			// 
			// uploadSelectedMenuItem
			// 
			this.uploadSelectedMenuItem.Enabled = false;
			this.uploadSelectedMenuItem.Name = "uploadSelectedMenuItem";
			this.uploadSelectedMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
			this.uploadSelectedMenuItem.Size = new System.Drawing.Size(201, 22);
			this.uploadSelectedMenuItem.Text = "&Upload Selected";
			this.uploadSelectedMenuItem.Click += new System.EventHandler(this.uploadSelectedMenuItem_Click);
			// 
			// uploadAllMenuItem
			// 
			this.uploadAllMenuItem.Enabled = false;
			this.uploadAllMenuItem.Name = "uploadAllMenuItem";
			this.uploadAllMenuItem.Size = new System.Drawing.Size(201, 22);
			this.uploadAllMenuItem.Text = "Upload All";
			this.uploadAllMenuItem.Click += new System.EventHandler(this.uploadAllMenuItem_Click);
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpMenuItem.Text = "Help";
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Name = "aboutMenuItem";
			this.aboutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.aboutMenuItem.Size = new System.Drawing.Size(152, 22);
			this.aboutMenuItem.Text = "A&bout";
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.png";
			this.openFileDialog1.InitialDirectory = "Environment.SpecialFolder.MyPictures.ToString()";
			this.openFileDialog1.Multiselect = true;
			this.openFileDialog1.RestoreDirectory = true;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.regenerateThumbnailToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(195, 48);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedMenuItem_Click);
			// 
			// regenerateThumbnailToolStripMenuItem
			// 
			this.regenerateThumbnailToolStripMenuItem.Name = "regenerateThumbnailToolStripMenuItem";
			this.regenerateThumbnailToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.regenerateThumbnailToolStripMenuItem.Text = "Regenerate Thumbnail";
			this.regenerateThumbnailToolStripMenuItem.Visible = false;
			this.regenerateThumbnailToolStripMenuItem.Click += new System.EventHandler(this.regenerateThumbnailMenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(100, 100);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.comboBoxHiddenFromSearch);
			this.groupBox1.Controls.Add(this.comboBoxSafetyLevel);
			this.groupBox1.Controls.Add(this.comboBoxContentType);
			this.groupBox1.Controls.Add(this.checkBoxPublic);
			this.groupBox1.Controls.Add(this.checkBoxFriends);
			this.groupBox1.Controls.Add(this.checkBoxFamily);
			this.groupBox1.Controls.Add(this.textBoxDescription);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxTags);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxTitle);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(863, 52);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(251, 277);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Attributes";
			// 
			// comboBoxHiddenFromSearch
			// 
			this.comboBoxHiddenFromSearch.Enabled = false;
			this.comboBoxHiddenFromSearch.FormattingEnabled = true;
			this.comboBoxHiddenFromSearch.Location = new System.Drawing.Point(86, 249);
			this.comboBoxHiddenFromSearch.Name = "comboBoxHiddenFromSearch";
			this.comboBoxHiddenFromSearch.Size = new System.Drawing.Size(121, 21);
			this.comboBoxHiddenFromSearch.TabIndex = 9;
			this.comboBoxHiddenFromSearch.SelectedIndexChanged += new System.EventHandler(this.comboBoxHiddenFromSearch_SelectedIndexChanged);
			// 
			// comboBoxSafetyLevel
			// 
			this.comboBoxSafetyLevel.Enabled = false;
			this.comboBoxSafetyLevel.FormattingEnabled = true;
			this.comboBoxSafetyLevel.Location = new System.Drawing.Point(86, 222);
			this.comboBoxSafetyLevel.Name = "comboBoxSafetyLevel";
			this.comboBoxSafetyLevel.Size = new System.Drawing.Size(121, 21);
			this.comboBoxSafetyLevel.TabIndex = 8;
			this.comboBoxSafetyLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxSafetyLevel_SelectedIndexChanged);
			// 
			// comboBoxContentType
			// 
			this.comboBoxContentType.Enabled = false;
			this.comboBoxContentType.FormattingEnabled = true;
			this.comboBoxContentType.Location = new System.Drawing.Point(86, 195);
			this.comboBoxContentType.Name = "comboBoxContentType";
			this.comboBoxContentType.Size = new System.Drawing.Size(121, 21);
			this.comboBoxContentType.TabIndex = 7;
			this.comboBoxContentType.SelectedIndexChanged += new System.EventHandler(this.comboBoxContentType_SelectedIndexChanged);
			// 
			// checkBoxPublic
			// 
			this.checkBoxPublic.AutoSize = true;
			this.checkBoxPublic.Enabled = false;
			this.checkBoxPublic.Location = new System.Drawing.Point(86, 125);
			this.checkBoxPublic.Name = "checkBoxPublic";
			this.checkBoxPublic.Size = new System.Drawing.Size(55, 17);
			this.checkBoxPublic.TabIndex = 4;
			this.checkBoxPublic.Text = "Public";
			this.checkBoxPublic.UseVisualStyleBackColor = true;
			this.checkBoxPublic.CheckedChanged += new System.EventHandler(this.checkBoxPublic_CheckedChanged);
			// 
			// checkBoxFriends
			// 
			this.checkBoxFriends.AutoSize = true;
			this.checkBoxFriends.Enabled = false;
			this.checkBoxFriends.Location = new System.Drawing.Point(86, 172);
			this.checkBoxFriends.Name = "checkBoxFriends";
			this.checkBoxFriends.Size = new System.Drawing.Size(60, 17);
			this.checkBoxFriends.TabIndex = 6;
			this.checkBoxFriends.Text = "Friends";
			this.checkBoxFriends.UseVisualStyleBackColor = true;
			this.checkBoxFriends.CheckedChanged += new System.EventHandler(this.checkBoxFriends_CheckedChanged);
			// 
			// checkBoxFamily
			// 
			this.checkBoxFamily.AutoSize = true;
			this.checkBoxFamily.Enabled = false;
			this.checkBoxFamily.Location = new System.Drawing.Point(86, 148);
			this.checkBoxFamily.Name = "checkBoxFamily";
			this.checkBoxFamily.Size = new System.Drawing.Size(55, 17);
			this.checkBoxFamily.TabIndex = 5;
			this.checkBoxFamily.Text = "Family";
			this.checkBoxFamily.UseVisualStyleBackColor = true;
			this.checkBoxFamily.CheckedChanged += new System.EventHandler(this.checkBoxFamily_CheckedChanged);
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Enabled = false;
			this.textBoxDescription.Location = new System.Drawing.Point(86, 39);
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(159, 54);
			this.textBoxDescription.TabIndex = 2;
			this.textBoxDescription.Leave += new System.EventHandler(this.textBoxDescription_Leave);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(39, 252);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Hidden:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Description:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 225);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Safety Level:";
			// 
			// textBoxTags
			// 
			this.textBoxTags.Enabled = false;
			this.textBoxTags.Location = new System.Drawing.Point(86, 99);
			this.textBoxTags.Name = "textBoxTags";
			this.textBoxTags.Size = new System.Drawing.Size(159, 20);
			this.textBoxTags.TabIndex = 3;
			this.textBoxTags.Leave += new System.EventHandler(this.textBoxTags_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 198);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Content-Type:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(49, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Tags:";
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Enabled = false;
			this.textBoxTitle.Location = new System.Drawing.Point(86, 13);
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(159, 20);
			this.textBoxTitle.TabIndex = 1;
			this.textBoxTitle.Leave += new System.EventHandler(this.textBoxTitle_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(53, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title:";
			// 
			// uploadWorker
			// 
			this.uploadWorker.WorkerReportsProgress = true;
			this.uploadWorker.WorkerSupportsCancellation = true;
			this.uploadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadWorker_DoWork);
			this.uploadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.uploadWorker_ProgressChanged);
			this.uploadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uploadWorker_RunWorkerCompleted);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 469);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(1126, 26);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.AutoToolTip = true;
			this.toolStripProgressBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 20);
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.AutoSize = false;
			this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(260, 21);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(580, 21);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.Text = "Ready.";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
			this.toolStripStatusLabel1.MouseEnter += new System.EventHandler(this.toolStripStatusLabel1_MouseEnter);
			this.toolStripStatusLabel1.MouseLeave += new System.EventHandler(this.toolStripStatusLabel1_MouseLeave);
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.AutoSize = false;
			this.toolStripStatusLabel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(38, 21);
			this.toolStripStatusLabel2.Text = " ";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabel2.ToolTipText = "You are not authenticated.";
			// 
			// populateWorker
			// 
			this.populateWorker.WorkerReportsProgress = true;
			this.populateWorker.WorkerSupportsCancellation = true;
			this.populateWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.populateWorker_DoWork);
			this.populateWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.populateWorker_ProgressChanged);
			this.populateWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.populateWorker_RunWorkerCompleted);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonFile,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonImages,
            this.toolStripSeparator2,
            this.toolStripDropDownButtonFlickr,
            this.toolStripSeparator3,
            this.toolStripDropDownButtonHelp,
            this.toolStripTextBoxLocation});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1126, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButtonFile
			// 
			this.toolStripDropDownButtonFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFilesToolStrip,
            this.chooseFolderToolStrip,
            this.exitToolStrip});
			this.toolStripDropDownButtonFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFile.Image")));
			this.toolStripDropDownButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonFile.Name = "toolStripDropDownButtonFile";
			this.toolStripDropDownButtonFile.Size = new System.Drawing.Size(54, 22);
			this.toolStripDropDownButtonFile.Text = "File";
			// 
			// chooseFilesToolStrip
			// 
			this.chooseFilesToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("chooseFilesToolStrip.Image")));
			this.chooseFilesToolStrip.Name = "chooseFilesToolStrip";
			this.chooseFilesToolStrip.Size = new System.Drawing.Size(159, 22);
			this.chooseFilesToolStrip.Text = "Choose Files...";
			this.chooseFilesToolStrip.Click += new System.EventHandler(this.chooseFilesMenuItem_Click);
			// 
			// chooseFolderToolStrip
			// 
			this.chooseFolderToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("chooseFolderToolStrip.Image")));
			this.chooseFolderToolStrip.Name = "chooseFolderToolStrip";
			this.chooseFolderToolStrip.Size = new System.Drawing.Size(159, 22);
			this.chooseFolderToolStrip.Text = "Choose Folder...";
			this.chooseFolderToolStrip.Click += new System.EventHandler(this.chooseFolderMenuItem_Click);
			// 
			// exitToolStrip
			// 
			this.exitToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStrip.Image")));
			this.exitToolStrip.Name = "exitToolStrip";
			this.exitToolStrip.Size = new System.Drawing.Size(159, 22);
			this.exitToolStrip.Text = "Exit";
			this.exitToolStrip.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButtonImages
			// 
			this.toolStripDropDownButtonImages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStrip,
            this.clearAllToolStrip,
            this.clearSelectedToolStrip,
            this.regenerateThumbnailsToolStrip});
			this.toolStripDropDownButtonImages.Enabled = false;
			this.toolStripDropDownButtonImages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonImages.Image")));
			this.toolStripDropDownButtonImages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonImages.Name = "toolStripDropDownButtonImages";
			this.toolStripDropDownButtonImages.Size = new System.Drawing.Size(74, 22);
			this.toolStripDropDownButtonImages.Text = "Images";
			// 
			// selectAllToolStrip
			// 
			this.selectAllToolStrip.Enabled = false;
			this.selectAllToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStrip.Image")));
			this.selectAllToolStrip.Name = "selectAllToolStrip";
			this.selectAllToolStrip.Size = new System.Drawing.Size(199, 22);
			this.selectAllToolStrip.Text = "Select All";
			this.selectAllToolStrip.Click += new System.EventHandler(this.selectAllMenuItem_Click);
			// 
			// clearAllToolStrip
			// 
			this.clearAllToolStrip.Enabled = false;
			this.clearAllToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("clearAllToolStrip.Image")));
			this.clearAllToolStrip.Name = "clearAllToolStrip";
			this.clearAllToolStrip.Size = new System.Drawing.Size(199, 22);
			this.clearAllToolStrip.Text = "Clear All";
			this.clearAllToolStrip.Click += new System.EventHandler(this.clearAllMenuItem_Click);
			// 
			// clearSelectedToolStrip
			// 
			this.clearSelectedToolStrip.Enabled = false;
			this.clearSelectedToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("clearSelectedToolStrip.Image")));
			this.clearSelectedToolStrip.Name = "clearSelectedToolStrip";
			this.clearSelectedToolStrip.Size = new System.Drawing.Size(199, 22);
			this.clearSelectedToolStrip.Text = "Clear Selected";
			this.clearSelectedToolStrip.Click += new System.EventHandler(this.clearSelectedMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButtonFlickr
			// 
			this.toolStripDropDownButtonFlickr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.authenticateToolStrip,
            this.enterCodeToolStrip,
            this.uploadSelectedToolStrip,
            this.uploadAllToolStrip});
			this.toolStripDropDownButtonFlickr.Enabled = false;
			this.toolStripDropDownButtonFlickr.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFlickr.Image")));
			this.toolStripDropDownButtonFlickr.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonFlickr.Name = "toolStripDropDownButtonFlickr";
			this.toolStripDropDownButtonFlickr.Size = new System.Drawing.Size(64, 22);
			this.toolStripDropDownButtonFlickr.Text = "Flickr";
			// 
			// authenticateToolStrip
			// 
			this.authenticateToolStrip.Enabled = false;
			this.authenticateToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("authenticateToolStrip.Image")));
			this.authenticateToolStrip.Name = "authenticateToolStrip";
			this.authenticateToolStrip.Size = new System.Drawing.Size(160, 22);
			this.authenticateToolStrip.Text = "Authenticate";
			this.authenticateToolStrip.Click += new System.EventHandler(this.authenticateMenuItem_Click);
			// 
			// enterCodeToolStrip
			// 
			this.enterCodeToolStrip.Enabled = false;
			this.enterCodeToolStrip.Name = "enterCodeToolStrip";
			this.enterCodeToolStrip.Size = new System.Drawing.Size(100, 23);
			this.enterCodeToolStrip.Text = "Enter Code";
			this.enterCodeToolStrip.KeyUp += new System.Windows.Forms.KeyEventHandler(this.enterCodeMenuItem_KeyUp);
			// 
			// uploadSelectedToolStrip
			// 
			this.uploadSelectedToolStrip.Enabled = false;
			this.uploadSelectedToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("uploadSelectedToolStrip.Image")));
			this.uploadSelectedToolStrip.Name = "uploadSelectedToolStrip";
			this.uploadSelectedToolStrip.Size = new System.Drawing.Size(160, 22);
			this.uploadSelectedToolStrip.Text = "Upload Selected";
			this.uploadSelectedToolStrip.Click += new System.EventHandler(this.uploadSelectedMenuItem_Click);
			// 
			// uploadAllToolStrip
			// 
			this.uploadAllToolStrip.Enabled = false;
			this.uploadAllToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("uploadAllToolStrip.Image")));
			this.uploadAllToolStrip.Name = "uploadAllToolStrip";
			this.uploadAllToolStrip.Size = new System.Drawing.Size(160, 22);
			this.uploadAllToolStrip.Text = "Upload All";
			this.uploadAllToolStrip.Click += new System.EventHandler(this.uploadAllMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButtonHelp
			// 
			this.toolStripDropDownButtonHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStrip});
			this.toolStripDropDownButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonHelp.Image")));
			this.toolStripDropDownButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonHelp.Name = "toolStripDropDownButtonHelp";
			this.toolStripDropDownButtonHelp.Size = new System.Drawing.Size(61, 22);
			this.toolStripDropDownButtonHelp.Text = "Help";
			// 
			// aboutToolStrip
			// 
			this.aboutToolStrip.Image = global::WinFlickSharp.Properties.Resources.profile;
			this.aboutToolStrip.Name = "aboutToolStrip";
			this.aboutToolStrip.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStrip.Text = "About";
			this.aboutToolStrip.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// toolStripTextBoxLocation
			// 
			this.toolStripTextBoxLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.toolStripTextBoxLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.toolStripTextBoxLocation.Name = "toolStripTextBoxLocation";
			this.toolStripTextBoxLocation.Size = new System.Drawing.Size(566, 25);
			this.toolStripTextBoxLocation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxLocation_KeyUp_1);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.flowLayoutPanel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 52);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(845, 418);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.Click += new System.EventHandler(this.flowLayoutPanel_Click);
			// 
			// generateThumbsWorker
			// 
			this.generateThumbsWorker.WorkerReportsProgress = true;
			this.generateThumbsWorker.WorkerSupportsCancellation = true;
			this.generateThumbsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GenerateThumbsWorker_DoWork);
			this.generateThumbsWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GenerateThumbsWorker_ProgressChanged);
			this.generateThumbsWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GenerateThumbsWorker_RunWorkerCompleted);
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "delete.ICO");
			this.imageList2.Images.SetKeyName(1, "125_31.ico");
			// 
			// regenerateThumbnailsToolStrip
			// 
			this.regenerateThumbnailsToolStrip.Name = "regenerateThumbnailsToolStrip";
			this.regenerateThumbnailsToolStrip.Size = new System.Drawing.Size(199, 22);
			this.regenerateThumbnailsToolStrip.Text = "Regenerate Thumbnails";
			this.regenerateThumbnailsToolStrip.Click += new System.EventHandler(this.regenerateThumbnailsMenuItem_Click);
			// 
			// regenerateThumbnailsMenuItem
			// 
			this.regenerateThumbnailsMenuItem.Name = "regenerateThumbnailsMenuItem";
			this.regenerateThumbnailsMenuItem.Size = new System.Drawing.Size(199, 22);
			this.regenerateThumbnailsMenuItem.Text = "Regenerate Thumbnails";
			this.regenerateThumbnailsMenuItem.Click += new System.EventHandler(this.regenerateThumbnailsMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1126, 495);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 500);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flickrMenuItem;
        private System.Windows.Forms.ToolStripMenuItem authenticateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadSelectedMenuItem;
        private System.Windows.Forms.ToolStripTextBox enterCodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFilesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFolderMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxPublic;
        private System.Windows.Forms.CheckBox checkBoxFriends;
        private System.Windows.Forms.CheckBox checkBoxFamily;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSafetyLevel;
        private System.Windows.Forms.ComboBox comboBoxContentType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxHiddenFromSearch;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker uploadWorker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker populateWorker;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem imagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.ComponentModel.BackgroundWorker generateThumbsWorker;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxLocation;
        private System.Windows.Forms.ToolStripMenuItem regenerateThumbnailToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFile;
		private System.Windows.Forms.ToolStripMenuItem chooseFilesToolStrip;
		private System.Windows.Forms.ToolStripMenuItem chooseFolderToolStrip;
		private System.Windows.Forms.ToolStripMenuItem exitToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonImages;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStrip;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStrip;
		private System.Windows.Forms.ToolStripMenuItem clearSelectedToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFlickr;
		private System.Windows.Forms.ToolStripMenuItem authenticateToolStrip;
		private System.Windows.Forms.ToolStripTextBox enterCodeToolStrip;
		private System.Windows.Forms.ToolStripMenuItem uploadSelectedToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonHelp;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStrip;
		private System.Windows.Forms.ToolStripMenuItem uploadAllToolStrip;
		private System.Windows.Forms.ToolStripMenuItem uploadAllMenuItem;
		private System.Windows.Forms.ToolStripMenuItem regenerateThumbnailsToolStrip;
		private System.Windows.Forms.ToolStripMenuItem regenerateThumbnailsMenuItem;
	}
}

