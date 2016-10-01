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
using System.Drawing.Drawing2D;

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
                authenticateMenuItem.Enabled = authenticateToolStrip.Enabled = enterCodeMenuItem.Enabled = enterCodeToolStrip.Enabled = false;
				toolStripStatusLabel2.Image = imageList2.Images[1];
                toolStripStatusLabel2.ToolTipText = string.Format("You are authenticated as: {0}", FlickrManager.OAuthToken.FullName);
                this.Text = FormPreamble + " - " + FlickrManager.OAuthToken.FullName;
            }
            else
            {
                authenticated = false;
				authenticateMenuItem.Enabled = authenticateToolStrip.Enabled = flickrMenuItem.Enabled = true;
				toolStripStatusLabel2.Image = imageList2.Images[0];
                toolStripStatusLabel2.ToolTipText = "You are not authenticated.";
                this.Text = FormPreamble + " - Not Authenticated";
            }
        }
        #endregion

#region Private Methods
        private void UpdateStatusLabel()
        {
            string status = string.Format("{0} item{1}", 
				itemcount, 
				itemcount != 1 ? "s" : "");
			if (selectedcount != 0)
			{
				status += string.Format("     {0} item{1} selected     {2}",
					selectedcount,
					selectedcount != 1 ? "s" : "",
					StringUtilities.GetBytesReadable(selectedbytes));
				uploadSelectedMenuItem.Enabled = uploadSelectedToolStrip.Enabled = clearSelectedMenuItem.Enabled = clearSelectedToolStrip.Enabled = true;
			}
			else
			{
				uploadSelectedMenuItem.Enabled = uploadSelectedToolStrip.Enabled = clearSelectedMenuItem.Enabled = clearSelectedToolStrip.Enabled = false;
			}
			if (itemcount == 0)
			{
				imagesMenuItem.Enabled = flickrMenuItem.Enabled = toolStripDropDownButtonImages.Enabled = toolStripDropDownButtonFlickr.Enabled = selectAllMenuItem.Enabled = selectAllToolStrip.Enabled = clearAllMenuItem.Enabled = clearAllToolStrip.Enabled = uploadAllMenuItem.Enabled = uploadAllToolStrip.Enabled = regenerateThumbnailsMenuItem.Enabled = regenerateThumbnailsToolStrip.Enabled = false;
			}
			else
			{
				imagesMenuItem.Enabled = flickrMenuItem.Enabled = toolStripDropDownButtonImages.Enabled = toolStripDropDownButtonFlickr.Enabled = selectAllMenuItem.Enabled = selectAllToolStrip.Enabled = clearAllMenuItem.Enabled = clearAllToolStrip.Enabled = uploadAllMenuItem.Enabled = uploadAllToolStrip.Enabled = regenerateThumbnailsMenuItem.Enabled = regenerateThumbnailsToolStrip.Enabled = true;
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
            if (generateThumbsWorker.IsBusy)
            {
                generateThumbsWorker.CancelAsync();
            }
            flowLayoutPanel1.Controls.Clear();
			itemcount = 0;
			selectedcount = 0;
			selectedbytes = 0;
			ClearForm();
            populateWorker.RunWorkerAsync(files);
        }

        private int[] GetSelectedIndices(FlowLayoutPanel flow, bool excludeuploaded = false)
        {
            List<int> indices = new List<int>();
            int i = 0;
            foreach (var item in flow.Controls)
            {
                var fpp = (FlickrPhotoPanel)item;
                if (fpp.IsSelected)
                {
					if (excludeuploaded && fpp.IsUploaded)
					{
						continue;
					}
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
            }
            selectedcount = 0;
            selectedbytes = 0;
			UpdateStatusLabel();
        }

        private void UnselectAllExcept(FlickrPhotoPanel fpp)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var lvi = (FlickrPhotoPanel)item;
                if (lvi != fpp)
                {
                    lvi.IsSelected = false;
                }
            }
            selectedcount = 1;
            selectedbytes = fpp.FileSizeBytes;
			UpdateStatusLabel();
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
                var accessToken = f.OAuthGetAccessToken(requestToken, enterCodeMenuItem.Text);
                FlickrManager.OAuthToken = accessToken;
                Text = accessToken.FullName;
                authWindow.Close();
				enterCodeMenuItem.Enabled = enterCodeToolStrip.Enabled = false;
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
		private void chooseFilesMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            var di = new DirectoryInfo(openFileDialog1.FileNames[0]);
            toolStripTextBoxLocation.Text = di.FullName;
            ProcessFiles(openFileDialog1.FileNames);
        }

        private void chooseFolderMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;

            toolStripTextBoxLocation.Text = folderBrowserDialog1.SelectedPath;
            ProcessFolder(folderBrowserDialog1.SelectedPath);
        }
        #endregion

        #region Toolbar
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void authenticateMenuItem_Click(object sender, EventArgs e)
        {
            if (FlickrManager.OAuthToken == null || FlickrManager.OAuthToken.Token == null)
            {
                authenticateMenuItem.Enabled = false;
                Flickr f = FlickrManager.GetInstance();
                requestToken = f.OAuthGetRequestToken("oob");

                string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write);
                authWindow = new AuthBrowser();
                authWindow.webControl1.Source = new Uri(url);
                authWindow.Show();
                enterCodeMenuItem.Enabled = enterCodeToolStrip.Enabled = true;
            }
            else
            {
                MessageBox.Show("You have already authenticated.");
            }
        }

		private void uploadSelectedMenuItem_Click(object sender, EventArgs e)
		{
			uploadSelectedMenuItem.Enabled = uploadAllMenuItem.Enabled = uploadSelectedToolStrip.Enabled = uploadAllToolStrip.Enabled = false;
			uploadWorker.RunWorkerAsync();
			pgd = new ProgressDialog();
			pgd.progressBar1.Style = ProgressBarStyle.Blocks;
			pgd.Text = "Uploading images. This could take a while...";
			if (DialogResult.Cancel == pgd.ShowDialog())
			{
				uploadWorker.CancelAsync();
			}
		}

		private void uploadAllMenuItem_Click(object sender, EventArgs e)
        {
			SelectAll();
			uploadSelectedMenuItem_Click(sender, e);
        }

        private void enterCodeMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Authenticate();
            }
        }

        private void regenerateThumbnailMenuItem_Click(object sender, EventArgs e)
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
                }
            }
        }

        private void clearSelectedMenuItem_Click(object sender, EventArgs e)
        {
            List<FlickrPhotoPanel> deleted = new List<FlickrPhotoPanel>();
			int[] indices = GetSelectedIndices(flowLayoutPanel1);
			long bytesremoved = 0;
			foreach (var index in indices)
            {
                var fpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[index];
                deleted.Add(fpp);
            }
            foreach (var item in deleted)
            {
				bytesremoved += item.FileSizeBytes;
				flowLayoutPanel1.Controls.Remove(item);
                item.Dispose();
            }
			itemcount -= deleted.Count;
			selectedcount -= deleted.Count;
			selectedbytes -= bytesremoved;
			ClearForm();
			UpdateStatusLabel();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            var ad = new AboutDialog();
            ad.ShowDialog();
        }

        private void clearAllMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var fpp = (FlickrPhotoPanel)item;
                fpp.Dispose();
            }
            GC.Collect();
            flowLayoutPanel1.Controls.Clear();
			itemcount = 0;
			selectedcount = 0;
			selectedbytes = 0;
			ClearForm();
			UpdateStatusLabel();
        }

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
			UpdateStatusLabel();
        }
        #endregion

        #region Form event handlers
        private void textBoxTitle_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Title = textBoxTitle.Text;
        }

        private void textBoxDescription_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Description = textBoxDescription.Text;
        }

        private void textBoxTags_Leave(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.Tags = textBoxTags.Text.Split(';');
        }

        private void checkBoxPublic_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.IsPublic = checkBoxPublic.Checked;
        }

        private void checkBoxFamily_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.VisibleToFamily = checkBoxFamily.Checked;
        }

        private void checkBoxFriends_CheckedChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            lvi.VisibleToFriends = checkBoxFriends.Checked;
        }

        private void comboBoxContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            ContentType cresult;
            if (Enum.TryParse(comboBoxContentType.Text, out cresult))
            lvi.ContentType = cresult;
        }

        private void comboBoxSafetyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            SafetyLevel cresult;
            if (Enum.TryParse(comboBoxSafetyLevel.Text, out cresult))
                lvi.SafetyLevel = cresult;
        }

        private void comboBoxHiddenFromSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lvi = GetFirstSelectedFlickrPhotoPanel();
            if (lvi == null) return;
            HiddenFromSearch cresult;
            if (Enum.TryParse(comboBoxHiddenFromSearch.Text, out cresult))
                lvi.HiddenFromSearch = cresult;
        }

        private void toolStripTextBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessFolder(toolStripTextBoxLocation.Text);
            }
        }
        #endregion

        #region Form
        private void Form1_Load(object sender, EventArgs e)
        {
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
                        selectAllMenuItem_Click(sender, e);
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
                UnselectAllExcept(fpp);
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
                    UnselectAllExcept(fpp);
                    var nextfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[next];
                    fpp.IsSelected = false;
                    nextfpp.IsSelected = true;
                    FillOutForm(nextfpp);
                    flowLayoutPanel1.ScrollControlIntoView(nextfpp);
                    break;
                case Keys.Left:
                    int lindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    int prev = lindex - 1 >= 0 ? lindex - 1 : flowLayoutPanel1.Controls.Count - 1;
                    var lfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[lindex];
                    UnselectAllExcept(lfpp);
                    var prevfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[prev];
                    lfpp.IsSelected = false;
                    prevfpp.IsSelected = true;
                    FillOutForm(prevfpp);
                    flowLayoutPanel1.ScrollControlIntoView(prevfpp);
                    break;
                case Keys.Up:
                    int uindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    var ufpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[uindex];
                    UnselectAllExcept(ufpp);
                    int columns = flowLayoutPanel1.Width / ufpp.Width;
                    int max = flowLayoutPanel1.Controls.Count - 1;
                    int uprev = uindex - columns >= 0 ? uindex - columns : max - (max % columns) + uindex;
                    if (uprev > max)
                    {
                        uprev -= columns;
                    }
                    var prevufpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[uprev];
                    ufpp.IsSelected = false;
                    prevufpp.IsSelected = true;
                    flowLayoutPanel1.ScrollControlIntoView(prevufpp);
                    break;
                case Keys.Down:
                    int dindex = GetSelectedIndices(flowLayoutPanel1)[0];
                    var dfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[dindex];
                    UnselectAllExcept(dfpp);
                    int dcolumns = flowLayoutPanel1.Width / dfpp.Width;
                    int dprev = dindex + dcolumns < flowLayoutPanel1.Controls.Count ? dindex + dcolumns : dindex % dcolumns;
                    var prevdfpp = (FlickrPhotoPanel)flowLayoutPanel1.Controls[dprev];
                    dfpp.IsSelected = false;
                    prevdfpp.IsSelected = true;
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
			BackgroundWorker b = sender as BackgroundWorker;
			decimal i = 1;
			int[] indices = GetSelectedIndices(flowLayoutPanel1, true);
			decimal count = indices.Length;
			var starttime = DateTime.Now;
			Parallel.ForEach(indices, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (index) =>
			{
				if (b.CancellationPending)
				{
					return;
				}
				FlickrPhotoPanel lv = (FlickrPhotoPanel)flowLayoutPanel1.Controls[index];
				var us = new UserState();
				us.LVI = lv;
				string photoId = f.UploadPicture(lv.FileName,
					lv.Title,
					lv.Description,
					string.Join(";", lv.Tags),
					lv.IsPublic,
					lv.VisibleToFamily,
					lv.VisibleToFriends);
				us.UpdateStatus = UpdateStatus.Success;
				int percent = (int)Math.Round((i / count) * 100m);
				decimal itemsremaining = count - i;
				var ticksperitem = DateTime.Now.Subtract(starttime).Ticks / i;
				var ticksremaining = ticksperitem * itemsremaining;
				var timeremaining = new TimeSpan((long)ticksremaining);
				us.Message = string.Format("Uploading image {1} of {2}.\r\n{0}% Completed.\r\nEst. Time Remaining: {3}{4}{5}\r\n{6}",
					(int)percent,
					i,
					count,
					timeremaining.Hours > 0 ? timeremaining.Hours.ToString() + " hours" : "",
					timeremaining.Minutes > 0 ? " " + timeremaining.Minutes.ToString() + " minutes" : "",
					timeremaining.Seconds > 0 ? " " + timeremaining.Seconds.ToString() + " seconds" : "",
					lv.FileName);
				uploadWorker.ReportProgress(percent, us);
				i++;
			});
        }

        private void uploadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
			pgd.Close();
            UnselectAll();
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Ready.";
            uploadAllMenuItem.Enabled = uploadAllToolStrip.Enabled = true;
        }

        private void uploadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
			var us = (UserState)e.UserState;
			var lvi = us.LVI;
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
			lvi.IsUploaded = true;
			lvi.Invalidate();
        }

        #endregion

