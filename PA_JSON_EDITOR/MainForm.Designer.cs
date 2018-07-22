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
            this.showAllButton = new System.Windows.Forms.Button();
            this.hideAllButton = new System.Windows.Forms.Button();
            this.Show_Scanner_button = new System.Windows.Forms.Button();
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
            // showAllButton
            // 
            this.showAllButton.Location = new System.Drawing.Point(93, 12);
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new System.Drawing.Size(75, 23);
            this.showAllButton.TabIndex = 1;
            this.showAllButton.Text = "Show all";
            this.showAllButton.UseVisualStyleBackColor = true;
            this.showAllButton.Click += new System.EventHandler(this.showAllButton_Click);
            // 
            // hideAllButton
            // 
            this.hideAllButton.Location = new System.Drawing.Point(174, 12);
            this.hideAllButton.Name = "hideAllButton";
            this.hideAllButton.Size = new System.Drawing.Size(75, 23);
            this.hideAllButton.TabIndex = 2;
            this.hideAllButton.Text = "Hide all";
            this.hideAllButton.UseVisualStyleBackColor = true;
            this.hideAllButton.Click += new System.EventHandler(this.hideAllButton_Click);
            // 
            // Show_Scanner_button
            // 
            this.Show_Scanner_button.Location = new System.Drawing.Point(255, 12);
            this.Show_Scanner_button.Name = "Show_Scanner_button";
            this.Show_Scanner_button.Size = new System.Drawing.Size(114, 23);
            this.Show_Scanner_button.TabIndex = 3;
            this.Show_Scanner_button.Text = "Show Scanner";
            this.Show_Scanner_button.UseVisualStyleBackColor = true;
            this.Show_Scanner_button.Click += new System.EventHandler(this.Show_Scanner_button_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 574);
            this.Controls.Add(this.Show_Scanner_button);
            this.Controls.Add(this.hideAllButton);
            this.Controls.Add(this.showAllButton);
            this.Controls.Add(this.Pick_folder_button);
            this.Name = "Main_Form";
            this.Text = "Pa Looker 2.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Pick_folder_button;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog pick_folder_dialog;
        private System.Windows.Forms.Button showAllButton;
        private System.Windows.Forms.Button hideAllButton;
        private System.Windows.Forms.Button Show_Scanner_button;
    }
}

