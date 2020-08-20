namespace MemoryGameLogic
{
    public enum eCellChoice
    {
        Valid = 0,
        OutOfBoundaries = 1,
        ChoseBefore = 2
    }

    public class Board<T>
    {
        private const byte k_RowDimension = 0;
        private const byte k_ColumnDimension = 1;
        private readonly Cell[,] r_CellsArray;
        public int RowSize { get; }
        public int ColSize { get; }
        public int AmountOfCoveredCell { get; private set; }
        

        public Board(T[,] i_ArrayOfT)
        {
            RowSize = i_ArrayOfT.GetLength(k_RowDimension);
            ColSize = i_ArrayOfT.GetLength(k_ColumnDimension);
            r_CellsArray = new Cell[RowSize, ColSize];
            AmountOfCoveredCell = RowSize * ColSize;

            for (int i = 0; i < RowSize; i++)
            {
                for (int j = 0; j < ColSize; j++)
                {
                    r_CellsArray[i, j] = new Cell(i, j, i_ArrayOfT[i, j]);
                }
            }
        }

        //public Board(Board<T> i_Board)
        //{
        //    ColSize = i_Board.ColSize;
        //    RowSize = i_Board.RowSize;
        //    r_CellsArray = new Cell[RowSize, ColSize];

        //    for (int i = 0; i < RowSize; i++)
        //    {
        //        for (int j = 0; j < ColSize; j++)
        //        {
        //            r_CellsArray[i, j] = new Cell(i_Board.r_CellsArray[i, j]);
        //        }
        //    }

        //    AmountOfCoveredCell = i_Board.AmountOfCoveredCell;
        //}

        

        private Cell getCell(Pair<int, int> i_Choice)
        {
            return r_CellsArray[i_Choice.FirstArgument, i_Choice.SecondArgument];
        }

        private bool isOutOfBoundaries(Pair<int, int> i_cell)
        {
            bool isOutOfBoundaries = false;

            if (RowSize <= i_cell.FirstArgument || i_cell.FirstArgument < 0)
            {
                isOutOfBoundaries = true;
            }
            else if (ColSize <= i_cell.SecondArgument || i_cell.SecondArgument < 0)
            {
                isOutOfBoundaries = true;
            }

            return isOutOfBoundaries;
        }


        public T GetValue(Pair<int, int> i_Choice)
        {
            Cell c = getCell(i_Choice);
            return c.Value;
        }

        public void SetCellVisibility(Pair<int, int> i_Choice, bool i_VisibilityToSet)
        {
            Cell c = getCell(i_Choice);
            r_CellsArray[c.RowNumber, c.ColumnNumber].Revealed = i_VisibilityToSet;

            if (i_VisibilityToSet == true)
            {
                AmountOfCoveredCell--;
            }
            else
            {
                AmountOfCoveredCell++;
            }
        }

        private struct Cell
        {
            public T Value { get; } 
            public int RowNumber { get; set; }
            public int ColumnNumber { get; set; }
            public bool Revealed { get; set; }

            public Cell(int i_RowNumber, int i_ColumnNumber, T i_Value)
            {
                Value = i_Value;
                RowNumber = i_RowNumber;
                ColumnNumber = i_ColumnNumber;
                Revealed = false;
            }

            //public Cell(Cell i_Cell)
            //{
            //    Value = i_Cell.Value;
            //    RowNumber = i_Cell.RowNumber;
            //    ColumnNumber = i_Cell.ColumnNumber;
            //    Revealed = i_Cell.Revealed;
            //}
        }

        public eCellChoice GetCellStatus(Pair<int, int> i_Choice)
        {
            eCellChoice cellChoice;

            if (!isOutOfBoundaries(i_Choice))
            {
                Cell c = getCell(i_Choice);
                cellChoice = c.Revealed ? eCellChoice.ChoseBefore : eCellChoice.Valid;
            }
            else
            {
                cellChoice = eCellChoice.OutOfBoundaries;
            }

            return cellChoice;
        }
    }
}