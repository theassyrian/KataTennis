using System;
using System.Linq;

namespace KataTennis
{
    public class TennisGame
    {
        readonly TennisGameScore _score;

        public TennisGame()
        {
            _score = new TennisGameScore();
        }

        public TennisGame(TennisGameScore score)
        {
            _score = new TennisGameScore(score);
        }

        public override string ToString() => $"Player A: {_score.PlayerA} - Player B: {_score.PlayerB}";

        public TennisGameScore GetGameScore() => new TennisGameScore(_score);

        public void PromoteScore(Player player)
        {
            var score = GetPlayerScore(player);
            
            if (IsFinishing(score))
            {
                PromoteFinishingScore(player, score);
            }
            else
            {
                SetPlayerScore(player, score + 1);
            }
        }

        private void PromoteFinishingScore(Player player, Score playerScore)
        {
            var otherPlayerScore = GetOtherPlayerScore(player);

            if (HasNoAdvantage(playerScore) && IsFinishing(otherPlayerScore))
            {
                SetPlayerScore(player, Score.FortyWithAdvantage);
                SetOtherPlayerScore(player, Score.Forty);
            }
            else
            {
                SetPlayerScore(player, Score.Won);
            }
        }

        private Score GetPlayerScore(Player player) => GetScore(() => player == Player.A);

        private Score GetOtherPlayerScore(Player player) => GetScore(() => player != Player.A);

        private Score GetScore(Func<bool> isPlayerA) => isPlayerA() ? _score.PlayerA : _score.PlayerB;
        
        private void SetPlayerScore(Player player, int score) => SetPlayerScore(player, (Score)score);

        private void SetPlayerScore(Player player, Score score) => SetScore(() => player == Player.A, score);

        private void SetOtherPlayerScore(Player player, Score score) => SetScore(() => player != Player.A, score);

        private void SetScore(Func<bool> isPlayerA, Score score)
        {
            if(isPlayerA())
                _score.PlayerA = score;
            else
                _score.PlayerB = score;
        }

        private bool IsFinishing(Score score) => score >= Score.Forty;

        private bool HasNoAdvantage(Score score) => score != Score.FortyWithAdvantage;
    }
}