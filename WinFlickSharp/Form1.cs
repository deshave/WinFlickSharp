//#define debug
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
            listViewItems = new List<FlickrPhotoPanel>();
            pgd = new ProgressDialog();
            if (FlickrManager.OAuthToken != null && FlickrManager.OAuthToken.Token != null)
            {
                authenticateToolStripMenuItem.Enabled = false;
                EnterCodeToolStripMenuItem.Enabled = false;
                toolStripButtonAuthenticate.Enabled = false;
                toolStripTextBoxEnterCode.Enabled = false;
                toolStripStatusLabel2.Image = Properties.Resources._125_31;
                toolStripStatusLabel2.ToolTipText = string.Format("You are authenticated as: {0}", FlickrManager.OAuthToken.FullName);
                this.Text = FormPreamble + " - " + FlickrManager.OAuthToken.FullName;
            }
            else
            {
                authenticated = true;
                authenticateToolStripMenuItem.Enabled = true;
                toolStripButtonAuthenticate.Enabled = true;
                EnterCodeToolStripMenuItem.Enabled = true;
                toolStripTextBoxEnterCode.Enabled = true;
                toolStripStatusLabel2.Image = Properties.Resources._200;
                toolStripStatusLabel2.ToolTipText = "You are not authenticated.";
                this.Text = FormPreamble;
            }
        }
        #endregion

#region Private Methods
        private void UpdateStatusLabel()
        {
            string status = string.Format("{0} item{1}", itemcount, itemcount != 1 ? "s" : "");
            if (selectedcount != 0)
            {
                status += string.Format("     {0} item{1} selected     {2} bytes", selectedcount, selectedcount != 1 ? "s":"", StringUtilities.GetBytesReadable(selectedbytes));
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
            flowLayoutPanel1.Controls.Clear();
            imageList1.Images.Clear();
            //listViewItems.Clear();
            backgroundWorker2.RunWorkerAsync(files);
            pgd = new ProgressDialog();
            pgd.progressBar1.Style = ProgressBarStyle.Blocks;
            pgd.Text = "Generating thumbnails. This could take a while...";
            if (DialogResult.Cancel == pgd.ShowDialog())
            {
                backgroundWorker2.CancelAsync();
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
        #endregion

#region Event Handlers
        #region Dialogs
        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            ProcessFiles(openFileDialog1.FileNames);
        }

        private void openFolderMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;

            var di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
            FileInfo[] files =
                di.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();
            ProcessFiles(files.Select(f => f.FullName).ToArray());
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
            backgroundWorker1.RunWorkerAsync();
        }

        private void EnterCodeToolStripMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Flickr f = FlickrManager.GetInstance();
                try
                {
                    var accessToken = f.OAuthGetAccessToken(requestToken, EnterCodeToolStripMenuItem.Text);
                    FlickrManager.OAuthToken = accessToken;
                    Text = accessToken.FullName;
                    authWindow.Close();
                    EnterCodeToolStripMenuItem.Enabled = false;
                    toolStripTextBoxEnterCode.Enabled = false;
                    authenticateToolStripMenuItem.Enabled = false;
                    toolStripButtonAuthenticate.Enabled = false;
                    authenticated = true;
                }
                catch (FlickrApiException ex)
                {
                    MessageBox.Show("Failed to get access token. Error message: " + ex.Message);
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
            //listViewItems.Clear();
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
            lvi.Public = checkBoxPublic.Checked;
            lvi.Invalidate();
        }

        private void checkBoxFamily_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Family = checkBoxFamily.Checked;
            lvi.Invalidate();
        }

        private void checkBoxFriends_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Friends = checkBoxFriends.Checked;
            lvi.Invalidate();
        }

        private void comboBoxContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            ContentType cresult;
            if (Enum.TryParse(comboBoxContentType.Text, out cresult))
            lvi.Type = cresult;
            lvi.Invalidate();
        }

        private void comboBoxSafetyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            SafetyLevel cresult;
            if (Enum.TryParse(comboBoxSafetyLevel.Text, out cresult))
                lvi.Level = cresult;
            lvi.Invalidate();
        }

        private void comboBoxHiddenFromSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            HiddenFromSearch cresult;
            if (Enum.TryParse(comboBoxHiddenFromSearch.Text, out cresult))
                lvi.Hidden = cresult;
            lvi.Invalidate();
        }
        #endregion

        #region ToolStrip
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

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var me = (ToolStripMenuItem)sender;
                EnterCodeToolStripMenuItem.Text = me.Text;
                EnterCodeToolStripMenuItem_KeyUp(sender, e);
            }
        }

        private void toolStripButtonUpload_Click(object sender, EventArgs e)
        {
            uploadToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            aboutToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region Form
        private void flowLayoutPanel_Click(object sender, EventArgs e)
        {
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
            if (lvi == null) return;
            Process.Start(lvi.FileName);
        }

        private void Lvi_Click(object sender, EventArgs e)
        {
            var fpp = (FlickrPhotoPanel)sender;
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                fpp.IsSelected = !fpp.IsSelected;

            }
            else
            {
                fpp.IsSelected = true;
                UnselectAll(fpp);
            }
            fpp.Invalidate();
            UpdateStatusLabel();
        }
        #endregion
