using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using FlickrNet;
using Helpers;
using PhotoPanel;

namespace WinFlickSharp
{
    /// <summary>
    /// This class provides support for uploading photos to Flickr.
    /// </summary>
    public partial class Form1 : Form
    {
#region Private Properties
        private OAuthRequestToken requestToken;
        private AuthBrowser authWindow;
        private string[] extensions = Properties.Settings.Default.Extensions.Split(';');
        private List<FlickrPhotoPanel> listViewItems;
        private ProgressDialog pgd;
        private readonly string FormPreamble = Properties.Settings.Default.FormPreamble;
        private int itemcount = 0;
        private int selectedcount = 0;
        private long selectedbytes = 0;
        private bool authenticated = false;
        #endregion

#region Constructor
        /// <summary>
        /// Constructs a new <see cref="Form1"/> object.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            comboBoxContentType.DataSource = Enum.GetValues(typeof(ContentType));
            comboBoxSafetyLevel.DataSource = Enum.GetValues(typeof(SafetyLevel));
            comboBoxHiddenFromSearch.DataSource = Enum.GetValues(typeof(HiddenFromSearch));
            listViewItems = new List<FlickrPhotoPanel>(1000);
            pgd = new ProgressDialog();
            if (FlickrManager.OAuthToken != null && FlickrManager.OAuthToken.Token != null)
            {
                authenticated = true;
                authenticateToolStripMenuItem.Enabled = false;
                EnterCodeToolStripMenuItem.Enabled = false;
                toolStripStatusLabel2.Image = Properties.Resources._125_31;
                toolStripStatusLabel2.ToolTipText = string.Format("You are authenticated as: {0}", FlickrManager.OAuthToken.FullName);
                this.Text = FormPreamble + " - " + FlickrManager.OAuthToken.FullName;
            }
            else
            {
                authenticated = false;
                authenticateToolStripMenuItem.Enabled = true;
                toolStripStatusLabel2.Image = Properties.Resources._200;
                toolStripStatusLabel2.ToolTipText = "You are not authenticated.";
                this.Text = FormPreamble + " - Not Authenticated";
            }
        }
        #endregion

#region Private Methods
        private void UpdateStatusLabel()
        {
            string status = string.Format("{0} item{1}", itemcount, itemcount != 1 ? "s" : "");
            if (selectedcount != 0)
            {
                status += string.Format("     {0} item{1} selected     {2}", selectedcount, selectedcount != 1 ? "s":"", StringUtilities.GetBytesReadable(selectedbytes));
            }
            toolStripStatusLabel3.Text = status;
        }

        private FlickrPhotoPanel GetFirstSelectedFlickrPhotoPanel()
        {
            int[] indices = GetSelectedIndices(flowLayoutPanel1);
            if (indices.Length == 0) return null;

            return (FlickrPhotoPanel)flowLayoutPanel1.Controls[indices[0]];
        }

        private void ProcessFiles(string[] files)
        {
            if (GenerateThumbsWorker.IsBusy)
            {
                GenerateThumbsWorker.CancelAsync();
            }
            flowLayoutPanel1.Controls.Clear();
            populateWorker.RunWorkerAsync(files);
            pgd = new ProgressDialog();
            pgd.progressBar1.Style = ProgressBarStyle.Blocks;
            pgd.Text = "Generating thumbnails. This could take a while...";
            if (DialogResult.Cancel == pgd.ShowDialog())
            {
                populateWorker.CancelAsync();
            }
        }

        private int[] GetSelectedIndices(FlowLayoutPanel flow)
        {
            List<int> indices = new List<int>();
            int i = 0;
            foreach (var item in flow.Controls)
            {
                var fpp = (FlickrPhotoPanel)item;
                if (fpp.IsSelected)
                {
                    indices.Add(i);
                }
                i++;
            }
            return indices.ToArray();
        }

