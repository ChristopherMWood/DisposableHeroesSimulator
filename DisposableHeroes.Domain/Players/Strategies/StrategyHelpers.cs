using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public static class StrategyHelpers
    {
        private readonly static Random Random = new Random();

        public static T RandomFromList<T>(IEnumerable<T> list)
        {
            var randomIndex = Random.Next(0, list.Count());
            return list.ElementAt(randomIndex);
        }

        public static int CalculatedPotentialHealth(Player player, ICard card)
        {
            if (card is TorsoCard)
            {
                return player.Health += (card as TorsoCard).HealthBoost;
            }

            return player.Health;
        }

        public static int CalculatedPotentialStrength(Player player, ICard card)
        {
            if (card is BodyPart)
            {
                var removedCard = player.EquiptCard(card, true);

                if (removedCard != null)
                {
                    var improvement = player.Strength;

                    player.EquiptCard(removedCard);

                    return improvement;
                }
            }

            return player.Perception;
        }

        public static int CalculatedPotentialAgility(Player player, ICard card)
        {
            if (card is BodyPart)
            {
                var removedCard = player.EquiptCard(card, true);

                if (removedCard != null)
                {
                    var improvement = player.Agility;

                    player.EquiptCard(removedCard);

                    return improvement;
                }
            }

            return player.Perception;
        }

        public static int CalculatedPotentialPerception(Player player, ICard card)
        {
            if (card is BodyPart)
            {
                var removedCard = player.EquiptCard(card, true);

                if (removedCard != null)
                {
                    var improvement = player.Perception;

                    player.EquiptCard(removedCard);

                    return improvement;
                }
            }

            return player.Perception;
        }

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

        public static Player ChooseHighestHealthPlayer(Player playerToExclude, Game game)
        {
            var playersToChooseFrom = game.Players.Where(p => p != playerToExclude);
            var highestHealthPlayer = playersToChooseFrom.FirstOrDefault();

            foreach (var player in playersToChooseFrom)
            {
                if (player.Health > highestHealthPlayer.Health)
                    highestHealthPlayer = player;
            }

            return highestHealthPlayer;
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
