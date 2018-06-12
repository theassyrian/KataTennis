using System;

namespace KataTennis
{
    class Program
    {
        readonly Random _random = new Random();

        Player PickWinner() => _random.Next() % 2 == 0 ? Player.A : Player.B;

        void SimulateGame()
        {
            var game = new TennisGame();

            for (var score = game.GetGameScore(); 
                score.PlayerA != Score.Won && score.PlayerB != Score.Won;
                score = game.GetGameScore())
            {
                var player = PickWinner();
                game.PromoteScore(player);
                Console.WriteLine(game);
            }
        }

        static void Main(string[] args) => new Program().SimulateGame();
    }
}
