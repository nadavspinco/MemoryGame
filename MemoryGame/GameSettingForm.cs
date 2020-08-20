using System;
using System.Collections.Generic;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MemoryGameLogic;

    

namespace MemoryGameUi
{
    public partial class GameSettingForm : Form
    {
        private const int k_RowMinSize = 4;
        private const int k_ColMinSize =4;
        private const int k_RowMaxSize = 6;
        private const int k_ColMaxSize = 6;
        public int RowSize { get; private set; } = 4;
        public int ColSize { get; private set; } = 4;
        public string FirstPlayerName { get; private set; }
        public string SecondPlayerName { get; private set; }
        public eGameMode GameMode { get; private set; } = eGameMode.PcEasyMode;
        public GameSettingForm()
        {
            InitializeComponent();
        }



        private void AgainstAFriend_Button_Click(object sender, EventArgs e)
        {

            if (SecondPlayerName_textbox.Enabled == true)
            {
                SecondPlayerName_textbox.Enabled = false;
                SecondPlayerName_textbox.Text = "computer";
                AgainstAFriend_Button.Text = "against a Friend";
                levelLabel.Visible = true;
                levelComboBox.Visible = true; // change also GameMode

            }
            else
            {
                SecondPlayerName_textbox.Enabled = true;
                SecondPlayerName_textbox.Text = "";
                AgainstAFriend_Button.Text = "against Computer";
                GameMode = eGameMode.TwoPlayers;
                levelLabel.Visible = false;
                levelComboBox.Visible = false;
            }
        }


    

        private void boardSize_button_Click(object sender, EventArgs e)
        {
            if (k_ColMaxSize == ColSize  && k_RowMaxSize == RowSize )
            {
                ColSize = k_ColMinSize;
                RowSize = k_RowMinSize;
            }
            else if (ColSize < k_ColMaxSize)
            {
                ColSize++;
                if (RowSize %2 != 0 && ColSize %2 != 0)
                {
                    boardSize_button_Click(sender, e);
                    return;
                    
                }
            }
            else
            {
                RowSize++;
                ColSize = k_ColMinSize;
                if (RowSize % 2 != 0 && ColSize % 2 != 0)
                {
                    boardSize_button_Click(sender, e);
                    return;
                }
            }

            (sender as Button).Text = string.Format("{0}{1}{2}", RowSize, 'X', ColSize);

        }

        private void start_button_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(FirstPlayerName_textBox.Text))
            {
                showStringInMessageBox("player 1 name is empty");
            }
            else if (string.IsNullOrEmpty(SecondPlayerName_textbox.Text))
            {
                showStringInMessageBox("player 2 name is empty");
            }
            else
            {
                FirstPlayerName = FirstPlayerName_textBox.Text;
                SecondPlayerName = SecondPlayerName_textbox.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void showStringInMessageBox(string i_String)
        {
            MessageBox.Show(i_String);
        }

        private void GameSettingForm_Load(object sender, EventArgs e)
        {

        }

        private void levelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (true == (sender as ComboBox).Visible)
            {
                GameMode = (eGameMode) (sender as ComboBox).SelectedIndex + 1;
            }
        }
    }
}
