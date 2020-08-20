namespace MemoryGameUi
{
    partial class GameSettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FirstPlayerName_textBox = new System.Windows.Forms.TextBox();
            this.SecondPlayerName_textbox = new System.Windows.Forms.TextBox();
            this.AgainstAFriend_Button = new System.Windows.Forms.Button();
            this.boardSize_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.start_button = new System.Windows.Forms.Button();
            this.levelComboBox = new System.Windows.Forms.ComboBox();
            this.levelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Player Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Second Player Name";
            // 
            // FirstPlayerName_textBox
            // 
            this.FirstPlayerName_textBox.Location = new System.Drawing.Point(180, 25);
            this.FirstPlayerName_textBox.Name = "FirstPlayerName_textBox";
            this.FirstPlayerName_textBox.Size = new System.Drawing.Size(152, 22);
            this.FirstPlayerName_textBox.TabIndex = 2;
            // 
            // SecondPlayerName_textbox
            // 
            this.SecondPlayerName_textbox.Enabled = false;
            this.SecondPlayerName_textbox.Location = new System.Drawing.Point(180, 59);
            this.SecondPlayerName_textbox.Name = "SecondPlayerName_textbox";
            this.SecondPlayerName_textbox.Size = new System.Drawing.Size(152, 22);
            this.SecondPlayerName_textbox.TabIndex = 3;
            this.SecondPlayerName_textbox.Text = "Computer";
            // 
            // AgainstAFriend_Button
            // 
            this.AgainstAFriend_Button.Location = new System.Drawing.Point(349, 53);
            this.AgainstAFriend_Button.Name = "AgainstAFriend_Button";
            this.AgainstAFriend_Button.Size = new System.Drawing.Size(142, 28);
            this.AgainstAFriend_Button.TabIndex = 4;
            this.AgainstAFriend_Button.Text = "Against a Friend";
            this.AgainstAFriend_Button.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.AgainstAFriend_Button.UseVisualStyleBackColor = true;
            this.AgainstAFriend_Button.Click += new System.EventHandler(this.AgainstAFriend_Button_Click);
            // 
            // boardSize_button
            // 
            this.boardSize_button.BackColor = System.Drawing.Color.MediumPurple;
            this.boardSize_button.Location = new System.Drawing.Point(25, 155);
            this.boardSize_button.Name = "boardSize_button";
            this.boardSize_button.Size = new System.Drawing.Size(110, 72);
            this.boardSize_button.TabIndex = 5;
            this.boardSize_button.Text = "4X4";
            this.boardSize_button.UseVisualStyleBackColor = false;
            this.boardSize_button.Click += new System.EventHandler(this.boardSize_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Board Size :";
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.GreenYellow;
            this.start_button.Location = new System.Drawing.Point(328, 178);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(155, 49);
            this.start_button.TabIndex = 7;
            this.start_button.Text = "Start!";
            this.start_button.UseVisualStyleBackColor = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // levelComboBox
            // 
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.Items.AddRange(new object[] {
            "Easy",
            "Medium",
            "Hard"});
            this.levelComboBox.Location = new System.Drawing.Point(180, 91);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(152, 24);
            this.levelComboBox.TabIndex = 8;
            this.levelComboBox.Text = "Easy";
            this.levelComboBox.SelectedIndexChanged += new System.EventHandler(this.levelComboBox_SelectedIndexChanged);
            this.levelComboBox.VisibleChanged += new System.EventHandler(this.levelComboBox_SelectedIndexChanged);
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(22, 94);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(42, 17);
            this.levelLabel.TabIndex = 9;
            this.levelLabel.Text = "Level";
            // 
            // GameSettingForm
            // 
            this.AcceptButton = this.start_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(495, 239);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.levelComboBox);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.boardSize_button);
            this.Controls.Add(this.AgainstAFriend_Button);
            this.Controls.Add(this.SecondPlayerName_textbox);
            this.Controls.Add(this.FirstPlayerName_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GameSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FirstPlayerName_textBox;
        private System.Windows.Forms.TextBox SecondPlayerName_textbox;
        private System.Windows.Forms.Button AgainstAFriend_Button;
        private System.Windows.Forms.Button boardSize_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.ComboBox levelComboBox;
        private System.Windows.Forms.Label levelLabel;
    }
}