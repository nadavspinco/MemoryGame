using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MemoryGameLogic;

namespace MemoryGameUi
{
   public class MemoryGameBoardForm: Form
   {
       private readonly Size r_CellSize = new System.Drawing.Size(80,80);
       private BoardButton[,] m_Buttons;
       private Point m_CurrentPoint = new Point(10,10);
       private Label m_currentPlayerLabel;
       private Label m_firstPlayerScoreLabel;
       private readonly int r_RowSize;
       private readonly int r_ColSize;
       private readonly string r_FirstPlayerName;
       private readonly string r_SecondPlayerName;
       private readonly eGameMode r_PcOrTwoiPlayers;
       private Label m_SecondPlayerScoreLabel;
       private BoardButton m_ChosenInFirstTurn;
       private BoardButton m_ChosenInSecondTurn;
       private MemoryGame<Image> m_MemoryGame;
       private readonly Timer m_CleanWrongChoiceTimer;
       private readonly Color r_FirstPlayerColor = Color.LightGreen;
       private readonly Color r_SecondPlayerColor = Color.MediumPurple;
       private readonly List<BoardButton> m_DisabledValidButtons;
       private readonly Timer m_AiTimer;
       private readonly int k_SecondAndHalfInMilSeconds = 4000;
       private const int k_AmountOfPixelsForPictureBox = 95;
       private const int k_AmountOfPixelsForSpacing = 10;
       private static readonly object r_lockObject = new object();
       public MemoryGameBoardForm(int i_RowSize, int i_ColSize,string i_FirstPlayerName, string i_SecondPlayerName, eGameMode i_PcOrTwoiPlayers)
       {
           r_RowSize = i_RowSize;
           r_ColSize = i_ColSize;
           r_FirstPlayerName = i_FirstPlayerName;
           r_SecondPlayerName = i_SecondPlayerName;
           r_PcOrTwoiPlayers = i_PcOrTwoiPlayers;
           m_DisabledValidButtons = new List<BoardButton>();
           m_AiTimer = new Timer();
           k_SecondAndHalfInMilSeconds /= (int) i_PcOrTwoiPlayers;
           m_AiTimer.Interval = k_SecondAndHalfInMilSeconds;
           m_AiTimer.Tick += makePcTurn;
           m_CleanWrongChoiceTimer = new Timer();
           m_CleanWrongChoiceTimer.Tick += onWrongChoice;
            m_CleanWrongChoiceTimer.Interval = k_SecondAndHalfInMilSeconds;
            InitializeComponent();
       }

       protected override void OnShown(EventArgs e)
       {
           base.OnShown(e);
            m_MemoryGame = new MemoryGame<Image>(r_FirstPlayerName,
               r_SecondPlayerName,
               createMatrixOfAnyObject(r_RowSize, r_ColSize, makeImageList(r_RowSize, r_ColSize)), r_PcOrTwoiPlayers);
            m_MemoryGame.m_UpdateAfterWrongChoice += wrongChoiceUiUpdate;
            m_MemoryGame.m_UpdateAfterCorrectChoice += correctChocieUiUpdate;
            m_MemoryGame.m_SetPair += setImage;
            m_MemoryGame.m_UpdateWithFirstChoice += setChocenInFirstMoveButton;
            m_MemoryGame.m_UpdateAfterSecondChoice += setChocenInSecondMoveButton;
            m_MemoryGame.m_UpdateOnGameOver += onGameOver;
       }

       private string makeScroeLabelText(int i_Score, string i_PlayerName)
       {
           return string.Format("{0}: {1} Pairs", i_PlayerName, i_Score);
       }

        private string makeCurrentPlayerLabelText(string i_PlayerName)
        {
            return string.Format("Current Player: {0}", i_PlayerName);
        }

        private void setCurrentPlayerLabel()
        {

            Color toSetColor = getCurrentPlayerColor();
            m_currentPlayerLabel.Text = makeCurrentPlayerLabelText(m_MemoryGame.CurrentPlayerName);
            m_currentPlayerLabel.BackColor = toSetColor;
        }

