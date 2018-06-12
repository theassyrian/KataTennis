namespace KataTennis.Test
{
    public static class TestGameFactory
    {
        public static TennisGame CreateDefault()
        {
            var game = new TennisGame();

            return game;
        }

        public static TennisGame Create(Score playerA = Score.Love, Score playerB = Score.Love)
        {
            var score = new TennisGameScore
            {
                PlayerA = playerA,
                PlayerB = playerB
            };

            var game = new TennisGame(score);

            return game;
        }
    }
}