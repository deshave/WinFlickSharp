﻿namespace WinFlickSharp
{
    partial class AuthBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
            this.webSessionProvider1 = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.SuspendLayout();
            // 
            // webControl1
            // 
            this.webControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webControl1.Location = new System.Drawing.Point(0, 0);
            this.webControl1.Size = new System.Drawing.Size(1075, 525);
            this.webControl1.TabIndex = 0;
            // 
            // webSessionProvider1
            // 
            this.webSessionProvider1.DataPath = "C:\\Users\\Dave\\AppData\\Roaming\\Awesomium Technologies LLC";
            this.webSessionProvider1.Views.Add(this.webControl1);
            // 
            // AuthBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 525);
            this.ControlBox = false;
            this.Controls.Add(this.webControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AuthBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AuthBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Awesomium.Windows.Forms.WebControl webControl1;
        private Awesomium.Windows.Forms.WebSessionProvider webSessionProvider1;
    }
}