        private void setClientSize()
        {
            this.Text = "Memory Game";
            this.ClientSize = new System.Drawing.Size
            (r_ColSize * k_AmountOfPixelsForPictureBox + k_AmountOfPixelsForSpacing,
                r_RowSize * k_AmountOfPixelsForPictureBox + k_AmountOfPixelsForPictureBox);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
        

        private void InitializeComponent()
        {
             setClientSize();

            m_Buttons = new BoardButton[r_RowSize, r_ColSize];
            for (int i = 0; i < r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize; j++)
                {
                    m_Buttons[i, j] = new BoardButton(i, j);
                    m_Buttons[i, j].Size = r_CellSize;
                    m_Buttons[i, j].Location = new Point(m_CurrentPoint.X, m_CurrentPoint.Y);
                    m_CurrentPoint.X += k_AmountOfPixelsForPictureBox;
                    m_Buttons[i, j].Click += gameBoardButtonClick;
                    this.Controls.Add(m_Buttons[i, j]);
                    this.Controls.Add(m_Buttons[i, j].ColorLabel);
                }
                m_CurrentPoint.Y += k_AmountOfPixelsForPictureBox;
                m_CurrentPoint.X = k_AmountOfPixelsForSpacing;
            }
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            m_currentPlayerLabel = new Label();
            m_currentPlayerLabel.Text = makeCurrentPlayerLabelText(r_FirstPlayerName);
            m_currentPlayerLabel.Location = new Point(m_CurrentPoint.X, m_CurrentPoint.Y);
            m_currentPlayerLabel.BackColor = r_FirstPlayerColor;
            m_currentPlayerLabel.AutoSize = true;
            this.Controls.Add(m_currentPlayerLabel);

            m_CurrentPoint.Y += m_currentPlayerLabel.Size.Height + k_AmountOfPixelsForSpacing;

            m_firstPlayerScoreLabel = new Label();
            m_firstPlayerScoreLabel.Location = new Point(m_CurrentPoint.X, m_CurrentPoint.Y);
            m_firstPlayerScoreLabel.Text = makeScroeLabelText(0, r_FirstPlayerName);
            m_firstPlayerScoreLabel.AutoSize = true;
            m_firstPlayerScoreLabel.BackColor = r_FirstPlayerColor;
            this.Controls.Add(m_firstPlayerScoreLabel);

            m_CurrentPoint.Y += m_currentPlayerLabel.Size.Height + k_AmountOfPixelsForSpacing;

            m_SecondPlayerScoreLabel = new Label();
            m_SecondPlayerScoreLabel.Location = new Point(m_CurrentPoint.X, m_CurrentPoint.Y);
            m_SecondPlayerScoreLabel.Text = makeScroeLabelText(0, r_SecondPlayerName);
            m_SecondPlayerScoreLabel.AutoSize = true;
            m_SecondPlayerScoreLabel.BackColor = r_SecondPlayerColor;
            this.Controls.Add(m_SecondPlayerScoreLabel);
        }


       private T[,] createMatrixOfAnyObject<T>(int i_RowSize, int i_ColSize, List<T> i_List)
        {
            Random random = new Random();
            List<T> tempList = new List<T>(i_List.Capacity * 2);
            foreach (var element in i_List)
            {
                tempList.Add(element);
                tempList.Add(element);
            }
            T[,] generalMatrix = new T[i_RowSize, i_ColSize];
            for (int i = 0; i < i_RowSize; i++)
            {
                for (int j = 0; j < i_ColSize; j++)
                {
                    int randomIndex = random.Next(tempList.Count);
                    generalMatrix[i, j] = tempList[randomIndex];
                    tempList.RemoveAt(randomIndex);
                }
            }

            return generalMatrix;
        }

       private void setImage(Pair<int, int> i_Pair, Image i_Image)
       {
           m_Buttons[i_Pair.FirstArgument, i_Pair.SecondArgument].Image = i_Image;
       }



        private void gameBoardButtonClick(object sender, EventArgs e)
        {
            BoardButton theSender = (sender as BoardButton);
            if (theSender != null)
            {
                theSender.Enabled = false;
                m_MemoryGame.PressedCell(theSender.LocationInBoard);
            }
        }

        private void setChocenInFirstMoveButton(Pair<int, int> i_Pair)
        {
            m_ChosenInFirstTurn = m_Buttons[i_Pair.FirstArgument, i_Pair.SecondArgument];
        }

        private void setChocenInSecondMoveButton(Pair<int, int> i_Pair)
        {
            shutDownAllBoardLegalButtons();
            m_ChosenInSecondTurn = m_Buttons[i_Pair.FirstArgument, i_Pair.SecondArgument];
        }



        private void correctChocieUiUpdate()
        {
            
                m_DisabledValidButtons.Remove(m_ChosenInFirstTurn);
                m_DisabledValidButtons.Remove(m_ChosenInSecondTurn);
                setButtonsColor();
                setCurrentPlayerScoreLabel();
                if (!m_MemoryGame.IsCurrentPlayerPc)
                {
                    resetAllDelayedButtons();
                }
            
        }

        private void wrongChoiceUiUpdate()
        {
            
                m_AiTimer.Enabled = false;
                m_CleanWrongChoiceTimer.Enabled = true;

        }

