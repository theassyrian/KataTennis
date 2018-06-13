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
            
            if (IsDeuce(player))
            {
                SetPlayerScore(player, Score.Advantage);
                SetPlayerScore(otherPlayer, Score.Deuce);
            }
            else if (IsAdvantage(player))
            {
                SetPlayerScore(player, Score.Won);
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
            
            if (IsForty(player) && IsForty(otherPlayer))
            {
                SetPlayerScore(player, Score.Deuce);
                SetPlayerScore(otherPlayer, Score.Deuce);
            }
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

        bool IsAdvantage(Player player) => GetPlayerScore(player) == Score.Advantage;
        
        bool IsDeuce(Player player) => GetPlayerScore(player) == Score.Deuce;

        bool IsForty(Player player) => GetPlayerScore(player) == Score.Forty;
    }
}