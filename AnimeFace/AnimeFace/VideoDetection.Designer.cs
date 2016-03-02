namespace AnimeFace
{
    partial class VideoDetection
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
            this.VideoBox = new System.Windows.Forms.PictureBox();
            this.Record = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VideoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoBox
            // 
            this.VideoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoBox.Location = new System.Drawing.Point(13, 13);
            this.VideoBox.Name = "VideoBox";
            this.VideoBox.Size = new System.Drawing.Size(539, 347);
            this.VideoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoBox.TabIndex = 0;
            this.VideoBox.TabStop = false;
            // 
            // Record
            // 
            this.Record.Location = new System.Drawing.Point(559, 13);
            this.Record.Name = "Record";
            this.Record.Size = new System.Drawing.Size(75, 23);
            this.Record.TabIndex = 1;
            this.Record.Text = "Record";
            this.Record.UseVisualStyleBackColor = true;
            this.Record.Click += new System.EventHandler(this.Record_Click);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(559, 62);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // VideoDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 372);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.Record);
            this.Controls.Add(this.VideoBox);
            this.Name = "VideoDetection";
            this.Text = "AnimeVideoDetection";
            ((System.ComponentModel.ISupportInitialize)(this.VideoBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VideoBox;
        private System.Windows.Forms.Button Record;
        private System.Windows.Forms.Button Play;
    }
}