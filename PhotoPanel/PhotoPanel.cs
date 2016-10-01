using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Helpers;
using System;
using System.Text;

namespace PhotoPanel
{
    /// <summary>
    /// This class provides support for a custom windows forms control that displays photo thumbnails and
    /// associated information, mainly for use in a <see cref="FlowLayoutPanel"/>.
    /// </summary>
    public partial class PhotoPanel : Control , INotifyPropertyChanged
    {
		protected ToolTip tt;
		protected string tooltiptext;
		/// <summary>
		/// This <see cref="Delegate"/> provides support for notifying consumers that the tool tip text
		/// for this control has changed.
		/// </summary>
		/// <param name="sender"><see cref="object"/> sending the event.</param>
		/// <param name="e"><see cref="PropertyChangedEventArgs"/> of the event.</param>
		public delegate void ToolTipChangedEventHandler(object sender, PropertyChangedEventArgs e);
        /// <summary>
        /// Notifies registered consumers that a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies registered consumers that the tool tip has changed.
        /// </summary>
        public event ToolTipChangedEventHandler ToolTipChanged;

        protected Size originalsize;
    	/// <summary>
        /// The <see cref="Size"/> of the original image used for the thumbnail.
        /// </summary>
        [Category("Misc")]
    	[Description("The size of the original image used to create the thumbnail.")]
    	public Size OriginalSize
    	{
    		get
    		{
    			return originalsize;
    		}
    		set
    		{
    			originalsize = value;
                OnPropertyChanged("OriginalSize");
    		}
    	}

        protected Bitmap thumbnail;
        /// <summary>
        /// The <see cref="Bitmap"/> to draw on the control as a thumbnail.
        /// </summary>
        [Category("Appearance")]
        [Description("The bitmap to draw as a thumbnail on the control.")]
        public Bitmap Thumbnail
        {
            get
            {
                return thumbnail;
            }
            set
            {
                thumbnail = value;
                OnPropertyChanged("Thumbnail");
            }
        }

