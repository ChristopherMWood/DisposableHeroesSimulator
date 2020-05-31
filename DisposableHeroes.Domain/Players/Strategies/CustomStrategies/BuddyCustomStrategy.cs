using DisposableHeroes.Domain.Actions;
using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies.CustomStrategies
{
    public class BuddyCustomStrategy : IPlayerStrategy
    {
        public BasicActions PerformAction(Player player, GameplayEvent gameplayEvent, IEnumerable<BasicActions> availableActions, Game game)
        {
            if (availableActions.Contains(BasicActions.DrawArmsCard))
            {
                return BasicActions.DrawArmsCard;
            }
            else if (availableActions.Contains(BasicActions.DrawWeaponCard))
            {
                return BasicActions.DrawWeaponCard;
            }
            else if (availableActions.Contains(BasicActions.DrawLegsCard))
            {
                return BasicActions.DrawLegsCard;
            }
            else if (availableActions.Contains(BasicActions.DrawHeadCard))
            {
                return BasicActions.DrawHeadCard;
            }
            else if (availableActions.Contains(BasicActions.DrawTorsoCard))
            {
                return BasicActions.DrawTorsoCard;
            }
            else if (availableActions.Contains(BasicActions.DrawSpecialCard))
            {
                return BasicActions.DrawSpecialCard;
            }

            return BasicActions.DoNothing;
        }

        public Tuple<CardActions, ICard> PerformCardAction(Player player, GameplayEvent gameplayEvent, IEnumerable<CardActions> availableActions, Game game, ICard card = null)
        {
            if (availableActions.Contains(CardActions.EquiptCard) && card != null)
            {
                if (player.Agility < 6 && player.Agility < StrategyHelpers.CalculatedPotentialAgility(player, card))
                {
                    return new Tuple<CardActions, ICard>(CardActions.EquiptCard, card);
                }
                else if (player.Strength < 6 && player.Strength < StrategyHelpers.CalculatedPotentialStrength(player, card))
                {
                    return new Tuple<CardActions, ICard>(CardActions.EquiptCard, card);
                }
                else if (player.Health < 10 && player.Health < StrategyHelpers.CalculatedPotentialHealth(player, card))
                {
                    return new Tuple<CardActions, ICard>(CardActions.EquiptCard, card);
                }
                else if (player.Perception < 6 && player.Perception < StrategyHelpers.CalculatedPotentialPerception(player, card))
                {
                    return new Tuple<CardActions, ICard>(CardActions.EquiptCard, card);
                }
            }

            return new Tuple<CardActions, ICard>(CardActions.DoNothing, null);
        }

        public Tuple<PlayerActions, Player, ICard> PerformPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<PlayerActions> availableActions, Game game)
        {
            if (availableActions.Contains(PlayerActions.StrengthAttack))
            {
                var targetedPlayer = StrategyHelpers.ChooseRandomPlayer(player, game);

                return new Tuple<PlayerActions, Player, ICard>(PlayerActions.StrengthAttack, targetedPlayer, null);
            }

            return new Tuple<PlayerActions, Player, ICard>(availableActions.First(), null, null);
        }

        public Tuple<LootActions, ICard> LootPlayerAction(Player player, GameplayEvent gameplayEvent, IEnumerable<LootActions> availableActions, Game game, Player killedPlayer)
        {
            return new Tuple<LootActions, ICard>(LootActions.DoNothing, null);
        }
    }
}
