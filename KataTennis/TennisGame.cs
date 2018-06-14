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

        public override string ToString()
        {
            string Name(Score score)
            {
                if (score == Score.FortyWithAdvantage)
                {
                    return "Forty (Advantage)";
                }

                return Enum.GetName(typeof(Score), score);
            }

            return  $"Player A: {Name(_score.PlayerA)} - Player B: {Name(_score.PlayerB)} - Game: {_score.GameState}";
        }

        public TennisGameScore GetGameScore() => new TennisGameScore(_score);

        public void PromoteScore(Player player)
        {
            if (IsFinishing(player))
            {
                PromoteInFinishingGamePlay(player);
            }
            else
            {
                PromoteInRegularGamePlay(player);
            }
        }

        void PromoteInFinishingGamePlay(Player player)
        {
            var otherPlayer = GetOtherPlayer(player);
            
            if (IsFortyWithAdvantage(player))
            {
                SetPlayerScore(player, Score.Won);
            }
            else if (IsFinishing(otherPlayer))
            {
                SetPlayerScore(player, Score.FortyWithAdvantage);
                SetPlayerScore(otherPlayer, Score.Forty);
            }
            else
            {
                SetPlayerScore(player, Score.Won);
            }
        }

        void PromoteInRegularGamePlay(Player player)
        {
            var otherPlayer = GetOtherPlayer(player);
            
            SetPlayerScore(player, GetPlayerScore(player) + 1);
        }

        Player GetOtherPlayer(Player player) => player != Player.A ? Player.A : Player.B;

        Score GetPlayerScore(Player player) => player == Player.A ? _score.PlayerA : _score.PlayerB;

        void SetPlayerScore(Player player, Score score)
        {
            if(player == Player.A)
                _score.PlayerA = score;
            else
                _score.PlayerB = score;
        }
        
        bool IsFinishing(Player player) => GetPlayerScore(player) >= Score.Forty;

        bool IsFortyWithAdvantage(Player player) => GetPlayerScore(player) == Score.FortyWithAdvantage;

        bool IsForty(Player player) => GetPlayerScore(player) == Score.Forty;
    }
}