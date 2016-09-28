using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlickrNet;
using System.Windows.Forms;
using System.Drawing;

namespace WinFlickSharp
{
    public class FlickrPhotoPanel : PhotoPanel.PhotoPanel
    {
        string[] tags;
        [Category("Appearance")]
        [Description("The tags of the picture on the control.")]
        public string[] Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
            }
        }

        bool pub;
        [Category("Appearance")]
        [Description("Whether or not the picture is public.")]
        public bool Public
        {
            get
            {
                return pub;
            }
            set
            {
                pub = value;
            }
        }

        bool family;
        [Category("Appearance")]
        [Description("Whether or not the picture is visible by family.")]
        public bool Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
            }            
        }

        bool friends;
        [Category("Appearance")]
        [Description("Whether or not the picture is visible by friends.")]
        public bool Friends
        {
            get
            {
                return friends;
            }
            set
            {
                friends = value;
            }
        }

        ContentType type;
        [Category("Appearance")]
        [Description("The content type of the picture.")]
        public ContentType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        SafetyLevel level;
        [Category("Appearance")]
        [Description("The safety level of the picture.")]
        public SafetyLevel Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        HiddenFromSearch hidden;
        [Category("Appearance")]
        [Description("Whether or not the picture is hidden from search.")]
        public HiddenFromSearch Hidden
        {
            get
            {
                return hidden;
            }
            set
            {
                hidden = value;
            }
        }

        public FlickrPhotoPanel() : base()
        {
            Title = "";
            Description = "";
            FileName = "";
            FileSizeBytes = 0;
            thumbnail = new Bitmap(120, 120);
            tags = new string[]{ };
            pub = false;
            family = true;
            friends = true;
            type = ContentType.Photo;
            level = SafetyLevel.Safe;
            hidden = HiddenFromSearch.Hidden;
            IsSelected = false;
        }

        public FlickrPhotoPanel(string title, string desc, string file, string filebytes, Bitmap thumb, string[] intags, bool ispublic, bool isfamily, bool isfriends, ContentType intype, SafetyLevel inlevel, HiddenFromSearch inhidden, bool isselected) : base(title, desc, file, filebytes, thumb, isselected)
        {
            tags = intags;
            Public = ispublic;
            Family = isfamily;
            Friends = isfriends;
            type = intype;
            level = inlevel;
            hidden = inhidden;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var textFont = new Font("Arial", 9);
            var f = new SolidBrush(this.ForeColor);
            e.Graphics.DrawString(string.Join(";", Tags), textFont, f, 126, 58);
            var checkboxes = string.Format("{0}{1}{2}",
                pub ? "Public" : "Private",
                family ? "/Family" : "",
                friends ? "/Friends" : "");
            e.Graphics.DrawString(checkboxes, textFont, f, 126, 72);
            e.Graphics.DrawString(type.ToString(), textFont, f, 126, 86);
            e.Graphics.DrawString(level.ToString(), textFont, f, 126, 100);
            e.Graphics.DrawString(hidden.ToString(), textFont, f, 126, 114);
            
        }
    }
}
