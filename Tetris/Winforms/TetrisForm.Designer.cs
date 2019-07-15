namespace Tetris.Winforms
{
    partial class TetrisForm
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
            this.testrisLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.previewPanel = new Tetris.Winforms.GamePanel();
            this.gamePanel = new Tetris.Winforms.GamePanel();
            this.scorePanel = new System.Windows.Forms.Panel();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreValue = new Tetris.Winforms.ScoreLabel();
            this.testrisLayoutPanel.SuspendLayout();
            this.scorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // testrisLayoutPanel
            // 
            this.testrisLayoutPanel.AutoSize = true;
            this.testrisLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.testrisLayoutPanel.Controls.Add(this.scorePanel);
            this.testrisLayoutPanel.Controls.Add(this.previewPanel);
            this.testrisLayoutPanel.Controls.Add(this.gamePanel);
            this.testrisLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.testrisLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.testrisLayoutPanel.Name = "testrisLayoutPanel";
            this.testrisLayoutPanel.Size = new System.Drawing.Size(716, 959);
            this.testrisLayoutPanel.TabIndex = 3;
            // 
            // previewPanel
            // 
            this.previewPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.previewPanel.GameGrid = null;
            this.previewPanel.Location = new System.Drawing.Point(291, 46);
            this.previewPanel.Margin = new System.Windows.Forms.Padding(10);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(133, 146);
            this.previewPanel.TabIndex = 2;
            this.previewPanel.XSize = 15;
            this.previewPanel.YSize = 15;
            // 
            // gamePanel
            // 
            this.gamePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gamePanel.GameGrid = null;
            this.gamePanel.Location = new System.Drawing.Point(15, 222);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(10);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(686, 722);
            this.gamePanel.TabIndex = 3;
            this.gamePanel.XSize = 20;
            this.gamePanel.YSize = 20;
            // 
            // scorePanel
            // 
            this.scorePanel.AutoSize = true;
            this.scorePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scorePanel.Controls.Add(this.lblScoreValue);
            this.scorePanel.Controls.Add(this.lblScore);
            this.scorePanel.Location = new System.Drawing.Point(3, 3);
            this.scorePanel.Name = "scorePanel";
            this.scorePanel.Size = new System.Drawing.Size(170, 25);
            this.scorePanel.TabIndex = 4;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(3, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(74, 25);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Score:";
            // 
            // lblScoreValue
            // 
            this.lblScoreValue.AutoSize = true;
            this.lblScoreValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreValue.Location = new System.Drawing.Point(93, 0);
            this.lblScoreValue.Name = "lblScoreValue";
            this.lblScoreValue.ScoreManager = null;
            this.lblScoreValue.Size = new System.Drawing.Size(74, 25);
            this.lblScoreValue.TabIndex = 3;
            this.lblScoreValue.Text = "Score:";
            this.lblScoreValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TetrisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(698, 915);
            this.Controls.Add(this.testrisLayoutPanel);
            this.DoubleBuffered = true;
            this.Name = "TetrisForm";
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcessKeyDown);
            this.testrisLayoutPanel.ResumeLayout(false);
            this.testrisLayoutPanel.PerformLayout();
            this.scorePanel.ResumeLayout(false);
            this.scorePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel testrisLayoutPanel;
        private System.Windows.Forms.Panel scorePanel;
        private ScoreLabel lblScoreValue;
        private System.Windows.Forms.Label lblScore;
        private GamePanel previewPanel;
        private GamePanel gamePanel;
    }
}

