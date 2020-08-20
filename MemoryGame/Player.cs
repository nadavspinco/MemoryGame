namespace MemoryGameLogic
{
   public class Player
    {
        public  bool IsThePlayerPc { get; }
        public string Name { get; }

        public int Score { get;private set; } = 0;

        public Player(string i_Name, bool i_IsThePlayerPc)
        {
            Name = i_Name;
            IsThePlayerPc = i_IsThePlayerPc;
        }

        public static Player operator ++(Player i_Player)
        {
            ++i_Player.Score;
            return i_Player;
        }

  
    }
}
