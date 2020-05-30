using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Players;
using DisposableHeroes.Domain.Players.Strategies.GenericStrategies;
using System;
using System.Collections.Generic;

namespace DisposableHeroes
{
    public static class Program
    {
        public static void Main()
        {
            var summary = new SimulationSummary();
            var successfulRuns = 0;

            Console.WriteLine($"Simulating {SimulationConstants.SimulationsToRun} games");

            for (var i = 1; i <= SimulationConstants.SimulationsToRun; i++)
            {
                var players = new List<Player>()
                {
                    new Player("Player 1", new StrengthStrategy()),
                    new Player("Player 2", new HealerStrategy()),
                    new Player("Player 3", new PrimitiveStrategy())
                };
                var gameSummary = GameSimulator.SimulateGame(players, players[0]);

                if (gameSummary == null)
                    continue;

                summary.TotalRounds += gameSummary.RoundsInGame;
                if (gameSummary.WinningPlayer != null)
                {
                    summary.TotalHealth += gameSummary.WinningPlayer.Health;
                    summary.TotalStrength += gameSummary.WinningPlayer.Strength;
                    summary.TotalAgility += gameSummary.WinningPlayer.Agility;
                    summary.TotalPerception += gameSummary.WinningPlayer.Perception;
                    summary.TotalWeaponDamage += gameSummary.WinningPlayer.Weapon != null ? gameSummary.WinningPlayer.Weapon.Damage : 0;
                    TrackStringOccurence(summary.StrategiesUsed, gameSummary.WinningPlayer.Strategy.ToString());
                    TrackStringOccurence(summary.PlayerWinsByName, gameSummary.WinningPlayer.Name);
                    successfulRuns++;
                }

                if (i % (SimulationConstants.SimulationsToRun / 10) == 0)
                    Console.WriteLine($"- {i}/{successfulRuns}");
            }

            Console.WriteLine($"Number of Failed Simulations: {SimulationConstants.SimulationsToRun - successfulRuns}");

            summary.NumberOfSimulations = successfulRuns;
            PrintSimulationSummary(summary);
            Console.ReadLine();
        }

        private static void TrackStringOccurence(SortedDictionary<string, double> dictionary, string text)
        {
            if (dictionary.ContainsKey(text))
                dictionary[text]++;
            else
                dictionary[text] = 1;
        }

        private static void PrintSimulationSummary(SimulationSummary simulationSummary)
        {
            Console.WriteLine("\n---------- Overall Game Averages ----------");
            Console.WriteLine("Average Rounds: " + simulationSummary.TotalRounds / simulationSummary.NumberOfSimulations);
            Console.WriteLine("\n---------- Player Stat Averages ----------");
            Console.WriteLine("Average Health: " + simulationSummary.TotalHealth / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Strength: " + simulationSummary.TotalStrength / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Agility: " + simulationSummary.TotalAgility / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Perception: " + simulationSummary.TotalPerception / simulationSummary.NumberOfSimulations);
            Console.WriteLine("Average Weapon Damage: " + simulationSummary.TotalWeaponDamage / simulationSummary.NumberOfSimulations);

            Console.WriteLine("\n---------- Strategy Win Rate ----------");

            foreach (var strategy in simulationSummary.StrategiesUsed)
            {
                var percentage = (strategy.Value / simulationSummary.NumberOfSimulations) * 100;
                Console.WriteLine(strategy.Key + ": " + percentage + "%");
            }

            Console.WriteLine("\n---------- Player Win Rate ----------");

            foreach (var playerName in simulationSummary.PlayerWinsByName)
            {
                var percentage = (playerName.Value / simulationSummary.NumberOfSimulations) * 100;
                Console.WriteLine(playerName.Key + ": " + percentage + "%");
            }

            Console.WriteLine("\n----------------------------------------");
        }
    }
}
