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
            if (IsFinishing(player))
            {
                PromoteInFinishingStage(player);
            }
            else
            {
                PromoteInRegularStage(player);
            }
        }

        void PromoteInFinishingStage(Player player)
        {
            var otherPlayer = GetOtherPlayer(player);

            if (!IsFinishingWithAdvantage(player) && IsFinishing(otherPlayer))
            {
                SetPlayerScore(player, Score.FortyWithAdvantage);
                SetPlayerScore(otherPlayer, Score.Forty);
            }
            else
            {
                SetPlayerScore(player, Score.Won);
            }
        }

        void PromoteInRegularStage(Player player) => SetPlayerScore(player, GetPlayerScore(player) + 1);

        Player GetOtherPlayer(Player player) => player != Player.A ? Player.A : Player.B;

        Score GetPlayerScore(Player player) => player == Player.A ? _score.PlayerA : _score.PlayerB;

        void SetPlayerScore(Player player, Score score)
        {
            if(player == Player.A)
                _score.PlayerA = score;
            else
                _score.PlayerB = score;
        }

        bool IsFinishingWithAdvantage(Player player) => GetPlayerScore(player) >= Score.FortyWithAdvantage;

        bool IsFinishing(Player player) => GetPlayerScore(player) >= Score.Forty;
    }
}