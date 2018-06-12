using System;
using Xunit;

namespace KataTennis.Test
{
    public class TennisGameTest
    {
        [Fact]
        public void GetScore_GameInInitialState_ReturnLoveForAllPlayers()
        {
            var game = TestGameFactory.CreateDefault();

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
            var game = TestGameFactory.Create(playerA: playerA, playerB: playerB);

            var actualScore = game.GetScore(Player.A);
            Assert.Equal(playerA, actualScore);

            actualScore = game.GetScore(Player.B);
            Assert.Equal(playerB, actualScore);
        }
    }
}
