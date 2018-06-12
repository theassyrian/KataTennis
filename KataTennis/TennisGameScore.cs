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

        public Score PlayerA { get; set; } = Score.Love;

        public Score PlayerB { get; set; } = Score.Love;
    }
}