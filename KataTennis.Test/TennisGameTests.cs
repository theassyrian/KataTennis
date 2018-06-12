using System;
using Xunit;

namespace KataTennis.Test
{
    public class TennisGameTest
    {
        [Fact]
        public void GetScore_GameInInitialState_ReturnLoveForAllPlayers()
        {
            var game = new TennisGame();

            var playerAScore = game.GetScore(Player.A);
            Assert.Equal(Score.Love, playerAScore);

            var playerBScore = game.GetScore(Player.B);
            Assert.Equal(Score.Love, playerBScore);
        }

        public TennisGame CreateGame(Score playerA = Score.Love, Score playerB = Score.Love)
        {
            var state = new TennisGameState
            {
                PlayerA = playerA,
                PlayerB = playerB
            };

            var game = new TennisGame(state);

            return game;
        }
    }
}
