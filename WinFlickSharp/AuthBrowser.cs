using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFlickSharp
{
    /// <summary>
    /// This class provides support for authenticating with Flickr's web site.
    /// </summary>
    public partial class AuthBrowser : Form
    {
        /// <summary>
        /// <see cref="Uri"/> containing the location of the remote resource.
        /// </summary>
        public Uri Url
        {
            get { return Url; }
            set { Url = value; }
        }

        /// <summary>
        /// Constructs a new <see cref="AuthBrowser"/> object.
        /// </summary>
        public AuthBrowser()
        {
            InitializeComponent();
        }
    }
}
