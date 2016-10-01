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
        string[] tags;
        [Category("Misc")]
        [Description("The tags of the image on the control.")]
        public string[] Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
                OnPropertyChanged("Tags");
                OnToolTipChanged("Tags");
            }
        }

        bool ispublic;
        /// <summary>
        /// <see cref="bool"/> indicating whether or not the image is public.
        /// </summary>
        [Category("Misc")]
        [Description("Whether or not the image is public.")]
        public bool IsPublic
        {
            get
            {
                return ispublic;
            }
            set
            {
                ispublic = value;
                OnPropertyChanged("IsPublic");
                OnToolTipChanged("IsPublic");
            }
        }

        bool visibletofamily;
        /// <summary>
        /// <see cref="bool"/> indicating whether or not the image is visible to family.
        /// </summary>
        [Category("Misc")]
        [Description("Whether or not the image is visible to family.")]
        public bool VisibleToFamily
        {
            get
            {
                return visibletofamily;
            }
            set
            {
                visibletofamily = value;
                OnPropertyChanged("VisibleToFamily");
                OnToolTipChanged("VisibleToFamily");
            }            
        }

        bool visibletofriends;
        /// <summary>
        /// <see cref="bool"/> indicating whether or not the image is visible to friends.
        /// </summary>
        [Category("Misc")]
        [Description("Whether or not the image is visible to friends.")]
        public bool VisibleToFriends
        {
            get
            {
                return visibletofriends;
            }
            set
            {
                visibletofriends = value;
				OnPropertyChanged("VisibleToFriends");
				OnToolTipChanged("VisibleToFriends");
            }
        }

        ContentType contenttype;
        /// <summary>
        /// <see cref="ContentType"/> of the image.
        /// </summary>
        [Category("Misc")]
        [Description("The content type of the image.")]
        public ContentType ContentType
        {
            get
            {
                return contenttype;
            }
            set
            {
                contenttype = value;
                OnPropertyChanged("ContentType");
                OnToolTipChanged("ContentType");
			}
        }

        SafetyLevel safetylevel;
        /// <summary>
		/// <see cref="FlickrNet.SafetyLevel"/> of the image.
		/// </summary>
		[Category("Misc")]
        [Description("The safety level of the image.")]
        public SafetyLevel SafetyLevel
        {
            get
            {
                return safetylevel;
            }
            set
            {
                safetylevel = value;
				OnPropertyChanged("SafetyLevel");
				OnToolTipChanged("SafetyLevel");
            }
        }

        HiddenFromSearch hiddenfromsearch;
        /// <summary>
		/// <see cref="FlickrNet.HiddenFromSearch"/> indicating whether or not the image is hidden from search.
		/// </summary>
		[Category("Misc")]
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
				OnPropertyChanged("HiddenFromSearch");
				OnToolTipChanged("HiddenFromSearch");
            }
        }

        /// <summary>
		/// Constructs a new <see cref="FlickrPhotoPanel"/> with default properties.
		/// </summary>
		public FlickrPhotoPanel() : base("","","",0,null)
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
        }

		/// <summary>
		/// Constructs a new <see cref="FlickrPhotoPanel"/> with the supplied properties.
		/// </summary>
		/// <param name="title"><see cref="string"/> containing the title of the image.</param>
		/// <param name="desc"><see cref="string"/> containing the description of the image.</param>
		/// <param name="file"><see cref="string"/> containing the full path of the image file.</param>
		/// <param name="filebytes"><see cref="string"/> describing the file size.</param>
		/// <param name="thumb"><see cref="Bitmap"/> thumbnail of the image.</param>
		/// <param name="intags"><see cref="string[]"/> containing the tags associated with the image.</param>
		/// <param name="ispub"><see cref="bool"/> indicating whether or not the image is viewable by the public.</param>
		/// <param name="isfamily"><see cref="bool"/> indicating whether or not the image is viewable by family.</param>
		/// <param name="isfriends"><see cref="bool"/> indicating whether or not the image is viewable by friends.</param>
		/// <param name="intype"><see cref="FlickrNet.ContentType"/> of the image.</param>
		/// <param name="inlevel"><see cref="FlickrNet.SafetyLevel"/> of the iamge.</param>
		/// <param name="inhidden"><see cref="FlickrNet.HiddenFromSearch"/> indicating whether or not the image is hidden from search.</param>
		public FlickrPhotoPanel(string title, string desc, string file, string filebytes, Bitmap thumb, string[] intags, bool ispub, bool isfamily, bool isfriends, ContentType intype, SafetyLevel inlevel, HiddenFromSearch inhidden, bool isselected) : base(title, desc, file, filebytes, thumb)
        {
            tt = new ToolTip();
            tags = intags;
            ispublic = ispub;
            visibletofamily = isfamily;
            visibletofriends = isfriends;
            contenttype = intype;
            safetylevel = inlevel;
            hiddenfromsearch = inhidden;
		}

		protected override void SetToolTipText()
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(title)) sb.AppendLine(title);
			if (!string.IsNullOrEmpty(description)) sb.AppendLine(description);
			sb.AppendLine(filenameshort);
			sb.AppendLine(strfilesizebytes + " " + originalsize.Width + "x" + originalsize.Height);
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
			sb.AppendLine(isuploaded ? "Uploaded" : "Not Uploaded");
			tooltiptext = sb.ToString();
		}
	}
}
