namespace KataTennis
{
    public class TennisGame
    {
        readonly TennisGameState _state;

        public TennisGame()
            : this(new TennisGameState())
        {
        }

        public TennisGame(TennisGameState state)
        {
            _state = state;
        }

        public Score GetScore(Player Player)
        {
            return Score.Love;
        }

        public void PromoteScore(Player Player)
        {
            
        }
    }
}