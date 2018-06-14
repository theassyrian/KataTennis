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

            Assert.Equal(State.Normal, score.GameState);
            Assert.Equal(Score.Love, score.PlayerA);
            Assert.Equal(Score.Love, score.PlayerB);
        }
        
        [Theory]
        [InlineData(Score.Fifteen, Score.Forty, State.Normal)]
        [InlineData(Score.Love, Score.Thirty, State.Normal)]
        [InlineData(Score.Forty, Score.Forty, State.Deuce)]
        [InlineData(Score.FortyWithAdvantage, Score.Forty, State.Advantage)]
        [InlineData(Score.Forty, Score.Won, State.Won)]
        public void GetScore_GameInInlineDataState_GetsInlineDatatState(Score playerA, Score playerB, State state)
        {
            var game = TestGameFactory.Create(playerA: playerA, playerB: playerB);

            var score = game.GetGameScore();
            
            Assert.Equal(state, score.GameState);
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
        public void PromoteScore_GameInThirtyFourtyState_PromotesGameToDeuceState()
        {
            var game = TestGameFactory.Create(playerA: Score.Thirty, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(State.Deuce, score.GameState);
            Assert.Equal(Score.Forty, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInDeuceState_PromotesPlayerAndGameToAdvantageState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(State.Advantage, score.GameState);
            Assert.Equal(Score.FortyWithAdvantage, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInFortyAdvantageState_SwitchesPlayerState()
        {
            var game = TestGameFactory.Create(playerA: Score.Forty, playerB: Score.FortyWithAdvantage);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(State.Advantage, score.GameState);
            Assert.Equal(Score.FortyWithAdvantage, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInAdvantageFortyState_PromotesPlayerToWonAndFortyState()
        {
            var game = TestGameFactory.Create(playerA: Score.FortyWithAdvantage, playerB: Score.Forty);

            game.PromoteScore(Player.A);

            var score = game.GetGameScore();
            Assert.Equal(State.Won, score.GameState);
            Assert.Equal(Score.Won, score.PlayerA);
            Assert.Equal(Score.Forty, score.PlayerB);
        }
        
        [Fact]
        public void PromoteScore_GameInWonState_ThrowsExceptionWhenPromote()
        {
            var game = TestGameFactory.Create(playerA: Score.Won, playerB: Score.Forty);

            Assert.Throws<InvalidOperationException>(() => game.PromoteScore(Player.A));
        }
    }
}
