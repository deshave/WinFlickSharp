using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testbed
{
	public partial class Form1 : Form
	{
		public event PropertyChangedEventHandler Progress;
		public Form1()
		{
			InitializeComponent();
		}

		protected void OnProgress(object sender, EventArgs e)
		{
			int percent = (int)(((double)(toolStripProgressBar1.Value - toolStripProgressBar1.Minimum) /	(double)(toolStripProgressBar1.Maximum - toolStripProgressBar1.Minimum)) * 100);
			using (Graphics gr = toolStripProgressBar1.CreateGraphics())
			{
				gr.DrawString(percent.ToString() + "%", SystemFonts.DefaultFont, Brushes.Black, new PointF(toolStripProgressBar1.Width / 2 - (gr.MeasureString(percent.ToString() + "%", SystemFonts.DefaultFont).Width / 2.0F), toolStripProgressBar1.Height / 2 - (gr.MeasureString(percent.ToString() + "%", SystemFonts.DefaultFont).Height / 2.0F)));
			}
		}
	}
}