#region Populate List BackgroundWorker
        private void populateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;
            BackgroundWorker b = sender as BackgroundWorker;
            decimal i = 1;
            decimal count = filenames.Length;
            var starttime = DateTime.Now;
            listViewItems.Clear();
            Parallel.ForEach(filenames, new ParallelOptions { MaxDegreeOfParallelism = 1 }, (file) =>
            {
                if (b.CancellationPending)
                {
                    return;
                }
				var us = new UserState();
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
                us.Message = string.Format("Processing file {1} of {2}.  {0}%   Est. Time Remaining: {3}{4}{5}",
                    (int)percent,
                    i,
                    count,
                    timeremaining.Hours > 0 ? timeremaining.Hours.ToString() + " hours" : "",
                    timeremaining.Minutes > 0 ? " " + timeremaining.Minutes.ToString() + " minutes" : "",
                    timeremaining.Seconds > 0 ? " " + timeremaining.Seconds.ToString() + " seconds" : "");
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
				//                pgd.progressBar1.Value = e.ProgressPercentage;
				//                pgd.Label = us.Message;
				toolStripProgressBar1.Value = e.ProgressPercentage;
				toolStripStatusLabel1.Text = us.Message;
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
                uploadSelectedMenuItem.Enabled = uploadAllMenuItem.Enabled = uploadSelectedToolStrip.Enabled = uploadAllToolStrip.Enabled = true;
            }
            itemcount = flowLayoutPanel1.Controls.Count;
            UpdateStatusLabel();
            generateThumbsWorker.RunWorkerAsync();
        }
        #endregion

