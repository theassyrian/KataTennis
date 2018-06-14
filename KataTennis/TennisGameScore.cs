namespace KataTennis
{
    public class TennisGameScore
    {
        public TennisGameScore()
        {
        }

        public TennisGameScore(TennisGameScore copy)
        {
            PlayerA = copy.PlayerA;
            PlayerB = copy.PlayerB;
        }

        public State GameState
        {
            get 
            {
                if (AllPlayers(Score.Forty))
                    return State.Deuce;
                if (AnyPlayer(Score.FortyWithAdvantage))
                    return State.Advantage;
                if (AnyPlayer(Score.Won))
                    return State.Won;

                return State.Normal;
            }
        }

        public Score PlayerA { get; set; } = Score.Love;

        public Score PlayerB { get; set; } = Score.Love;

        bool AllPlayers(Score score) => PlayerA == score && PlayerB == score;

        bool AnyPlayer(Score score) => PlayerA == score || PlayerB == score;
    }
}