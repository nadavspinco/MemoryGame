namespace MemoryGameLogic
{
    public struct Pair<T, S>
    {
        public T FirstArgument { get; set; }
        public S SecondArgument { get; set; }

        public Pair(T i_FirstArgument, S i_SecondArgument)
        {
            FirstArgument = i_FirstArgument;
            SecondArgument = i_SecondArgument;
        }
    }
}