        private void setCurrentPlayerScoreLabel()
        {
            Label currentPlayerScoreLabel = getCurrentPlayerScoreLabel();
            currentPlayerScoreLabel.Text =
                makeScroeLabelText(m_MemoryGame.CurrentPlayerScore, m_MemoryGame.CurrentPlayerName);
        }

        private void makePcTurn(object sender, EventArgs e)
        {
            if (m_MemoryGame.IsCurrentPlayerPc == true && m_MemoryGame.IsGameOn())
            {
                Pair<int, int> move = m_MemoryGame.GetMoveFromAi();
                gameBoardButtonClick(m_Buttons[move.FirstArgument, move.SecondArgument], null);
            }
        }



        private void setButtonsColor()
        {
            m_ChosenInSecondTurn.BackColor = m_ChosenInFirstTurn.BackColor = getCurrentPlayerColor();
        }

        private Color getCurrentPlayerColor()
        {
            Color toSetColor = r_FirstPlayerColor;
            if (m_MemoryGame.IsSecondPlayerTurn)
            {
                toSetColor = r_SecondPlayerColor;
            }

            return toSetColor;
        }

        
        private void onWrongChoice(object sender, EventArgs e)
        {
            m_CleanWrongChoiceTimer.Enabled = false;
            m_ChosenInFirstTurn.SetToDefult();
            m_ChosenInSecondTurn.SetToDefult();
            m_AiTimer.Enabled = m_MemoryGame.IsCurrentPlayerPc;
            if (!m_MemoryGame.IsCurrentPlayerPc)
            {
                resetAllDelayedButtons();
            }
            setCurrentPlayerLabel();
        }

        private Label getCurrentPlayerScoreLabel()
        {
            Label toReturnLabel;
            if (m_MemoryGame.IsFirstPlayerTurn == true)
            {
                toReturnLabel = m_firstPlayerScoreLabel;
            }
            else
            {
                toReturnLabel = m_SecondPlayerScoreLabel;
            }

            return toReturnLabel;
        }

        private void shutDownAllBoardLegalButtons()
        {
            foreach (var control in Controls)
            {
                if (control is BoardButton)
                {
                    if ((control as  BoardButton).Enabled == true)
                    {
                        m_DisabledValidButtons.Add(control as BoardButton);
                        (control as BoardButton).Enabled = false;
                    }
                }
            }
        }

        private void resetAllDelayedButtons()
        {
            foreach (var button in m_DisabledValidButtons)
            {
                button.Enabled = true;
            }
            m_DisabledValidButtons.Clear();
        }

        private List<Image> makeImageList(int i_RowSize, int i_ColSize)
        {
            List<Image> imageList = new List<Image>( i_RowSize * i_ColSize);
            var converter = new ImageConverter();

            try
            {

                using (var client = new WebClient())
                {
                    for (int i = 0; i < (i_RowSize * i_ColSize) / 2; i++)
                    {
                            var imageData = client.DownloadData(@"https://picsum.photos/80");
                            var image = (Image) converter.ConvertFrom(imageData);
                            imageList.Add(image);
                    }
                }
            }

            catch (WebException e)
            {
                MessageBox.Show("Internet Connection Error");
                throw e;
            }


            return imageList;
        }

        private void onGameOver()
        {
            MessageBoxButtons messageBoxBoxButtons = MessageBoxButtons.YesNo;
             DialogResult messageBoxDialogResult = MessageBox.Show(makeFinalScoreString(), "End Game", messageBoxBoxButtons);
             DialogResult = messageBoxDialogResult;
            this.Close();
        }

        private string makeFinalScoreString()
        {
            StringBuilder finalScoreString = new StringBuilder();
            int PlayerOneScore = m_MemoryGame.FirstPlayerScore;
            int PlayerTwoScore = m_MemoryGame.SecondPlayerScore;
            if (PlayerTwoScore == PlayerOneScore)
            {
                finalScoreString.Append(string.Format("It Is A tie!{0}",System.Environment.NewLine));
            }
            else if(PlayerTwoScore <= PlayerOneScore)
            {
                finalScoreString.Append(string.Format("{0} Is the Winner!{1}",m_MemoryGame.FirstPlayerName, System.Environment.NewLine));
            }
            else
            {
                finalScoreString.Append(string.Format("{0} Is the Winner!{1}", m_MemoryGame.SecondPlayerName, System.Environment.NewLine));
            }

            finalScoreString.Append(m_firstPlayerScoreLabel.Text);
            finalScoreString.Append(System.Environment.NewLine);
            finalScoreString.Append(m_SecondPlayerScoreLabel.Text);
            finalScoreString.Append(System.Environment.NewLine);
            finalScoreString.Append("Press Yes to Restart or No To Quit");
            return finalScoreString.ToString();
        }
    }
}
