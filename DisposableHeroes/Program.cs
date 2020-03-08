using DisposableHeroes.Domain;
using DisposableHeroes.Domain.Players;
using DisposableHeroes.Domain.Players.Strategies;
using DisposableHeroes.Domain.Stats;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisposableHeroes
{
    public static class Program
    {
        public static void Main()
        {
            var simulationProgressRate = 10;
            var simulationSummary = new SimulationSummary() 
            { 
                NumberOfSimulations = 10000
            };

            Console.WriteLine("Simulating " + simulationSummary.NumberOfSimulations + " games");

            for (int i = 0; i < simulationSummary.NumberOfSimulations; i++)
            {
                var gameSummary = PlayGame().Result;
                simulationSummary.TotalRounds += gameSummary.RoundsInGame;
                simulationSummary.TotalHealth += gameSummary.WinningPlayer.Health;
                simulationSummary.TotalStrength += gameSummary.WinningPlayer.Strength;
                simulationSummary.TotalAgility += gameSummary.WinningPlayer.Agility;
                simulationSummary.TotalPerception += gameSummary.WinningPlayer.Perception;

                var winningStrategy = gameSummary.WinningPlayer.Strategy.ToString();

                if (simulationSummary.StrategiesUsed.ContainsKey(winningStrategy))
                    simulationSummary.StrategiesUsed[winningStrategy] += 1;
                else
                    simulationSummary.StrategiesUsed[winningStrategy] = 1;

                if (i % (simulationSummary.NumberOfSimulations / simulationProgressRate) == 0)
                    Console.WriteLine("- " + i + "/" + simulationSummary.NumberOfSimulations);
            }

            Console.WriteLine("\n---------- Overall Game Averages ----------");
            Console.WriteLine("Average Rounds: " + simulationSummary.TotalRounds / simulationSummary.NumberOfSimulations);
            Console.WriteLine("\n---------- Player Stat Averages ----------");
            Console.WriteLine("Average Health: " + simulationSummary.TotalHealth / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Strength: " + simulationSummary.TotalStrength / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Agility: " + simulationSummary.TotalAgility / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Perception: " + simulationSummary.TotalPerception / simulationSummary.NumberOfSimulations);

            Console.WriteLine("\n---------- Strategy Win Rate ----------");

            foreach (var strategy in simulationSummary.StrategiesUsed)
            {
                var percentage = (strategy.Value / simulationSummary.NumberOfSimulations) * 100;
                Console.WriteLine("Strategy - " + strategy.Key + ": " + percentage + "%");
            }

            Console.WriteLine("\n----------------------------------------");

            Console.ReadLine();
        }

        public static async Task<GameSummary> PlayGame()
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

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards());
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards());
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards());
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards());
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards());
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards());
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

                var startingPlayer = game.SetStartingPlayerAsOneWithLowestHealth();
                game.SetStartingPlayer(startingPlayer);

                round++;
            }

            var winner = game.Players.First();

            return new GameSummary()
            {
                WinningPlayer = winner,
                RoundsInGame = round
            };
        }
    }
}
