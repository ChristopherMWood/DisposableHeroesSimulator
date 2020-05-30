using DisposableHeroes.Domain.Actions;
using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies.GenericStrategies
{
    public class HealerStrategy : IPlayerStrategy
    {
        public BasicActions PerformAction(Player player, GameplayEvent gameplayEvent, IEnumerable<BasicActions> availableActions, Game game)
        {
            if (availableActions.Contains(BasicActions.DrawTorsoCard))
            {
                return BasicActions.DrawTorsoCard;
            }

            return availableActions.First();
        }

        public Tuple<CardActions, ICard> PerformCardAction(Player player, GameplayEvent gameplayEvent, IEnumerable<CardActions> availableActions, Game game, ICard card = null)
        {
            if (availableActions.Contains(CardActions.EquiptCard) && card != null && card is TorsoCard)
            {
                return new Tuple<CardActions, ICard>(CardActions.EquiptCard, null);
            }

            return new Tuple<CardActions, ICard> (availableActions.First(), null);
        }

        public Tuple<PlayerActions, Player, ICard> PerformPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<PlayerActions> availableActions, Game game)
        {
            if (availableActions.Contains(PlayerActions.Heal) && !StrategyHelpers.OnlyTwoPlayersLeftInGame(game))
            {
                return new Tuple<PlayerActions, Player, ICard>(PlayerActions.Heal, null, null);
            }
            else if (availableActions.Contains(PlayerActions.StrengthAttack))
            {
                var playerToAttack = StrategyHelpers.ChooseRandomPlayer(player, game);
                return new Tuple<PlayerActions, Player, ICard>(PlayerActions.StrengthAttack, playerToAttack, null);
            }

            return new Tuple<PlayerActions, Player, ICard>(PlayerActions.DoNothing, null, null);
        }

        public Tuple<LootActions, ICard> LootPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<LootActions> availableActions, Game game, Player killedPlayer)
        {
            return new Tuple<LootActions, ICard>(availableActions.First(), null);
        }
    }
}