#endregion

#region Upload BackgroundWorker
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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
                        lv.Public,
                        lv.Family,
                        lv.Friends);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;            
        }

        private void Flickr_OnUploadProgress(object sender, UploadProgressEventArgs e)
        {
            backgroundWorker1.ReportProgress(e.ProcessPercentage);
            toolStripStatusLabel1.Text = e.FullFileName;
        }
        #endregion

#region Populate List BackgroundWorker
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;
            BackgroundWorker b = sender as BackgroundWorker;
            var us = new UserState();
            decimal i = 1;
            decimal count = filenames.Length;
#if debug
            Console.WriteLine(string.Format("Processing {0} files.", count));
#endif
            var starttime = DateTime.Now;
            listViewItems.Clear();
            Parallel.ForEach(filenames, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (file) =>
//            foreach (var file in filenames)
            {
#if debug
                Console.WriteLine("Processing {0}", file);
#endif
                if (b.CancellationPending)
                {
#if debug
                    Console.WriteLine("{0}:BackgroundWorker cancelled.", file);
#endif
                    return;
                }
                var lvi = new FlickrPhotoPanel();
                using (var bm = Image.FromFile(file))
                {
#if debug
                    Console.WriteLine("Generating Thumbnail.");
#endif
                    lvi.Thumbnail = GraphicsUtilities.ResizeImage(bm, 120, 120, true);
                }
                lvi.Click += Lvi_Click;
                lvi.DoubleClick += Lvi_DoubleClick;
                lvi.ContextMenuStrip = this.contextMenuStrip1;
                lvi.Width = 250;
                lvi.Height = 130;
                lvi.FileName = file;
                lvi.FileSizeBytes = new FileInfo(file).Length;
                listViewItems.Add(lvi);
                us.UpdateStatus = UpdateStatus.Success;
                //us.LVI = lvi;
                decimal percent = (i / count) * 100m;
#if debug
                Console.WriteLine("We are {0}% done with the list.", percent);
#endif
                decimal itemsremaining = count - i;
#if debug
                Console.WriteLine("{0} items remaining.", itemsremaining);
#endif
                var ticksperitem = DateTime.Now.Subtract(starttime).Ticks / i;
#if debug
                Console.WriteLine("Currently {0} ticks per item.", ticksperitem);
#endif
                var ticksremaining = ticksperitem * itemsremaining;
#if debug
                Console.WriteLine("Currently {0} ticks remaining.", ticksremaining);
#endif
                var timeremaining = new TimeSpan((long)ticksremaining);
#if debug
                Console.WriteLine("Currently {0} time remaining.", timeremaining.ToString());
#endif
                us.Message = string.Format("Processing File {1} of {2}.\r\n{0}% Completed.\r\nEst. Time Remaining: {3}{4}{5}\r\n{6}",
                    (int)percent,
                    i,
                    count,
                    timeremaining.Hours > 0 ? timeremaining.Hours.ToString() + " hours" : "",
                    timeremaining.Minutes > 0 ? " " + timeremaining.Minutes.ToString() + " minutes" : "",
                    timeremaining.Seconds > 0 ? " " + timeremaining.Seconds.ToString() + " seconds" : "",
                    file);
#if debug
                Console.WriteLine("Reporting progress.");
#endif
                backgroundWorker2.ReportProgress((int)Math.Round(percent), us);
                i++;
//            }
            });
#if debug
            Console.WriteLine("Done with files.");
#endif
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
//            var us = (UserState)e.UserState;
//            BackgroundWorker b = sender as BackgroundWorker;
//            if (b.CancellationPending)
//            {
#if debug
                Console.WriteLine("BackgroundWorker2 ended.");
#endif
//            }
//            if (us.UpdateStatus == UpdateStatus.Success)
//            {
                pgd.progressBar1.Value = e.ProgressPercentage;
//                pgd.Label = us.Message;
#if debug
                Console.WriteLine("Adding {0} to listViewItems.", us.LVI.Text);
#endif
                //if (us.LVI != null)
                //    //listViewItems.Add(us.LVI);
                //    flowLayoutPanel1.Controls.Add(us.LVI);
#if debug
                //Console.WriteLine("Adding {0} to imageList.", us.Thumbnail.Size.ToString());
#endif
            //}
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
        }
        #endregion
    }
}
