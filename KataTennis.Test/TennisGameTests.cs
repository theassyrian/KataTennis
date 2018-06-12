using System;
using Xunit;

namespace KataTennis.Test
{
    public class TennisGameTest
    {
        [Fact]
        public void GetScore_GameInInitialState_GetsLoveForEveryone()
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
        public void GetScore_GameInInlineDataState_GetsInlineDatatState(Score playerA, Score playerB)
        {
            var game = TestGameFactory.Create(playerA: playerA, playerB: playerB);

            var actualScore = game.GetScore(Player.A);
            Assert.Equal(playerA, actualScore);

            actualScore = game.GetScore(Player.B);
            Assert.Equal(playerB, actualScore);
        }
        
        [Fact]
        public void PromoteScore_GameInInitialState_PromotesPlayerAScore()
        {
            var game = TestGameFactory.CreateDefault();

            game.PromoteScore(Player.A);

            var playerAScore = game.GetScore(Player.A);
            var playerBScore = game.GetScore(Player.B);

            Assert.Equal(Score.Fifteen, playerAScore);
            Assert.Equal(Score.Love, playerBScore);
        }
        
        [Fact]
        public void PromoteScore_GameInInitialState_PromotesPlayerBScore()
        {
            var game = TestGameFactory.CreateDefault();

            game.PromoteScore(Player.B);

            var playerAScore = game.GetScore(Player.A);
            var playerBScore = game.GetScore(Player.B);

            Assert.Equal(Score.Love, playerAScore);
            Assert.Equal(Score.Fifteen, playerBScore);
        }
        
        [Theory]
        [InlineData(Score.Love, Score.Fifteen)]
        [InlineData(Score.Fifteen, Score.Thirty)]
        [InlineData(Score.Thirty, Score.Forty)]
        [InlineData(Score.Forty, Score.Won)]
        public void PromoteScore_GameInInlineDataState_FollowsScoreSequence(Score init, Score expected)
        {
            var game = TestGameFactory.Create(playerA: init);
            
            game.PromoteScore(Player.A);

            var actualScore = game.GetScore(Player.A);
            Assert.Equal(expected, actualScore);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyFortyState_PromotesPlayerToFortyWithAdvantageState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var playerAScore = game.GetScore(Player.A);
            var playerBScore = game.GetScore(Player.B);

            Assert.Equal(Score.FortyWithAdvantage, playerAScore);
            Assert.Equal(Score.Forty, playerBScore);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyFortyWithAdvantageState_SwitchesPlayerState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.FortyWithAdvantage);

            game.PromoteScore(Player.A);

            var playerAScore = game.GetScore(Player.A);
            var playerBScore = game.GetScore(Player.B);

            Assert.Equal(Score.FortyWithAdvantage, playerAScore);
            Assert.Equal(Score.Forty, playerBScore);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyWithAdvantageFortyState_PromotesPlayerToWonState()
        {
            var game = TestGameFactory.Create(playerA: Score.FortyWithAdvantage, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var playerAScore = game.GetScore(Player.A);
            var playerBScore = game.GetScore(Player.B);

            Assert.Equal(Score.Won, playerAScore);
            Assert.Equal(Score.Forty, playerBScore);
        }
    }
}
