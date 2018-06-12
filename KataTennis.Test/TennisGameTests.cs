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
        
        [Theory]
        [InlineData(Score.Fifteen, Score.Forty)]
        [InlineData(Score.Love, Score.Thirty)]
        [InlineData(Score.Forty, Score.FortyWithAdvantage)]
        public void GetScore_PlayerScoreFromData_ReturnCorrectly(Score playerA, Score playerB)
        {
            var game = CreateGame(playerA: playerA, playerB: playerB);

            var actualScore = game.GetScore(Player.A);
            Assert.Equal(playerA, actualScore);

            actualScore = game.GetScore(Player.B);
            Assert.Equal(playerB, actualScore);
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
