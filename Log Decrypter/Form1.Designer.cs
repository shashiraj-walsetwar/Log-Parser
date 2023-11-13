
namespace Log_Decrypter
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.logDisplay = new System.Windows.Forms.RichTextBox();
            this.Save = new System.Windows.Forms.Button();
            this.filePath_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(592, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // logDisplay
            // 
            this.logDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logDisplay.Location = new System.Drawing.Point(27, 108);
            this.logDisplay.Name = "logDisplay";
            this.logDisplay.Size = new System.Drawing.Size(701, 340);
            this.logDisplay.TabIndex = 2;
            this.logDisplay.Text = "";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(348, 454);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 3;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // filePath_textbox
            // 
            this.filePath_textbox.Location = new System.Drawing.Point(27, 51);
            this.filePath_textbox.Name = "filePath_textbox";
            this.filePath_textbox.Size = new System.Drawing.Size(521, 22);
            this.filePath_textbox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 498);
            this.Controls.Add(this.filePath_textbox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.logDisplay);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Log Decryptor Utility";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox logDisplay;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox filePath_textbox;
    }
}

