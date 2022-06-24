namespace OOP_WindowForm
{
    partial class FavoritePlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoritePlayers));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.pnlPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFavPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFavPlayer = new System.Windows.Forms.Label();
            this.favoriteStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromFavoriteStripItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblPlayer);
            this.splitContainer1.Panel1.Controls.Add(this.pnlPlayers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlFavPlayers);
            this.splitContainer1.Panel2.Controls.Add(this.lblFavPlayer);
            this.splitContainer1.Size = new System.Drawing.Size(1163, 779);
            this.splitContainer1.SplitterDistance = 625;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPlayer.Location = new System.Drawing.Point(12, 9);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(84, 27);
            this.lblPlayer.TabIndex = 1;
            this.lblPlayer.Text = "Players";
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.AllowDrop = true;
            this.pnlPlayers.AutoScroll = true;
            this.pnlPlayers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlayers.Location = new System.Drawing.Point(0, 39);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(625, 740);
            this.pnlPlayers.TabIndex = 0;
            this.pnlPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlPlayers_DragDrop);
            this.pnlPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlPlayers_DragEnter);
            // 
            // pnlFavPlayers
            // 
            this.pnlFavPlayers.AllowDrop = true;
            this.pnlFavPlayers.AutoScroll = true;
            this.pnlFavPlayers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFavPlayers.Location = new System.Drawing.Point(0, 39);
            this.pnlFavPlayers.Name = "pnlFavPlayers";
            this.pnlFavPlayers.Size = new System.Drawing.Size(534, 740);
            this.pnlFavPlayers.TabIndex = 3;
            this.pnlFavPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlFavPlayers_DragDrop);
            this.pnlFavPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlFavPlayers_DragEnter);
            // 
            // lblFavPlayer
            // 
            this.lblFavPlayer.AutoSize = true;
            this.lblFavPlayer.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFavPlayer.Location = new System.Drawing.Point(12, 9);
            this.lblFavPlayer.Name = "lblFavPlayer";
            this.lblFavPlayer.Size = new System.Drawing.Size(172, 27);
            this.lblFavPlayer.TabIndex = 2;
            this.lblFavPlayer.Text = "Favorite Players";
            // 
            // favoriteStripItem
            // 
            this.favoriteStripItem.Name = "favoriteStripItem";
            this.favoriteStripItem.Size = new System.Drawing.Size(223, 24);
            this.favoriteStripItem.Text = "Favorite";
            this.favoriteStripItem.Click += new System.EventHandler(this.favoriteStripItem_Click);
            // 
            // removeFromFavoriteStripItem
            // 
            this.removeFromFavoriteStripItem.Name = "removeFromFavoriteStripItem";
            this.removeFromFavoriteStripItem.Size = new System.Drawing.Size(223, 24);
            this.removeFromFavoriteStripItem.Text = "Remove from favorite";
            this.removeFromFavoriteStripItem.Click += new System.EventHandler(this.removeFromFavoriteStripItem_Click);
            // 
            // FavoritePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 779);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FavoritePlayers";
            this.Text = "World Cup 2018";
            this.Load += new System.EventHandler(this.FavoritePlayers_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel pnlPlayers;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.FlowLayoutPanel pnlFavPlayers;
        private System.Windows.Forms.Label lblFavPlayer;
        private System.Windows.Forms.ContextMenuStrip playerMenu;
        private System.Windows.Forms.ToolStripMenuItem favoriteStripItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromFavoriteStripItem;
    }
}