        private void UnselectAll()
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var lvi = (FlickrPhotoPanel)item;
                lvi.IsSelected = false;
                lvi.Invalidate();
            }
            selectedcount = 0;
            selectedbytes = 0;
        }

        private void UnselectAll(FlickrPhotoPanel fpp)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var lvi = (FlickrPhotoPanel)item;
                if (lvi != fpp)
                {
                    lvi.IsSelected = false;
                    lvi.Invalidate();
                }
            }
            selectedcount = 0;
            selectedbytes = 0;
        }

        private void SelectAll()
        {
            long totalbytes = 0;
            int count = 0;
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var lvi = (FlickrPhotoPanel)item;
                totalbytes += lvi.FileSizeBytes;
                lvi.IsSelected = true;
                lvi.Invalidate();
                count++;
            }
            selectedcount = count;
            selectedbytes = (int)totalbytes;
            UpdateStatusLabel();
        }

        private void ProcessFolder(string folder)
        {
            var di = new DirectoryInfo(folder);
            FileInfo[] files =
                di.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();
            ProcessFiles(files.Select(f => f.FullName).ToArray());
        }

        private void FillOutForm(FlickrPhotoPanel fpp)
        {
            textBoxTitle.Text = fpp.Title;
            textBoxDescription.Text = fpp.Description;
            textBoxTags.Text = string.Join(";", fpp.Tags);
            checkBoxPublic.Checked = fpp.IsPublic;
            checkBoxFamily.Checked = fpp.VisibleToFamily;
            checkBoxFriends.Checked = fpp.VisibleToFriends;
            comboBoxContentType.SelectedItem = fpp.ContentType;
            comboBoxSafetyLevel.SelectedItem = fpp.SafetyLevel;
            comboBoxHiddenFromSearch.SelectedItem = fpp.HiddenFromSearch;
            textBoxTitle.Enabled = textBoxDescription.Enabled = textBoxTags.Enabled = checkBoxPublic.Enabled = checkBoxFamily.Enabled = checkBoxFriends.Enabled = comboBoxContentType.Enabled = comboBoxSafetyLevel.Enabled = comboBoxHiddenFromSearch.Enabled = true;
        }

        private void ClearForm()
        {
            textBoxTitle.Enabled = textBoxDescription.Enabled = textBoxTags.Enabled = checkBoxPublic.Enabled = checkBoxFamily.Enabled = checkBoxFriends.Enabled = comboBoxContentType.Enabled = comboBoxSafetyLevel.Enabled = comboBoxHiddenFromSearch.Enabled = false;
            textBoxTitle.Text = textBoxDescription.Text = textBoxTags.Text = "";
            checkBoxPublic.Checked = checkBoxFamily.Checked = checkBoxFriends.Checked = false;
            comboBoxContentType.SelectedItem = comboBoxSafetyLevel.SelectedItem = comboBoxHiddenFromSearch.SelectedItem = null;
        }

        private void Authenticate()
        {
            Flickr f = FlickrManager.GetInstance();
            try
            {
                var accessToken = f.OAuthGetAccessToken(requestToken, EnterCodeToolStripMenuItem.Text);
                FlickrManager.OAuthToken = accessToken;
                Text = accessToken.FullName;
                authWindow.Close();
                EnterCodeToolStripMenuItem.Enabled = false;
                authenticated = true;
            }
            catch (FlickrApiException ex)
            {
                MessageBox.Show("Failed to get access token. Error message: " + ex.Message);
            }
        }
        #endregion

