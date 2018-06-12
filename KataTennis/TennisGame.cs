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

        public Score GetScore(Player player) 
        {
            return IsPlayerA(player) ? _state.PlayerA : _state.PlayerB;
        }

        public void PromoteScore(Player Player)
        {

        }

        private bool IsPlayerA(Player player) => player == Player.A;
    }
}