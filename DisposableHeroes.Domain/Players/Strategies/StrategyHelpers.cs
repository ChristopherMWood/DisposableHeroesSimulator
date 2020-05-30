using DisposableHeroes.Gameplay;
using System;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public static class StrategyHelpers
    {
        private readonly static Random Random = new Random();

        public static Player ChooseRandomPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var randomPlayerIndex = Random.Next(0, playersToChooseFrom.Count());

            return playersToChooseFrom.ElementAt(randomPlayerIndex);
        }

        public static Player ChooseLowestHealthPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var weakestPlayer = playersToChooseFrom.FirstOrDefault();

            foreach (var player in playersToChooseFrom)
            {
                if (player.Health < weakestPlayer.Health)
                    weakestPlayer = player;
            }

            return weakestPlayer;
        }

        public static Player ChooseLowestStrengthPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var lowestStrengthPlayer = playersToChooseFrom.FirstOrDefault();

            foreach (var player in playersToChooseFrom)
            {
                if (player.Strength < lowestStrengthPlayer.Strength)
                    lowestStrengthPlayer = player;
            }

            return lowestStrengthPlayer;
        }

        public static Player ChooseLowestAgilityPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var lowestAgilityPlayer = playersToChooseFrom.FirstOrDefault();

            foreach (var player in playersToChooseFrom)
            {
                if (player.Agility < lowestAgilityPlayer.Agility)
                    lowestAgilityPlayer = player;
            }

            return lowestAgilityPlayer;
        }

        public static Player ChooseLowestPerceptionPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var lowestPerceptionPlayer = playersToChooseFrom.FirstOrDefault();

            foreach (var player in playersToChooseFrom)
            {
                if (player.Perception < lowestPerceptionPlayer.Perception)
                    lowestPerceptionPlayer = player;
            }

            return lowestPerceptionPlayer;
        }

        public static bool OnlyTwoPlayersLeftInGame(Game game)
        {
            return game.Players.Count() == 2;
        }
    }
}