#region Event Handlers
        #region Dialogs
        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            var di = new DirectoryInfo(openFileDialog1.FileNames[0]);
            toolStripTextBoxLocation.Text = di.FullName;
            ProcessFiles(openFileDialog1.FileNames);
        }

        private void openFolderMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;

            toolStripTextBoxLocation.Text = folderBrowserDialog1.SelectedPath;
            ProcessFolder(folderBrowserDialog1.SelectedPath);
        }
        #endregion

        #region Toolbar
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void authenticateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FlickrManager.OAuthToken == null || FlickrManager.OAuthToken.Token == null)
            {
                authenticateToolStripMenuItem.Enabled = false;
                Flickr f = FlickrManager.GetInstance();
                requestToken = f.OAuthGetRequestToken("oob");

                string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write);
                authWindow = new AuthBrowser();
                authWindow.webControl1.Source = new Uri(url);
                authWindow.Show();
                EnterCodeToolStripMenuItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("You have already authenticated.");
            }
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadToolStripMenuItem.Enabled = toolStripButtonUpload.Enabled = false;
            UploadWorker.RunWorkerAsync();
        }

        private void EnterCodeToolStripMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Authenticate();
            }
        }

        private void regenerateThumbnailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    FlickrPhotoPanel fpp = (FlickrPhotoPanel)owner.SourceControl;
                    using (var bm = Image.FromFile(fpp.FileName))
                    {
                        fpp.Thumbnail = GraphicsUtilities.ResizeImage(bm, 120, 120, Color.Black, true);
                        fpp.OriginalSize = new System.Drawing.Size(bm.Width, bm.Height);
                    }
                    fpp.Invalidate();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            List<FlickrPhotoPanel> deleted = new List<FlickrPhotoPanel>();
            if (lvi == null)
            {
                ToolStripItem item = (sender as ToolStripItem);
                if (item != null)
                {
                    ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                    if (owner != null)
                    {
                        FlickrPhotoPanel fpp = (FlickrPhotoPanel)owner.SourceControl;
                        deleted.Add(fpp);
                    }
                }
            }
            else
            {
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    var fpp = (FlickrPhotoPanel)item;
                    if (fpp.IsSelected)
                    {
                        deleted.Add(fpp);
                    }
                }
            }
            foreach (var item in deleted)
            {
                flowLayoutPanel1.Controls.Remove(item);
                item.Dispose();
                //listViewItems.Remove(item);
            }
            UnselectAll();
            flowLayoutPanel1.Invalidate();
            uploadToolStripMenuItem.Enabled = toolStripButtonUpload.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ad = new AboutDialog();
            ad.ShowDialog();
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var fpp = (FlickrPhotoPanel)item;
                fpp.Dispose();
            }
            GC.Collect();
            flowLayoutPanel1.Controls.Clear();
            uploadToolStripMenuItem.Enabled = toolStripButtonUpload.Enabled = false;

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        #endregion

        #region Form event handlers
        private void textBoxTitle_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Title = textBoxTitle.Text;
            lvi.Invalidate();
        }

        private void textBoxDescription_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Description = textBoxDescription.Text;
            lvi.Invalidate();
        }

        private void textBoxTags_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Tags = textBoxTags.Text.Split(';');
            lvi.Invalidate();
        }

        private void checkBoxPublic_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.IsPublic = checkBoxPublic.Checked;
            lvi.Invalidate();
        }

        private void checkBoxFamily_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.VisibleToFamily = checkBoxFamily.Checked;
            lvi.Invalidate();
        }

        private void checkBoxFriends_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.VisibleToFriends = checkBoxFriends.Checked;
            lvi.Invalidate();
        }

        private void comboBoxContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            ContentType cresult;
            if (Enum.TryParse(comboBoxContentType.Text, out cresult))
            lvi.ContentType = cresult;
            lvi.Invalidate();
        }

        private void comboBoxSafetyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            SafetyLevel cresult;
            if (Enum.TryParse(comboBoxSafetyLevel.Text, out cresult))
                lvi.SafetyLevel = cresult;
            lvi.Invalidate();
        }

        private void comboBoxHiddenFromSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            HiddenFromSearch cresult;
            if (Enum.TryParse(comboBoxHiddenFromSearch.Text, out cresult))
                lvi.HiddenFromSearch = cresult;
            lvi.Invalidate();
        }

        private void toolStripTextBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessFolder(toolStripTextBoxLocation.Text);
            }
        }
        #endregion

        #region ToolStrip
        private void EnterCodeToolStripMenuItem_Enter(object sender, EventArgs e)
        {
            EnterCodeToolStripMenuItem.SelectAll();
        }

        private void toolStripButtonOpenFiles_Click(object sender, EventArgs e)
        {
            openFileMenuItem_Click(sender, e);
        }

        private void toolStripButtonOpenFolder_Click(object sender, EventArgs e)
        {
            openFolderMenuItem_Click(sender, e);
        }

        private void toolStripButtonClearList_Click(object sender, EventArgs e)
        {
            clearListToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonRemoveSelected_Click(object sender, EventArgs e)
        {
            removeToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonAuthenticate_Click(object sender, EventArgs e)
        {
            authenticateToolStripMenuItem_Click(sender, e);
        }

        private void toolStripTextBoxLocation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var me = (ToolStripTextBox)sender;
                EnterCodeToolStripMenuItem.Text = me.Text;
                EnterCodeToolStripMenuItem_KeyUp(sender, e);
            }
        }

        private void toolStripButtonUpload_Click(object sender, EventArgs e)
        {
            uploadToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            aboutToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region Form
        private void Form1_Load(object sender, EventArgs e)
        {
            Helpers.GraphicsUtilities.DoubleBuffered(flowLayoutPanel1, true);
        }

        private void toolStripTextBoxLocation_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Directory.Exists(toolStripTextBoxLocation.Text))
                    ProcessFolder(toolStripTextBoxLocation.Text);
            }
        }

        private void flowLayoutPanel_Click(object sender, EventArgs e)
        {
            this.ActiveControl = GetFirstSelectedFlickrPhotoPanel();
            textBoxTitle.Text = textBoxDescription.Text = textBoxTags.Text = "";
            checkBoxFamily.Checked = checkBoxFriends.Checked = checkBoxPublic.Checked = false;
            comboBoxContentType.SelectedItem = comboBoxSafetyLevel.SelectedItem = comboBoxHiddenFromSearch.SelectedItem = null;
            textBoxTitle.Enabled = textBoxDescription.Enabled = textBoxTags.Enabled = checkBoxPublic.Enabled = checkBoxFamily.Enabled = checkBoxFriends.Enabled = comboBoxContentType.Enabled = comboBoxSafetyLevel.Enabled = comboBoxHiddenFromSearch.Enabled = false;
            UnselectAll();
            selectedcount = 0;
            selectedbytes = 0;
            UpdateStatusLabel();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        selectAllToolStripMenuItem_Click(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Lvi_DoubleClick(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            this.ActiveControl = lvi;
            if (lvi == null) return;
            Process.Start(lvi.FileName);
        }

        private void Lvi_Click(object sender, EventArgs e)
        {
            var fpp = (FlickrPhotoPanel)sender;
            this.ActiveControl = fpp;
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (fpp.IsSelected)
                {
                    fpp.IsSelected = false;
                    selectedcount -= 1;
                    selectedbytes -= fpp.FileSizeBytes;
                }
                else
                {
                    fpp.IsSelected = true;
                    selectedcount += 1;
                    selectedbytes += fpp.FileSizeBytes;
                }
            }
            else
            {
                fpp.IsSelected = true;
                UnselectAll(fpp);
                selectedcount = 1;
                selectedbytes = fpp.FileSizeBytes;
            }
            if (selectedcount == 1)
            {
                FillOutForm(fpp);
            }
            else
            {
                ClearForm();
            }
            fpp.Invalidate();
            UpdateStatusLabel();
        }

        private void Lvi_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    int rindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    int next = rindex + 1 >= flowLayoutPanel1.Controls.Count ? 0 : rindex + 1;
                    var fpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[rindex];
                    UnselectAll(fpp);
                    var nextfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[next];
                    fpp.IsSelected = false;
                    fpp.Invalidate();
                    nextfpp.IsSelected = true;
                    nextfpp.Invalidate();
                    FillOutForm(nextfpp);
                    flowLayoutPanel1.ScrollControlIntoView(nextfpp);
                    break;
                case Keys.Left:
                    int lindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    int prev = lindex - 1 >= 0 ? lindex - 1 : flowLayoutPanel1.Controls.Count - 1;
                    var lfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[lindex];
                    UnselectAll(lfpp);
                    var prevfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[prev];
                    lfpp.IsSelected = false;
                    lfpp.Invalidate();
                    prevfpp.IsSelected = true;
                    prevfpp.Invalidate();
                    FillOutForm(prevfpp);
                    flowLayoutPanel1.ScrollControlIntoView(prevfpp);
                    break;
                case Keys.Up:
                    int uindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    var ufpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[uindex];
                    UnselectAll(ufpp);
                    int columns = flowLayoutPanel1.Width / ufpp.Width;
                    int max = flowLayoutPanel1.Controls.Count - 1;
                    int uprev = uindex - columns >= 0 ? uindex - columns : max - (max % columns) + uindex;
                    if (uprev > max)
                    {
                        uprev -= columns;
                    }
                    var prevufpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[uprev];
                    ufpp.IsSelected = false;
                    ufpp.Invalidate();
                    prevufpp.IsSelected = true;
                    prevufpp.Invalidate();
                    flowLayoutPanel1.ScrollControlIntoView(prevufpp);
                    break;
                case Keys.Down:
                    int dindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    var dfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[dindex];
                    UnselectAll(dfpp);
                    int dcolumns = flowLayoutPanel1.Width / dfpp.Width;
                    int dprev = dindex + dcolumns < flowLayoutPanel1.Controls.Count ? dindex + dcolumns : dindex % dcolumns;
                    var prevdfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[dprev];
                    dfpp.IsSelected = false;
                    dfpp.Invalidate();
                    prevdfpp.IsSelected = true;
                    prevdfpp.Invalidate();
                    flowLayoutPanel1.ScrollControlIntoView(prevdfpp);
                    break;
                default:
                    break;
            }
        }

        #endregion
        #endregion

