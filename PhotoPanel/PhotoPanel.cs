using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Helpers;

namespace PhotoPanel
{
    public abstract partial class PhotoPanel : Control
    {
        protected Bitmap thumbnail;
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
                RefreshToolTip();
            }
        }
        protected string filenameshort;
        protected string filename;
        [Category("Appearance")]
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
                RefreshToolTip();
            }
        }

        protected string title;
        [Category("Appearance")]
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
                RefreshToolTip();
            }
        }

        protected string description;
        [Category("Appearance")]
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
                RefreshToolTip();
            }
        }

        protected bool isselected;
        public bool IsSelected
        {
            get
            {
                return isselected;
            }
            set
            {
                isselected = value;
            }
        }

        protected string strfilesizebytes;
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
                RefreshToolTip();
            }
        }

        protected long filesizebytes;
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
                RefreshToolTip();
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

        public PhotoPanel()
        {
            thumbnail = new Bitmap(120, 120);
            title = "Title";
            description = "Description";
            filename = "";
            filenameshort = "";
            filesizebytes = 0;
        }

        public PhotoPanel(string intitle, string desc, string file, string filebytes, Bitmap thumb, bool selected)
        {
            title = intitle;
            description = desc;
            filename = file;
            strfilesizebytes = filebytes;
            thumbnail = thumb;
            isselected = selected;
        }

        public PhotoPanel(string intitle, string desc, string file, long filebytes, Bitmap thumb, bool selected)
        {
            title = intitle;
            description = desc;
            filename = file;
            filesizebytes = filebytes;
            thumbnail = thumb;
            isselected = selected;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var titleFont = new Font("Arial", 11);
            var textFont = new Font("Arial", 9);
            var textColor = new SolidBrush(this.ForeColor);
            var b = new SolidBrush(this.BackColor);
            base.OnPaint(e);
            if (IsSelected)
            {
                e.Graphics.FillRectangle(textColor, 0, 0, Width, Height);
                e.Graphics.FillRectangle(b, 2, 2, Width - 4, Height - 4);
            }
            e.Graphics.DrawImage(thumbnail, 5, 5, 120, 120);
            e.Graphics.DrawString(Title, titleFont, textColor, 125, 2);
            e.Graphics.DrawString(Description, textFont, textColor, 126, 16);
            e.Graphics.DrawString(filenameshort, textFont, textColor, 126, 30);
            e.Graphics.DrawString(strfilesizebytes, textFont, textColor, 126, 44);
        }

        protected abstract void RefreshToolTip();
    }
}
