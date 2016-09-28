using System.Windows.Forms;

namespace WinFlickSharp
{
    /// <summary>
    /// This partial class extends <see cref="Form"/> to provide a dialog to inform the user of the progress of a long operation.
    /// </summary>
    public partial class ProgressDialog : Form
    {
        /// <summary>
        /// Constructs a new <see cref="ProgressDialog"/>.
        /// </summary>
        public ProgressDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays the string on the dialog to inform the user of progress.
        /// </summary>
        public string Label
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private void buttonStop_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