#region Upload BackgroundWorker
        private void uploadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = FlickrManager.GetAuthInstance();
            f.OnUploadProgress += new EventHandler<UploadProgressEventArgs>(Flickr_OnUploadProgress);
            //            ListView.ListViewItemCollection lvic = listView1.Groups[0].Items;
            
            foreach (var lvi in flowLayoutPanel1.Controls)
            {
                FlickrPhotoPanel lv = (FlickrPhotoPanel)lvi;
                if (lv.IsSelected)
                {
                    string photoId = f.UploadPicture(lv.FileName,
                        lv.Title,
                        lv.Description,
                        string.Join(";", lv.Tags),
                        lv.IsPublic,
                        lv.VisibleToFamily,
                        lv.VisibleToFriends);
                }
            }
        }

        private void uploadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // TODO: Indicate what's been uploaded
            //
            UnselectAll();
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Ready.";
            uploadToolStripMenuItem.Enabled = true;
            toolStripButtonUpload.Enabled = true;
        }

        private void uploadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;            
        }

        private void Flickr_OnUploadProgress(object sender, UploadProgressEventArgs e)
        {
            UploadWorker.ReportProgress(e.ProcessPercentage);
            toolStripStatusLabel1.Text = e.FullFileName;
        }
        #endregion

#region Populate List BackgroundWorker
        private void populateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;
            BackgroundWorker b = sender as BackgroundWorker;
            var us = new UserState();
            decimal i = 1;
            decimal count = filenames.Length;
            var starttime = DateTime.Now;
            listViewItems.Clear();
            Parallel.ForEach(filenames, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (file) =>
            {
                if (b.CancellationPending)
                {
                    return;
                }
                var lvi = new FlickrPhotoPanel();
                lvi.Click += Lvi_Click;
                lvi.DoubleClick += Lvi_DoubleClick;
                lvi.KeyUp += Lvi_KeyUp;
                lvi.ContextMenuStrip = this.contextMenuStrip1;
                lvi.Width = 250;
                lvi.Height = 130;
                lvi.FileName = file;
                lvi.FileSizeBytes = new FileInfo(file).Length;
                listViewItems.Add(lvi);
                var tt = new ToolTip();
                us.UpdateStatus = UpdateStatus.Success;
                decimal percent = (i / count) * 100m;
                decimal itemsremaining = count - i;
                var ticksperitem = DateTime.Now.Subtract(starttime).Ticks / i;
                var ticksremaining = ticksperitem * itemsremaining;
                var timeremaining = new TimeSpan((long)ticksremaining);
                us.Message = string.Format("Processing File {1} of {2}.\r\n{0}% Completed.\r\nEst. Time Remaining: {3}{4}{5}\r\n{6}",
                    (int)percent,
                    i,
                    count,
                    timeremaining.Hours > 0 ? timeremaining.Hours.ToString() + " hours" : "",
                    timeremaining.Minutes > 0 ? " " + timeremaining.Minutes.ToString() + " minutes" : "",
                    timeremaining.Seconds > 0 ? " " + timeremaining.Seconds.ToString() + " seconds" : "",
                    file);
                populateWorker.ReportProgress((int)Math.Round(percent), us);
                i++;
            });
        }

        private void populateWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var us = (UserState)e.UserState;
            BackgroundWorker b = sender as BackgroundWorker;
            if (b.CancellationPending)
            {
                return;
            }
            if (us.UpdateStatus == UpdateStatus.Success)
            {
                pgd.progressBar1.Value = e.ProgressPercentage;
                pgd.Label = us.Message;
            }
        }

        private void populateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pgd.Close();
            toolStripProgressBar1.Value = 0;
            flowLayoutPanel1.SuspendLayout();
            listViewItems.Sort((x, y) => x.FileName.CompareTo(y.FileName));
            flowLayoutPanel1.Controls.AddRange(listViewItems.ToArray());
            flowLayoutPanel1.ResumeLayout();
            if (authenticated)
            {
                uploadToolStripMenuItem.Enabled = toolStripButtonUpload.Enabled = true;
            }
            itemcount = flowLayoutPanel1.Controls.Count;
            UpdateStatusLabel();
            GenerateThumbsWorker.RunWorkerAsync();
        }
        #endregion

#region Generate Thumbnails Worker
        private void GenerateThumbsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var us = new UserState();
            Parallel.ForEach<Control>(flowLayoutPanel1.Controls.Cast<Control>(), new ParallelOptions { MaxDegreeOfParallelism = 1 }, (item) =>

            //            foreach (var item in flowLayoutPanel1.Controls)
            {
                var fpp = (FlickrPhotoPanel)item;
                us.LVI = fpp;
                using (var bm = Image.FromFile(fpp.FileName))
                {
                    us.Thumbnail = GraphicsUtilities.ResizeImage(bm, 120, 120, Color.Black, true);
                    us.OriginalSize = new System.Drawing.Size(bm.Width, bm.Height);
                }
                GenerateThumbsWorker.ReportProgress(0, us);
            });
        }

        private void GenerateThumbsWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var us = (UserState)e.UserState;
            var lvi = us.LVI;
            lvi.Thumbnail = us.Thumbnail;
            lvi.OriginalSize = us.OriginalSize;
            lvi.Invalidate();
        }
        #endregion

    }
}
