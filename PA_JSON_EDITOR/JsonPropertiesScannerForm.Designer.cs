namespace PA_JSON_EDITOR
{
    partial class JsonPropertiesScannerForm
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
            this.scan_properties_button = new System.Windows.Forms.Button();
            this.save_properties_button = new System.Windows.Forms.Button();
            this.list_box_properties = new System.Windows.Forms.ListBox();
            this.save_file_dialog = new System.Windows.Forms.SaveFileDialog();
            this.extract_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scan_properties_button
            // 
            this.scan_properties_button.Location = new System.Drawing.Point(12, 12);
            this.scan_properties_button.Name = "scan_properties_button";
            this.scan_properties_button.Size = new System.Drawing.Size(101, 23);
            this.scan_properties_button.TabIndex = 0;
            this.scan_properties_button.Text = "Scan properties";
            this.scan_properties_button.UseVisualStyleBackColor = true;
            this.scan_properties_button.Click += new System.EventHandler(this.scan_properties_button_Click);
            // 
            // save_properties_button
            // 
            this.save_properties_button.Location = new System.Drawing.Point(119, 12);
            this.save_properties_button.Name = "save_properties_button";
            this.save_properties_button.Size = new System.Drawing.Size(101, 23);
            this.save_properties_button.TabIndex = 5;
            this.save_properties_button.Text = "Save Properties";
            this.save_properties_button.UseVisualStyleBackColor = true;
            this.save_properties_button.Click += new System.EventHandler(this.save_properties_button_Click_1);
            // 
            // list_box_properties
            // 
            this.list_box_properties.FormattingEnabled = true;
            this.list_box_properties.Location = new System.Drawing.Point(12, 41);
            this.list_box_properties.Name = "list_box_properties";
            this.list_box_properties.Size = new System.Drawing.Size(623, 368);
            this.list_box_properties.TabIndex = 1;
            // 
            // save_file_dialog
            // 
            this.save_file_dialog.FileOk += new System.ComponentModel.CancelEventHandler(this.save_file_dialog1_FileOk);
            // 
            // extract_button
            // 
            this.extract_button.Location = new System.Drawing.Point(560, 12);
            this.extract_button.Name = "extract_button";
            this.extract_button.Size = new System.Drawing.Size(75, 23);
            this.extract_button.TabIndex = 9;
            this.extract_button.Text = "Extract";
            this.extract_button.UseVisualStyleBackColor = true;
            this.extract_button.Click += new System.EventHandler(this.extract_button_Click);
            // 
            // JsonPropertiesScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 458);
            this.Controls.Add(this.extract_button);
            this.Controls.Add(this.save_properties_button);
            this.Controls.Add(this.list_box_properties);
            this.Controls.Add(this.scan_properties_button);
            this.Name = "JsonPropertiesScannerForm";
            this.Text = "Json Properties Scanner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button scan_properties_button;
        private System.Windows.Forms.Button save_properties_button;
        private System.Windows.Forms.ListBox list_box_properties;
        private System.Windows.Forms.SaveFileDialog save_file_dialog;
        private System.Windows.Forms.Button extract_button;
    }
}