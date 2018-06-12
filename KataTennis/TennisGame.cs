using System;
using System.Linq;

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
            return GetPlayerScore(player);
        }

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

        private Score GetScore(Func<bool> isPlayerA) => isPlayerA() ? _state.PlayerA : _state.PlayerB;
        
        private void SetPlayerScore(Player player, int score) => SetPlayerScore(player, (Score)score);

        private void SetPlayerScore(Player player, Score score) => SetScore(() => player == Player.A, score);

        private void SetOtherPlayerScore(Player player, Score score) => SetScore(() => player != Player.A, score);

        private void SetScore(Func<bool> isPlayerA, Score score)
        {
            if(isPlayerA())
                _state.PlayerA = score;
            else
                _state.PlayerB = score;
        }

        private bool IsFinishing(Score score) => score >= Score.Forty;

        private bool HasNoAdvantage(Score score) => score != Score.FortyWithAdvantage;
    }
}