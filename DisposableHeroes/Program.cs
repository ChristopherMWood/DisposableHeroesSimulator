using DisposableHeroes.Domain;
using DisposableHeroes.Domain.Players;
using DisposableHeroes.Domain.Players.Strategies;
using DisposableHeroes.Domain.Stats;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes
{
    public static class Program
    {
        public static void Main()
        {
            var gamesToSimulate = 1000;
            var gameSummaries = new List<GameSummary>();

            for (int i = 0; i < gamesToSimulate; i++)
            {
                gameSummaries.Add(PlayGame());
                Console.WriteLine("Games Simulated: " + i + "/" + gamesToSimulate);
            }

            var totalRounds = 0;
            foreach (var summary in gameSummaries)
            {
                totalRounds += summary.NumberOfRounds;
            }

            Console.WriteLine("Average Rounds: " + totalRounds/gameSummaries.Count);
            Console.ReadLine();
        }

        public static GameSummary PlayGame()
        {
            var players = new List<BasePlayer>()
            {
                new BasePlayer("Player One", new PrimitiveStrategy()),
                new BasePlayer("Player Two", new PrimitiveStrategy()),
                new BasePlayer("Player Three", new PrimitiveStrategy()),
                new BasePlayer("Player Four", new PrimitiveStrategy()),
                new BasePlayer("Player Five", new PrimitiveStrategy()),
                new BasePlayer("Player Six", new PrimitiveStrategy())
            };

            var game = new Game(players);

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards);
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards);
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards);
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards);
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards);
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards);
            game.SetStartingPlayer(players[0]);

            var round = 1;

            while (game.Players.Count > 1)
            {
                if (round > 100)
                {
                    Console.WriteLine("GAME ENDED PREMATURELY (Infinite Game Loop Detected)");
                    break;
                }

                game.PlayBuildRound();
                game.PlayPrepareRound();
                game.PlayAttackRound();

                var startingPlayer = GetStartingPlayer(game);
                game.SetStartingPlayer(startingPlayer);

                round++;
            }

            var winner = game.Players.First();
            //Console.WriteLine("\n----------------------------------");
            //Console.WriteLine("Winner: " + winner.Name);
            //Console.WriteLine("Health: " + winner.Health);
            //Console.WriteLine("Strength: " + winner.Strength);
            //Console.WriteLine("Agility: " + winner.Agility);
            //Console.WriteLine("Perception: " + winner.Perception);
            //Console.WriteLine("Total # of Rounds: " + round);

            return new GameSummary()
            {
                WinnerName = winner.Name,
                WinningPlayerStrength = winner.Strength,
                WinningPlayerAgility = winner.Agility,
                WinningPlayerPerception = winner.Perception,
                NumberOfRounds = round
            };
        }

        public static BasePlayer GetStartingPlayer(Game game)
        {
            var startingPlayer = game.Players.First();

            foreach (var player in game.Players)
            {
                if (player.Health < startingPlayer.Health)
                {
                    startingPlayer = player;
                }
            }

            return startingPlayer;
        }
    }
}
