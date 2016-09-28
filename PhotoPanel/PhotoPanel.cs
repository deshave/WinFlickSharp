using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Helpers;

namespace PhotoPanel
{
    public partial class PhotoPanel : Control
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
            }
        }
        string filenameshort;
        string filename;
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
            }
        }

        [Category("Appearance")]
        [Description("The title to draw on the control.")]
        public string Title { get; set; }

        [Category("Appearance")]
        [Description("The description to draw on the control.")]
        public string Description { get; set; }

        public bool IsSelected
        {
            get; set;
        }

        string strfilesizebytes;
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
            }
        }

        long filesizebytes;
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
            }
        }


        protected override Size DefaultMaximumSize
        {
            get
            {
                return new Size(250, 130);
            }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(250, 130);
            }
        }

        public PhotoPanel()
        {
            Thumbnail = new Bitmap(120, 120);
            Title = "Title";
            Description = "Description";
            filename = "";
            filenameshort = "";
            filesizebytes = 0;
        }

        public PhotoPanel(string title, string desc, string file, string filebytes, Bitmap thumb, bool isselected)
        {
            Title = title;
            Description = desc;
            filename = file;
            strfilesizebytes = filebytes;
            thumbnail = thumb;
            IsSelected = isselected;
        }

        public PhotoPanel(string title, string desc, string file, long filebytes, Bitmap thumb, bool isselected)
        {
            Title = title;
            Description = desc;
            filename = file;
            filesizebytes = filebytes;
            thumbnail = thumb;
            IsSelected = isselected;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var titleFont = new Font("Arial", 11);
            var textFont = new Font("Arial", 9);
            var b = new SolidBrush(this.BackColor);
            var r = new SolidBrush(this.ForeColor);
            base.OnPaint(e);
            if (IsSelected)
            {
                e.Graphics.FillRectangle(r, 0, 0, Width, Height);
                e.Graphics.FillRectangle(b, 2, 2, Width - 4, Height - 4);
            }
            e.Graphics.DrawImage(thumbnail, 5, 5, 120, 120);
            e.Graphics.DrawString(Title, titleFont, r, 126, 1);
            e.Graphics.DrawString(Description, textFont, r, 126, 16);
            e.Graphics.DrawString(filenameshort, textFont, r, 126, 30);
            e.Graphics.DrawString(strfilesizebytes, textFont, r, 126, 44);
        }
    }
}
