/*
 * Created by SharpDevelop.
 * User: Dave
 * Date: 9/11/2016
 * Time: 1:15 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Drawing;
using System.Windows.Forms;

namespace WinFlickSharp
{
    /// <summary>
    /// An <see cref="UpdateStatus"/> providing choices for the status of this progress update.
    /// </summary>
    public enum UpdateStatus
    {
        /// <summary>
        /// The update was successful.
        /// </summary>
        Success,
        /// <summary>
        /// The update was not successful.
        /// </summary>
        Failure
    }

    /// <summary>
    /// This struct provides support for progress updating.
    /// </summary>
    public struct UserState
    {
        UpdateStatus updatestatus;
        /// <summary>
        /// Contains the <see cref="UpdateStatus"/> of this object.
        /// </summary>
        public UpdateStatus UpdateStatus
        {
            get
            {
                return updatestatus;
            }
            set
            {
                updatestatus = value;
            }
        }

        /// <summary>
        /// Contains the thumbnail of this progress update.
        /// </summary>
        public Bitmap Thumbnail { get; set; }

        FlickrPhotoPanel lvi;
        /// <summary>
        /// Contains the <see cref="ListViewItem"/> object of this progress update.
        /// </summary>
        public FlickrPhotoPanel LVI
        {
            get
            {
                return lvi;
            }
            set
            {
                lvi = value;
            }
        }

        /// <summary>
        /// Contains the error message of the exception.
        /// </summary>
        string message;
        /// <summary>
        /// Gets and sets the message <see cref="string"/> of the exception.
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
	}
}
