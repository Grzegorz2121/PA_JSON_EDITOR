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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pick_folder_dialog = new System.Windows.Forms.SaveFileDialog();
            this.Show_Scanner_button = new System.Windows.Forms.Button();
            this.Show_Json_editor_button = new System.Windows.Forms.Button();
            this.Show_GUI_Adj_button = new System.Windows.Forms.Button();
            this.Show_Papa_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pick_folder_dialog
            // 
            this.pick_folder_dialog.AddExtension = false;
            this.pick_folder_dialog.FileName = "Folder selection";
            this.pick_folder_dialog.OverwritePrompt = false;
            this.pick_folder_dialog.Title = "Folder selection";
            this.pick_folder_dialog.ValidateNames = false;
            // 
            // Show_Scanner_button
            // 
            this.Show_Scanner_button.Location = new System.Drawing.Point(12, 12);
            this.Show_Scanner_button.Name = "Show_Scanner_button";
            this.Show_Scanner_button.Size = new System.Drawing.Size(114, 23);
            this.Show_Scanner_button.TabIndex = 3;
            this.Show_Scanner_button.Text = "Show Scanner";
            this.Show_Scanner_button.UseVisualStyleBackColor = true;
            this.Show_Scanner_button.Click += new System.EventHandler(this.Show_Scanner_button_Click);
            // 
            // Show_Json_editor_button
            // 
            this.Show_Json_editor_button.Location = new System.Drawing.Point(132, 12);
            this.Show_Json_editor_button.Name = "Show_Json_editor_button";
            this.Show_Json_editor_button.Size = new System.Drawing.Size(114, 23);
            this.Show_Json_editor_button.TabIndex = 4;
            this.Show_Json_editor_button.Text = "Show JSON editor";
            this.Show_Json_editor_button.UseVisualStyleBackColor = true;
            this.Show_Json_editor_button.Click += new System.EventHandler(this.Show_Json_editor_button_Click);
            // 
            // Show_GUI_Adj_button
            // 
            this.Show_GUI_Adj_button.Location = new System.Drawing.Point(12, 41);
            this.Show_GUI_Adj_button.Name = "Show_GUI_Adj_button";
            this.Show_GUI_Adj_button.Size = new System.Drawing.Size(114, 23);
            this.Show_GUI_Adj_button.TabIndex = 5;
            this.Show_GUI_Adj_button.Text = "Show GUI Adj";
            this.Show_GUI_Adj_button.UseVisualStyleBackColor = true;
            this.Show_GUI_Adj_button.Click += new System.EventHandler(this.Show_GUI_Adj_button_Click);
            // 
            // Show_Papa_button
            // 
            this.Show_Papa_button.Location = new System.Drawing.Point(132, 41);
            this.Show_Papa_button.Name = "Show_Papa_button";
            this.Show_Papa_button.Size = new System.Drawing.Size(114, 23);
            this.Show_Papa_button.TabIndex = 6;
            this.Show_Papa_button.Text = "Show Papa converter";
            this.Show_Papa_button.UseVisualStyleBackColor = true;
            this.Show_Papa_button.Click += new System.EventHandler(this.Show_Papa_button_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 108);
            this.Controls.Add(this.Show_Papa_button);
            this.Controls.Add(this.Show_GUI_Adj_button);
            this.Controls.Add(this.Show_Json_editor_button);
            this.Controls.Add(this.Show_Scanner_button);
            this.Name = "Main_Form";
            this.Text = "Pa Looker 2.0";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog pick_folder_dialog;
        private System.Windows.Forms.Button Show_Scanner_button;
        private System.Windows.Forms.Button Show_Json_editor_button;
        private System.Windows.Forms.Button Show_GUI_Adj_button;
        private System.Windows.Forms.Button Show_Papa_button;
    }
}

