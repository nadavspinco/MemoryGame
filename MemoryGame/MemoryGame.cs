namespace MemoryGameLogic
{
    public enum eTurnStatus
    {
        BeforeRevealCard = 0,
        AfterRevealCard = 1,
        WrongChoice = 2,
        CorrectChoice = 3
    }

    public enum eGameMode     
    {
        TwoPlayers = 0,
        PcEasyMode = 1,
        PcMediumMode= 2,
        PcHardMode = 3,
    }
    public delegate void updateUiDelegate();

    public delegate void UpdateWithChoice(Pair<int, int> i_Pair);


    public class MemoryGame<T>
    {
        public delegate void SetPairOnUi(Pair<int, int> i_Pair, T i_T);
        private const byte k_CorrectChoiceCardQuantity = 2;
        private const byte k_RowDimension = 0;
        private const byte k_ColumnDimension = 1;


        private Player Player1 { get; }
        private Player Player2 { get; }
        private Player CurrentPlayer { get; set; }


        private Board<T> Board { get; }
        private Pair<int, int> m_ChosenInTheFirstMove;
        private Pair<int, int> m_ChosenInTheSecondMove;
        private T m_ChosenInTheFirstMoveValue;
        public eTurnStatus TurnStatus { get; private set; }
        
        private readonly AiMemoryGame<T> r_AiMemoryGame;


        public event SetPairOnUi m_SetPair;
        public event updateUiDelegate m_UpdateAfterWrongChoice;
        public event updateUiDelegate m_UpdateAfterCorrectChoice;

        public event UpdateWithChoice m_UpdateWithFirstChoice;
        public event updateUiDelegate m_UpdateOnGameOver;
        public event UpdateWithChoice m_UpdateAfterSecondChoice;
        
        public MemoryGame(
            string i_PlayerUserName1, 
            string i_PlayerUserName2,
            T[,] i_ArrayOfT, 
            eGameMode i_GameMode)
        {
            const bool v_IsPc = true;
            Board = new Board<T>(i_ArrayOfT);

            Player1 = new Player(i_PlayerUserName1, !v_IsPc);
            Player2 = i_GameMode == eGameMode.TwoPlayers
                ? new Player(i_PlayerUserName2, !v_IsPc)
                : new Player(i_PlayerUserName2, v_IsPc);

            CurrentPlayer = Player1;

            if (i_GameMode >= eGameMode.PcEasyMode)
            {
                r_AiMemoryGame = new AiMemoryGame<T>(Board, i_GameMode);
            }

            TurnStatus = eTurnStatus.BeforeRevealCard;
        }

        

        public Pair<int, int> GetMoveFromAi()
        {
            return r_AiMemoryGame.CreateMove();
        }

        public bool IsGameOn()
        {
            return Board.AmountOfCoveredCell > 0;
        }

        public void PressedCell(Pair<int, int> i_Choice)
        {
            T valueOfChoice = Board.GetValue(i_Choice);
            eCellChoice cellChoice = Board.GetCellStatus(i_Choice);
            const bool v_Visible = true; 

            if (cellChoice == eCellChoice.Valid && TurnStatus != eTurnStatus.WrongChoice &&
                TurnStatus != eTurnStatus.CorrectChoice)
            {
                Board.SetCellVisibility(i_Choice, v_Visible);

                r_AiMemoryGame?.Evaluate(i_Choice, valueOfChoice);

                m_SetPair?.Invoke(i_Choice, valueOfChoice);

                if (TurnStatus == eTurnStatus.BeforeRevealCard)
                {
                    m_UpdateWithFirstChoice?.Invoke(i_Choice);
                    m_ChosenInTheFirstMove = i_Choice;
                    m_ChosenInTheFirstMoveValue = valueOfChoice;
                    TurnStatus = eTurnStatus.AfterRevealCard;
                }
                else if (TurnStatus == eTurnStatus.AfterRevealCard)
                {
                    m_UpdateAfterSecondChoice?.Invoke(i_Choice);
                    m_ChosenInTheSecondMove = i_Choice;

                    if (object.Equals(m_ChosenInTheFirstMoveValue, Board.GetValue(i_Choice)))
                    {
                        TurnStatus = eTurnStatus.CorrectChoice;
                    }
                    else
                    {
                        TurnStatus = eTurnStatus.WrongChoice;
                    }

                    determineTurn();
                }
            }


        }

        private eTurnStatus determineTurn()
        {
            eTurnStatus isCorrectChoice = TurnStatus;
            const bool v_Visible = true;

            if (TurnStatus == eTurnStatus.CorrectChoice)
            {
                ++CurrentPlayer;
                isCorrectChoice = eTurnStatus.CorrectChoice;
                m_UpdateAfterCorrectChoice?.Invoke();
                if (!IsGameOn())
                {
                    m_UpdateOnGameOver?.Invoke();
                }
            }
            else if (TurnStatus == eTurnStatus.WrongChoice)
            {
                Board.SetCellVisibility(m_ChosenInTheFirstMove, !v_Visible);
                Board.SetCellVisibility(m_ChosenInTheSecondMove, !v_Visible);

                if (CurrentPlayer == Player1)
                {
                    CurrentPlayer = Player2;
                }
                else
                {
                    CurrentPlayer = Player1;
                }

                if (r_AiMemoryGame != null)
                {
                    r_AiMemoryGame.AddToValidCards(m_ChosenInTheFirstMove);
                    r_AiMemoryGame.AddToValidCards(m_ChosenInTheSecondMove);
                }

                m_UpdateAfterWrongChoice?.Invoke();

            }

            TurnStatus = eTurnStatus.BeforeRevealCard;

            return isCorrectChoice;
        }

        public bool IsCurrentPlayerPc => CurrentPlayer.IsThePlayerPc;

        public string CurrentPlayerName => CurrentPlayer.Name;

        public bool IsFirstPlayerTurn => object.ReferenceEquals(CurrentPlayer, Player1);

        public bool IsSecondPlayerTurn => !IsFirstPlayerTurn;

        public int FirstPlayerScore => Player1.Score;

        public int SecondPlayerScore => Player2.Score;

        public string FirstPlayerName => Player1.Name;

        public string SecondPlayerName => Player2.Name;

        public int CurrentPlayerScore => CurrentPlayer.Score;


    }
   
}
