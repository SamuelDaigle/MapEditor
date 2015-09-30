namespace Map_Editor
{
    partial class SceneCreation
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
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblSceneName = new System.Windows.Forms.Label();
            this.txtSceneName = new System.Windows.Forms.TextBox();
            this.grbTerrain = new System.Windows.Forms.GroupBox();
            this.lblDefaultTile = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.grbTerrain.SuspendLayout();
            this.SuspendLayout();
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(72, 28);
            this.numWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(120, 20);
            this.numWidth.TabIndex = 0;
            this.numWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(12, 30);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 70);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(38, 13);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "Height";
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(72, 68);
            this.numHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(120, 20);
            this.numHeight.TabIndex = 2;
            this.numHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(110, 296);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "OK";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblSceneName
            // 
            this.lblSceneName.AutoSize = true;
            this.lblSceneName.Location = new System.Drawing.Point(50, 94);
            this.lblSceneName.Name = "lblSceneName";
            this.lblSceneName.Size = new System.Drawing.Size(67, 13);
            this.lblSceneName.TabIndex = 5;
            this.lblSceneName.Text = "Scene name";
            // 
            // txtSceneName
            // 
            this.txtSceneName.Location = new System.Drawing.Point(139, 91);
            this.txtSceneName.Name = "txtSceneName";
            this.txtSceneName.Size = new System.Drawing.Size(100, 20);
            this.txtSceneName.TabIndex = 6;
            // 
            // grbTerrain
            // 
            this.grbTerrain.Controls.Add(this.comboBox1);
            this.grbTerrain.Controls.Add(this.lblDefaultTile);
            this.grbTerrain.Controls.Add(this.numWidth);
            this.grbTerrain.Controls.Add(this.lblWidth);
            this.grbTerrain.Controls.Add(this.numHeight);
            this.grbTerrain.Controls.Add(this.lblHeight);
            this.grbTerrain.Location = new System.Drawing.Point(53, 127);
            this.grbTerrain.Name = "grbTerrain";
            this.grbTerrain.Size = new System.Drawing.Size(200, 150);
            this.grbTerrain.TabIndex = 7;
            this.grbTerrain.TabStop = false;
            this.grbTerrain.Text = "Terrain";
            // 
            // lblDefaultTile
            // 
            this.lblDefaultTile.AutoSize = true;
            this.lblDefaultTile.Location = new System.Drawing.Point(6, 108);
            this.lblDefaultTile.Name = "lblDefaultTile";
            this.lblDefaultTile.Size = new System.Drawing.Size(61, 13);
            this.lblDefaultTile.TabIndex = 4;
            this.lblDefaultTile.Text = "Default Tile";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(71, 105);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // SceneCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 336);
            this.Controls.Add(this.grbTerrain);
            this.Controls.Add(this.txtSceneName);
            this.Controls.Add(this.lblSceneName);
            this.Controls.Add(this.btnSubmit);
            this.Name = "SceneCreation";
            this.Text = "SceneCreation";
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.grbTerrain.ResumeLayout(false);
            this.grbTerrain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblSceneName;
        private System.Windows.Forms.TextBox txtSceneName;
        private System.Windows.Forms.GroupBox grbTerrain;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblDefaultTile;
    }
}