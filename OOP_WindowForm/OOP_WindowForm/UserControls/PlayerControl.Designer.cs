namespace OOP_WindowForm.UserControls
{
    partial class PlayerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblplayerPosition = new System.Windows.Forms.Label();
            this.lblPlayerCaptain = new System.Windows.Forms.Label();
            this.pngStar = new System.Windows.Forms.PictureBox();
            this.lblShirtNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pngStar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(79, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 185);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPlayerName.Location = new System.Drawing.Point(74, 233);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(198, 27);
            this.lblPlayerName.TabIndex = 1;
            this.lblPlayerName.Text = "BIGFIRST BIGLAST";
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblplayerPosition
            // 
            this.lblplayerPosition.AutoSize = true;
            this.lblplayerPosition.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblplayerPosition.Location = new System.Drawing.Point(133, 271);
            this.lblplayerPosition.Name = "lblplayerPosition";
            this.lblplayerPosition.Size = new System.Drawing.Size(76, 23);
            this.lblplayerPosition.TabIndex = 2;
            this.lblplayerPosition.Text = "Position";
            // 
            // lblPlayerCaptain
            // 
            this.lblPlayerCaptain.AutoSize = true;
            this.lblPlayerCaptain.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPlayerCaptain.Location = new System.Drawing.Point(133, 294);
            this.lblPlayerCaptain.Name = "lblPlayerCaptain";
            this.lblPlayerCaptain.Size = new System.Drawing.Size(71, 23);
            this.lblPlayerCaptain.TabIndex = 3;
            this.lblPlayerCaptain.Text = "Captain";
            this.lblPlayerCaptain.Visible = false;
            // 
            // pngStar
            // 
            this.pngStar.Image = ((System.Drawing.Image)(resources.GetObject("pngStar.Image")));
            this.pngStar.Location = new System.Drawing.Point(280, 3);
            this.pngStar.Name = "pngStar";
            this.pngStar.Size = new System.Drawing.Size(59, 50);
            this.pngStar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pngStar.TabIndex = 4;
            this.pngStar.TabStop = false;
            this.pngStar.Visible = false;
            // 
            // lblShirtNumber
            // 
            this.lblShirtNumber.AutoSize = true;
            this.lblShirtNumber.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblShirtNumber.Location = new System.Drawing.Point(289, 323);
            this.lblShirtNumber.Name = "lblShirtNumber";
            this.lblShirtNumber.Size = new System.Drawing.Size(36, 27);
            this.lblShirtNumber.TabIndex = 5;
            this.lblShirtNumber.Text = "99";
            this.lblShirtNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblShirtNumber);
            this.Controls.Add(this.pngStar);
            this.Controls.Add(this.lblPlayerCaptain);
            this.Controls.Add(this.lblplayerPosition);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PlayerControl";
            this.Size = new System.Drawing.Size(342, 362);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerControl_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pngStar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblplayerPosition;
        private System.Windows.Forms.Label lblPlayerCaptain;
        private System.Windows.Forms.PictureBox pngStar;
        private System.Windows.Forms.Label lblShirtNumber;
    }
}
