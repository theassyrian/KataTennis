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
            
            var score = game.GetGameScore();

            Assert.Equal(Score.Love, score.PlayerA);
            Assert.Equal(Score.Love, score.PlayerB);
        }
        
        [Theory]
        [InlineData(Score.Fifteen, Score.Forty)]
        [InlineData(Score.Love, Score.Thirty)]
        [InlineData(Score.Forty, Score.FortyWithAdvantage)]
        public void GetScore_GameInInlineDataState_GetsInlineDatatState(Score playerA, Score playerB)
        {
            var game = TestGameFactory.Create(playerA: playerA, playerB: playerB);

            var score = game.GetGameScore();
            
            Assert.Equal(playerA, score.PlayerA);
            Assert.Equal(playerB, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInInitialState_PromotesPlayerAScore()
        {
            var game = TestGameFactory.CreateDefault();

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(Score.Fifteen, score.PlayerA);
            Assert.Equal(Score.Love, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInInitialState_PromotesPlayerBScore()
        {
            var game = TestGameFactory.CreateDefault();

            game.PromoteScore(Player.B);

            var score = game.GetGameScore();
            Assert.Equal(Score.Love, score.PlayerA);
            Assert.Equal(Score.Fifteen, score.PlayerB);
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

            var score = game.GetGameScore();
            Assert.Equal(expected, score.PlayerA);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyFortyState_PromotesPlayerToFortyWithAdvantageState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(Score.FortyWithAdvantage, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyFortyWithAdvantageState_SwitchesPlayerState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.FortyWithAdvantage);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(Score.FortyWithAdvantage, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyWithAdvantageFortyState_PromotesPlayerToWonState()
        {
            var game = TestGameFactory.Create(playerA: Score.FortyWithAdvantage, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(Score.Won, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
    }
}