        protected string filenameshort;
        protected string filename;
        /// <summary>
        /// <see cref="string"/> containing the file name of the image on the control.
        /// </summary>
        [Category("Misc")]
        [Description("The file name of the picture on the control.")]
        public string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                if (value.Length > 20)
                {
                    filenameshort = value.Substring(0, 8) + "..." + value.Substring(value.Length - 8, 8);
                }
                else
                {
                    filenameshort = value;
                }
                filename = value;
                OnPropertyChanged("FileName");
                OnToolTipChanged("FileName");
            }
        }

        protected string title;
        /// <summary>
        /// <see cref="string"/> containing the title of the image on the control.
        /// </summary>
        [Category("Misc")]
        [Description("The title to draw on the control.")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
                OnToolTipChanged("Title");
            }
        }

        protected string description;
        /// <summary>
        /// <see cref="string"/> containing the description of the image on the control.
        /// </summary>
        [Category("Misc")]
        [Description("The description to draw on the control.")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
                OnToolTipChanged("Description");
            }
        }

        protected bool isselected = false;
        [Category("Appearance")]
        [Description("Whether or not the control is selected.")]
        /// <summary>
        /// <see cref="bool"/> indicating whether or not the control is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isselected;
            }
            set
            {
                isselected = value;
                OnPropertyChanged("IsSelected");
            }
        }

		protected bool isuploaded = false;
		[Category("Appearance")]
		[Description("Whether or not the image is uploaded.")]
		/// <summary>
		/// <see cref="bool"/> indicating whether or not the image is uploaded.
		/// </summary>
		public bool IsUploaded
		{
			get
			{
				return isuploaded;
			}
			set
			{
				isuploaded = value;
				OnPropertyChanged("IsUploaded");
				OnToolTipChanged("IsUploaded");
			}
		}

		protected string strfilesizebytes;
        [Category("Misc")]
        [Description("The size of the image.")]
        /// <summary>
        /// <see cref="string"/> containing the size of the image file.
        /// </summary>
        public string StringFileSizeBytes
        {
            get
            {
                return strfilesizebytes;
            }
            set
            {
                strfilesizebytes = value;
                filesizebytes = StringUtilities.GetBytesFromReadable(strfilesizebytes);
                OnPropertyChanged("StringFileSizeBytes");
                OnToolTipChanged("StringFileSizeBytes");
            }
        }

        protected long filesizebytes;

        [Category("Misc")]
        [Description("The size of the image file in bytes.")]
        /// <summary>
        /// <see cref="long"/> containing the size of the image file in bytes.
        /// </summary>
        public long FileSizeBytes
        {
            get
            {
                return filesizebytes;
            }
            set
            {
                filesizebytes = value;
                strfilesizebytes = StringUtilities.GetBytesReadable(value);
                OnPropertyChanged("FileSizeBytes");
                OnToolTipChanged("FileSizeBytes");
            }
        }


        protected override Size DefaultMaximumSize
        {
            get
            {
                return new Size(255, 130);
            }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(255, 130);
            }
        }

        /// <summary>
        /// Constructs a new instance of <see cref="PhotoPanel"/> with default properties.
        /// </summary>
        public PhotoPanel()
        {
			tt = new ToolTip();
			thumbnail = new Bitmap(120, 120);
            title = "Title";
            description = "Description";
            filename = "filename.jpg";
            filenameshort = "filename.jpg";
            filesizebytes = 0;
			//SetToolTipText();
        }

        /// <summary>
        /// Constructs a new instance of <see cref="PhotoPanel"/> with the supplied properties.
        /// </summary>
        /// <param name="intitle"><see cref="string"/> containing the title of the image.</param>
        /// <param name="desc"><see cref="string"/> containing the description of the image.</param>
        /// <param name="file"><see cref="string"/> containing the full path of the image file.</param>
        /// <param name="filebytes"><see cref="string"/> describing the file size.</param>
        /// <param name="thumb"><see cref="Bitmap"/> thumbnail of the image.</param>
        public PhotoPanel(string intitle, string desc, string file, string filebytes, Bitmap thumb)
        {
			tt = new ToolTip();
			title = intitle;
            description = desc;
            filename = file;
            strfilesizebytes = filebytes;
            thumbnail = thumb;
			//SetToolTipText();
        }

        /// <summary>
        /// Constructs a new instance of <see cref="PhotoPanel"/> with the supplied properties.
        /// </summary>
        /// <param name="intitle"><see cref="string"/> containing the title of the image.</param>
        /// <param name="desc"><see cref="string"/> containing the description of the image.</param>
        /// <param name="file"><see cref="string"/> containing the full path of the image file.</param>
        /// <param name="filebytes"><see cref="long"/> containing the file size in bytes.</param>
        /// <param name="thumb"><see cref="Bitmap"/> thumbnail of the image.</param>
        public PhotoPanel(string intitle, string desc, string file, long filebytes, Bitmap thumb)
        {
			tt = new ToolTip();
			title = intitle;
            description = desc;
            filename = file;
            filesizebytes = filebytes;
            thumbnail = thumb;
			//SetToolTipText();
        }

		protected virtual void SetToolTipText()
		{
			Console.WriteLine("Base:SetToolTipText()");
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(title)) sb.AppendLine(title);
			if (!string.IsNullOrEmpty(description)) sb.AppendLine(description);
			sb.AppendLine(filenameshort);
			sb.AppendLine(originalsize.Width + "x" + originalsize.Height);
			sb.AppendLine(isuploaded ? "Uploaded" : "Not Uploaded");
			tooltiptext = sb.ToString();
		}

		protected override void OnPaint(PaintEventArgs e)
        {
			var font = this.Font;
			var titleFont = new Font(font.FontFamily.Name, font.Size + 2, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
			var foregroundbrush = new SolidBrush(this.ForeColor);
            var backgroundbrush = new SolidBrush(this.BackColor);
            base.OnPaint(e);
            if (isselected)
            {
                e.Graphics.FillRectangle(foregroundbrush, 0, 0, Width, Height);
                e.Graphics.FillRectangle(backgroundbrush, 2, 2, Width - 4, Height - 4);
            }
            e.Graphics.DrawImage(thumbnail, 5, 5, 120, 120);
			if (isuploaded)
			{
				e.Graphics.DrawString("▲", this.Font, new SolidBrush(Color.Green), 111, 5);
			}
			else
			{
				e.Graphics.DrawString("▲", this.Font, new SolidBrush(Color.Red), 111, 5);
			}
            e.Graphics.DrawString(tooltiptext, this.Font, foregroundbrush, 125, 2);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
			this.Invalidate();
        }

        protected virtual void OnToolTipChanged(string name)
        {
			SetToolTipText();
			tt.SetToolTip(this, tooltiptext);
			ToolTipChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
	}
}
