namespace ConwayLife.UI
{
    partial class LifeGameForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LifeGameForm));
            this.btnExit = new System.Windows.Forms.Button();
            this.numGenerations = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chklstSurviveCounts = new System.Windows.Forms.CheckedListBox();
            this.chklistBornCounts = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numCols = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.btnRunGame = new System.Windows.Forms.Button();
            this.bwGame = new System.ComponentModel.BackgroundWorker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.btnPauseGame = new System.Windows.Forms.Button();
            this.btnStepGame = new System.Windows.Forms.Button();
            this.btnClearGame = new System.Windows.Forms.Button();
            this.pnlField = new LifeGamePanel();
            ((System.ComponentModel.ISupportInitialize)(this.numGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(794, 498);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // numGenerations
            // 
            this.numGenerations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numGenerations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGenerations.Location = new System.Drawing.Point(802, 349);
            this.numGenerations.Name = "numGenerations";
            this.numGenerations.Size = new System.Drawing.Size(74, 23);
            this.numGenerations.TabIndex = 8;
            this.numGenerations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cells Survive With:";
            // 
            // chklstSurviveCounts
            // 
            this.chklstSurviveCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chklstSurviveCounts.CheckOnClick = true;
            this.chklstSurviveCounts.Enabled = false;
            this.chklstSurviveCounts.FormattingEnabled = true;
            this.chklstSurviveCounts.Location = new System.Drawing.Point(670, 118);
            this.chklstSurviveCounts.Name = "chklstSurviveCounts";
            this.chklstSurviveCounts.Size = new System.Drawing.Size(93, 139);
            this.chklstSurviveCounts.TabIndex = 4;
            // 
            // chklistBornCounts
            // 
            this.chklistBornCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chklistBornCounts.CheckOnClick = true;
            this.chklistBornCounts.Enabled = false;
            this.chklistBornCounts.FormattingEnabled = true;
            this.chklistBornCounts.Location = new System.Drawing.Point(783, 118);
            this.chklistBornCounts.Name = "chklistBornCounts";
            this.chklistBornCounts.Size = new System.Drawing.Size(93, 139);
            this.chklistBornCounts.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(780, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cells Born With:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(678, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Halt Run At Generation:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(766, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cols:";
            // 
            // numCols
            // 
            this.numCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numCols.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCols.Location = new System.Drawing.Point(802, 307);
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(74, 23);
            this.numCols.TabIndex = 7;
            this.numCols.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(759, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Rows:";
            // 
            // numRows
            // 
            this.numRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRows.Location = new System.Drawing.Point(802, 278);
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(74, 23);
            this.numRows.TabIndex = 6;
            this.numRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(711, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Step Delay (MS):";
            // 
            // numDelay
            // 
            this.numDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDelay.Location = new System.Drawing.Point(802, 378);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(74, 23);
            this.numDelay.TabIndex = 9;
            this.numDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnRunGame
            // 
            this.btnRunGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunGame.ForeColor = System.Drawing.Color.Green;
            this.btnRunGame.Location = new System.Drawing.Point(670, 12);
            this.btnRunGame.Name = "btnRunGame";
            this.btnRunGame.Size = new System.Drawing.Size(100, 30);
            this.btnRunGame.TabIndex = 0;
            this.btnRunGame.Text = "Run";
            this.btnRunGame.UseVisualStyleBackColor = true;
            // 
            // bwGame
            // 
            this.bwGame.WorkerReportsProgress = true;
            this.bwGame.WorkerSupportsCancellation = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 498);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Generation:";
            // 
            // lblGeneration
            // 
            this.lblGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGeneration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneration.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblGeneration.Location = new System.Drawing.Point(99, 498);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(92, 23);
            this.lblGeneration.TabIndex = 17;
            // 
            // btnPauseGame
            // 
            this.btnPauseGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPauseGame.ForeColor = System.Drawing.Color.Black;
            this.btnPauseGame.Location = new System.Drawing.Point(776, 12);
            this.btnPauseGame.Name = "btnPauseGame";
            this.btnPauseGame.Size = new System.Drawing.Size(100, 30);
            this.btnPauseGame.TabIndex = 2;
            this.btnPauseGame.Text = "Pause";
            this.btnPauseGame.UseVisualStyleBackColor = true;
            // 
            // btnStepGame
            // 
            this.btnStepGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStepGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStepGame.ForeColor = System.Drawing.Color.Green;
            this.btnStepGame.Location = new System.Drawing.Point(670, 48);
            this.btnStepGame.Name = "btnStepGame";
            this.btnStepGame.Size = new System.Drawing.Size(100, 30);
            this.btnStepGame.TabIndex = 1;
            this.btnStepGame.Text = "Step";
            this.btnStepGame.UseVisualStyleBackColor = true;
            // 
            // btnClearGame
            // 
            this.btnClearGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearGame.ForeColor = System.Drawing.Color.Black;
            this.btnClearGame.Location = new System.Drawing.Point(776, 48);
            this.btnClearGame.Name = "btnClearGame";
            this.btnClearGame.Size = new System.Drawing.Size(100, 30);
            this.btnClearGame.TabIndex = 3;
            this.btnClearGame.Text = "Clear";
            this.btnClearGame.UseVisualStyleBackColor = true;
            // 
            // pnlField
            // 
            this.pnlField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlField.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlField.Location = new System.Drawing.Point(15, 12);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(649, 483);
            this.pnlField.TabIndex = 19;
            // 
            // frmLifeGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 532);
            this.Controls.Add(this.btnClearGame);
            this.Controls.Add(this.btnStepGame);
            this.Controls.Add(this.pnlField);
            this.Controls.Add(this.btnPauseGame);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnRunGame);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numDelay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCols);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chklistBornCounts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chklstSurviveCounts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numGenerations);
            this.Controls.Add(this.btnExit);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 570);
            this.Name = "LifeGameForm";
            this.Text = "Life - Conway Style";
            ((System.ComponentModel.ISupportInitialize)(this.numGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.NumericUpDown numGenerations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chklstSurviveCounts;
        private System.Windows.Forms.CheckedListBox chklistBornCounts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCols;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Button btnRunGame;
        private System.ComponentModel.BackgroundWorker bwGame;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.Button btnPauseGame;
        private LifeGamePanel pnlField;
        private System.Windows.Forms.Button btnStepGame;
        private System.Windows.Forms.Button btnClearGame;
    }
}

