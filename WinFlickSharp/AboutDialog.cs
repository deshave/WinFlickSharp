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
    public partial class AboutDialog : Form
    {
        /// <summary>
        /// Constructs a new <see cref="AboutDialog"/> object.
        /// </summary>
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
