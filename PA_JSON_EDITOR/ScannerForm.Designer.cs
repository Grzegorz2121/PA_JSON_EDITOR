namespace PA_JSON_EDITOR
{
    partial class ScannerForm
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
            this.Scan_button = new System.Windows.Forms.Button();
            this.Save_Properties_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Scan_button
            // 
            this.Scan_button.Location = new System.Drawing.Point(12, 12);
            this.Scan_button.Name = "Scan_button";
            this.Scan_button.Size = new System.Drawing.Size(75, 23);
            this.Scan_button.TabIndex = 0;
            this.Scan_button.Text = "Scan";
            this.Scan_button.UseVisualStyleBackColor = true;
            this.Scan_button.Click += new System.EventHandler(this.Scan_button_Click);
            // 
            // Save_Properties_button
            // 
            this.Save_Properties_button.Location = new System.Drawing.Point(93, 12);
            this.Save_Properties_button.Name = "Save_Properties_button";
            this.Save_Properties_button.Size = new System.Drawing.Size(101, 23);
            this.Save_Properties_button.TabIndex = 5;
            this.Save_Properties_button.Text = "Save Properties";
            this.Save_Properties_button.UseVisualStyleBackColor = true;
            this.Save_Properties_button.Click += new System.EventHandler(this.Save_Properties_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(333, 368);
            this.listBox1.TabIndex = 1;
            // 
            // ScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 435);
            this.Controls.Add(this.Save_Properties_button);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Scan_button);
            this.Name = "ScannerForm";
            this.Text = "ScannerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Scan_button;
        private System.Windows.Forms.Button Save_Properties_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
    }
}