#region Generate Thumbnails Worker
        private void GenerateThumbsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
			var b = sender as BackgroundWorker;
			decimal i = 1;
			decimal count = flowLayoutPanel1.Controls.Count;
			var starttime = DateTime.Now;
			Parallel.ForEach(flowLayoutPanel1.Controls.Cast<Control>(), 
				new ParallelOptions { MaxDegreeOfParallelism = 1 }, 
				(item) =>
            {
				if (b.CancellationPending)
				{
					return;
				}
                var fpp = (FlickrPhotoPanel)item;
				var us = new UserState();
				us.LVI = fpp;
                using (var bm = Image.FromFile(fpp.FileName))
                {
                    us.Thumbnail = GraphicsUtilities.ResizeImage(bm, 120, 120, Color.Black, true);
                    us.OriginalSize = new System.Drawing.Size(bm.Width, bm.Height);
                }
				decimal percent = (i / count) * 100m;
				decimal itemsremaining = count - i;
				var ticksperitem = DateTime.Now.Subtract(starttime).Ticks / i;
				var ticksremaining = ticksperitem * itemsremaining;
				var timeremaining = new TimeSpan((long)ticksremaining);
				us.Message = string.Format("Generating thumbnail {1} of {2}  {0}%  Est. Time Remaining:{3}{4}{5}. Click to stop.",
					(int)percent,
					i,
					count,
					timeremaining.Hours > 0 ? timeremaining.Hours.ToString() + " hours" : "",
					timeremaining.Minutes > 0 ? " " + timeremaining.Minutes.ToString() + " minutes" : "",
					timeremaining.Seconds > 0 ? " " + timeremaining.Seconds.ToString() + " seconds" : "");
				generateThumbsWorker.ReportProgress((int)Math.Round(percent), us);
				i++;
            });
        }

        private void GenerateThumbsWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var us = (UserState)e.UserState;
            var lvi = us.LVI;
            lvi.Thumbnail = us.Thumbnail;
            lvi.OriginalSize = us.OriginalSize;
			toolStripProgressBar1.Value = e.ProgressPercentage;
			toolStripStatusLabel1.Text = us.Message;
		}

		private void GenerateThumbsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			toolStripProgressBar1.Value = 0;
			toolStripStatusLabel1.Text = "Ready.";
		}
		#endregion

		private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{
			generateThumbsWorker.CancelAsync();
		}

		private void regenerateThumbnailsMenuItem_Click(object sender, EventArgs e)
		{
			generateThumbsWorker.RunWorkerAsync();
		}

		private void toolStripStatusLabel1_MouseEnter(object sender, EventArgs e)
		{
			if (generateThumbsWorker.IsBusy)
			{
				statusStrip1.Cursor = Cursors.Hand;
			}
		}

		private void toolStripStatusLabel1_MouseLeave(object sender, EventArgs e)
		{
			statusStrip1.Cursor = Cursors.Default;
		}
	}
}
