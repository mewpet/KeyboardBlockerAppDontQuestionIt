namespace ActiveWindowTitleApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label activeWindowTitleLabel;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.activeWindowTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // activeWindowTitleLabel
            this.activeWindowTitleLabel.AutoSize = true;
            this.activeWindowTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.activeWindowTitleLabel.Name = "activeWindowTitleLabel";
            this.activeWindowTitleLabel.Size = new System.Drawing.Size(127, 13);
            this.activeWindowTitleLabel.TabIndex = 0;
            this.activeWindowTitleLabel.Text = "Active Window: (None)";

            // Form1
            this.ClientSize = new System.Drawing.Size(400, 150);
            this.Controls.Add(this.activeWindowTitleLabel);
            this.Name = "Form1";
            this.Text = "Keyboard Blocker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
