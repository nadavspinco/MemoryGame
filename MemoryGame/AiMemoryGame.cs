using System;
using System.Collections.Generic;

namespace MemoryGameLogic
{
    public class AiMemoryGame<T>
    {
        public readonly Stack<Pair<int, int>> m_SmartMoves;
        private readonly Dictionary<Pair<int, int>, T> m_ExposedCells;
        private readonly List<Pair<int, int>> m_UnExposedCells;
        private readonly List<Pair<int, int>> m_ValidCells;
        private readonly eGameMode r_GameMode;
        private readonly Board<T> m_Board;
        private  readonly Random r_Random = new Random();

        public AiMemoryGame(Board<T> i_Board,eGameMode i_GameMode)
        {
            r_GameMode = i_GameMode;
            m_Board = i_Board;
            int rowSize = m_Board.RowSize;
            int colSize = m_Board.ColSize;
            m_ValidCells = new List<Pair<int, int>>(rowSize * colSize);
            if (eGameMode.PcEasyMode < i_GameMode)
            {
                m_ExposedCells = new Dictionary<Pair<int, int>, T>(rowSize * colSize);
                m_SmartMoves = new Stack<Pair<int, int>>();
            }

            if (eGameMode.PcHardMode ==  i_GameMode)
            {
                m_UnExposedCells = new List<Pair<int, int>>(rowSize * colSize);
            }


            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    m_ValidCells.Add(new Pair<int, int>(i, j));
                    if (m_UnExposedCells!=null)
                    {
                        m_UnExposedCells.Add(new Pair<int, int>(i, j));
                    }
                  
                }
            }

            m_ExposedCells = new Dictionary<Pair<int, int>, T>(rowSize * colSize);
           
        }

        private void addToSmartMoves(Pair<int, int> i_Pair)
        {
            m_SmartMoves.Push(i_Pair);
        }

        public void Evaluate(Pair<int, int> i_Pair, T i_Value)
        {
            m_ValidCells.Remove(i_Pair);
            if (m_UnExposedCells != null)
            {
                m_UnExposedCells.Remove(i_Pair);
            }

            bool isValueAlreadyExposedFlag = false;
            if (m_ExposedCells != null && m_SmartMoves != null)
            {
                // Check If i_Value Already Exists In ExposedCells 
                foreach (var revealedCell in m_ExposedCells)
                {
                    if (revealedCell.Value.Equals(i_Value))
                    {
                        isValueAlreadyExposedFlag = true;

                        if (!revealedCell.Key.Equals(i_Pair))
                        {
                            addToSmartMoves(i_Pair);
                            addToSmartMoves(revealedCell.Key);
                        }

                        break;
                    }
                }
                if (!isValueAlreadyExposedFlag)
                {
                    m_ExposedCells.Add(i_Pair, i_Value);
                }
            }

        }

        private bool existsInValidCards(Pair<int, int> i_Pair)
        {
            return m_Board.GetCellStatus(i_Pair) == eCellChoice.Valid;
        }

        public void AddToValidCards(Pair<int, int> i_Pair)
        {
            m_ValidCells.Add(i_Pair);
        }

        public Pair<int, int> CreateMove()
        {
            Pair<int, int> move = new Pair<int, int>(); 
            bool isThereASmartAndValidMoveFlag = false;

            while (m_SmartMoves != null && m_SmartMoves.Count > 0)
            {
                move = m_SmartMoves.Pop();

                if (existsInValidCards(move))
                {
                    isThereASmartAndValidMoveFlag = true;
                    break;
                }
            }

            if (!isThereASmartAndValidMoveFlag)
            {
                

                if (m_UnExposedCells!= null&& m_UnExposedCells.Count > 0)
                {
                    int randomIndex = r_Random.Next(m_UnExposedCells.Count);
                    move = m_UnExposedCells[randomIndex];
                }
                else
                {
                    int randomIndex = r_Random.Next(m_ValidCells.Count);
                    move = m_ValidCells[randomIndex];
                }
            }

            return move;
        }
    }
}
