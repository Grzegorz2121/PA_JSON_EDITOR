namespace Pa_Looker_2
{
    partial class Main_Form
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
            this.Pick_folder_button = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pick_folder_dialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // Pick_folder_button
            // 
            this.Pick_folder_button.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Pick_folder_button.Location = new System.Drawing.Point(12, 12);
            this.Pick_folder_button.Name = "Pick_folder_button";
            this.Pick_folder_button.Size = new System.Drawing.Size(75, 23);
            this.Pick_folder_button.TabIndex = 0;
            this.Pick_folder_button.Text = "Pick folder";
            this.Pick_folder_button.UseVisualStyleBackColor = true;
            this.Pick_folder_button.Click += new System.EventHandler(this.Pick_folder_button_Click);
            // 
            // pick_folder_dialog
            // 
            this.pick_folder_dialog.AddExtension = false;
            this.pick_folder_dialog.FileName = "Folder selection";
            this.pick_folder_dialog.OverwritePrompt = false;
            this.pick_folder_dialog.Title = "Folder selection";
            this.pick_folder_dialog.ValidateNames = false;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 574);
            this.Controls.Add(this.Pick_folder_button);
            this.Name = "Main_Form";
            this.Text = "Pa Looker 2.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Pick_folder_button;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog pick_folder_dialog;
    }
}

