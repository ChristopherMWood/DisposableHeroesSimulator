﻿using DisposableHeroes.Domain.Actions;
using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class HealerStrategy : IPlayerStrategy
    {
        public BasicActions PerformAction(Player player, GameplayEvent gameplayEvent, IEnumerable<BasicActions> availableActions, Game game)
        {
            return availableActions.First();
        }

        public Tuple<CardActions, ICard> PerformCardAction(Player player, GameplayEvent gameplayEvent, IEnumerable<CardActions> availableActions, Game game, ICard card = null)
        {
            return new Tuple<CardActions, ICard> (availableActions.First(), null);
        }

        public Tuple<PlayerActions, Player, ICard> PerformPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<PlayerActions> availableActions, Game game)
        {
            var playerToAttack = StrategyHelpers.ChooseRandomPlayer(player, game);
            return new Tuple<PlayerActions, Player, ICard>(availableActions.First(), playerToAttack, null);
        }

        public Tuple<LootActions, ICard> LootPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<LootActions> availableActions, Game game, Player killedPlayer)
        {
            return new Tuple<LootActions, ICard>(availableActions.First(), null);
        }
    }
}
