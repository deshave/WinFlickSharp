using System;
using System.ComponentModel;
using FlickrNet;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace PhotoPanel
{
    public class FlickrPhotoPanel : PhotoPanel
    {
        private ToolTip tt;
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
                RefreshToolTip();
            }
        }

        bool ispublic;
        [Category("Appearance")]
        [Description("Whether or not the picture is public.")]
        public bool IsPublic
        {
            get
            {
                return ispublic;
            }
            set
            {
                ispublic = value;
                RefreshToolTip();
            }
        }

        bool visibletofamily;
        [Category("Appearance")]
        [Description("Whether or not the picture is visible by family.")]
        public bool VisibleToFamily
        {
            get
            {
                return visibletofamily;
            }
            set
            {
                visibletofamily = value;
                RefreshToolTip();
            }            
        }

        bool visibletofriends;
        [Category("Appearance")]
        [Description("Whether or not the picture is visible by friends.")]
        public bool VisibleToFriends
        {
            get
            {
                return visibletofriends;
            }
            set
            {
                visibletofriends = value;
                RefreshToolTip();
            }
        }

        ContentType contenttype;
        [Category("Appearance")]
        [Description("The content type of the picture.")]
        public ContentType ContentType
        {
            get
            {
                return contenttype;
            }
            set
            {
                contenttype = value;
                RefreshToolTip();
            }
        }

        SafetyLevel safetylevel;
        [Category("Appearance")]
        [Description("The safety level of the picture.")]
        public SafetyLevel SafetyLevel
        {
            get
            {
                return safetylevel;
            }
            set
            {
                safetylevel = value;
                RefreshToolTip();
            }
        }

        HiddenFromSearch hiddenfromsearch;
        [Category("Appearance")]
        [Description("Whether or not the picture is hidden from search.")]
        public HiddenFromSearch HiddenFromSearch
        {
            get
            {
                return hiddenfromsearch;
            }
            set
            {
                hiddenfromsearch = value;
                RefreshToolTip();
            }
        }

        public FlickrPhotoPanel() : base("","","",0,null,false)
        {
            tt = new ToolTip();
            title = "";
            description = "";
            filename = "";
            filesizebytes = 0;
            thumbnail = new Bitmap(120, 120);
            tags = new string[]{ };
            ispublic = false;
            visibletofamily = true;
            visibletofriends = true;
            contenttype = ContentType.Photo;
            safetylevel = SafetyLevel.Safe;
            hiddenfromsearch = HiddenFromSearch.Hidden;
            isselected = false;
            RefreshToolTip();
        }

        public FlickrPhotoPanel(string title, string desc, string file, string filebytes, Bitmap thumb, string[] intags, bool ispub, bool isfamily, bool isfriends, ContentType intype, SafetyLevel inlevel, HiddenFromSearch inhidden, bool isselected) : base(title, desc, file, filebytes, thumb, isselected)
        {
            tt = new ToolTip();
            tags = intags;
            ispublic = ispub;
            visibletofamily = isfamily;
            visibletofriends = isfriends;
            contenttype = intype;
            safetylevel = inlevel;
            hiddenfromsearch = inhidden;
            RefreshToolTip();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var textFont = new Font("Arial", 9);
            var f = new SolidBrush(this.ForeColor);
            e.Graphics.DrawString(string.Join(";", Tags), textFont, f, 126, 58);
            var checkboxes = string.Format("{0}{1}{2}",
                ispublic ? "Public" : "Private",
                visibletofamily ? "/Family" : "",
                visibletofriends ? "/Friends" : "");
            e.Graphics.DrawString(checkboxes, textFont, f, 126, 72);
            e.Graphics.DrawString(contenttype.ToString(), textFont, f, 126, 86);
            e.Graphics.DrawString(safetylevel.ToString(), textFont, f, 126, 100);
            e.Graphics.DrawString(hiddenfromsearch.ToString(), textFont, f, 126, 113);            
        }

        protected override void RefreshToolTip()
        {
            var sb = new StringBuilder();
            if (title != "")
            {
                sb.AppendLine(title);
            }
            if (Description != "")
            {
                sb.AppendLine(description);
            }
            sb.AppendLine(filename);
            if (strfilesizebytes != "")
            {
                sb.AppendLine(strfilesizebytes);
            }
            if (tags != null && tags.Length != 0)
            {
                sb.AppendLine(string.Join(";", tags));
            }
            sb.Append(ispublic ? "Public" : "Private");
            sb.Append(visibletofamily ? "/Family" : "");
            sb.AppendLine(visibletofriends ? "/Friends" : "");
            sb.AppendLine(contenttype.ToString());
            sb.AppendLine(safetylevel.ToString());
            sb.AppendLine(hiddenfromsearch.ToString());
            tt.SetToolTip(this, sb.ToString());
        }
    }
}
