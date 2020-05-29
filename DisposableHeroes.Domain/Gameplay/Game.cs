using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Dice;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using DisposableHeroes.Domain.Constants;
using DisposableHeroes.Domain.Actions;

namespace DisposableHeroes.Gameplay
{
    public class Game
    {
        public GameState State { get; private set; } = GameState.InProgress;
        public LinkedList<Player> Players { get; } = new LinkedList<Player>();
        public LinkedList<Player> DeadPlayers { get; } = new LinkedList<Player>();
        public Player CurrentPlayer { get; private set; }

        public CardDeck<HeadCard> HeadsDeck = new CardDeck<HeadCard>();
        public CardDeck<ArmsCard> ArmsDeck = new CardDeck<ArmsCard>();
        public CardDeck<LegsCard> LegsDeck = new CardDeck<LegsCard>();
        public CardDeck<TorsoCard> TorsosDeck = new CardDeck<TorsoCard>();
        public CardDeck<SpecialCard> SpecialsDeck = new CardDeck<SpecialCard>();
        public CardDeck<WeaponCard> WeaponsDeck = new CardDeck<WeaponCard>();
        public CardDeck<ICard> DiscardDeck = new CardDeck<ICard>();
        public Random GameRandomGenerator = new Random();

        public int Round { get; } = 1;

        public Game(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                Players.AddLast(player);
            }
        }

        public void CheckForGameOver()
        {
            if (Players.Count() <= 1)
                State = GameState.GameEnded;
        }

        public void SetStartingPlayer(Player player)
        {
            CurrentPlayer = player;
        }

        public void GiveAllPlayersSpecialCard()
        {
            DoActionForAllPlayersInOrder((player) =>
            {
                player.Backpack.Cards.Add(SpecialsDeck.Draw());
            });
        }

        public void PlayBuildRound()
        {
            DoActionForAllPlayersInOrder((player) =>
            {
                var action = ResolveDrawCardForPlayer(player);
                PerformDrawCardActionPhaseAction(action, player);
            });
        }

        public void PlayPrepareRound()
        {
            DoActionForAllPlayersInOrder((player) =>
            {
                var availablePlayerOptions = new List<CardActions>()
                {
                    CardActions.EquiptCardFromBackpack,
                    CardActions.UnequipCard,
                    CardActions.DoNothing
                };

                var cardAction = player.PerformCardAction(GameplayEvent.PreparePhase, availablePlayerOptions, this);

                switch (cardAction.Item1)
                {
                    case CardActions.EquiptCardFromBackpack:
                        var playerSelectedACard = cardAction.Item2 != null;

                        if (playerSelectedACard)
                        {
                            var cardFromBackpack = player.Backpack.RemoveFromBackpack(cardAction.Item2);

                            if (cardFromBackpack != null)
                            {
                                EquipCardForPlayer(player, cardFromBackpack);
                            }
                        }
                        break;
                    case CardActions.UnequipCard:
                        var playerSelectedACardToUnequipt = cardAction.Item2 != null;

                        if (playerSelectedACardToUnequipt)
                        {
                            var unequiptCard = player.UnequiptCard(cardAction.Item2);

                            if (unequiptCard != null)
                            {
                                DiscardDeck.AddToDeck(unequiptCard);
                            }
                        }
                        break;
                }
            });
        }

        public void PlayAttackRound()
        {
            DoActionForAllPlayersInOrder((player) =>
            {
                //TODO !!!: Might be able to reduce the condition in `if'
                if (Players.Count(p => p.Health > 0) > 1)
                {
                    var availablePlayerOptions = new List<BasicActions>()
                    {
                        BasicActions.StrengthAttack,
                        BasicActions.PerceptionAttack,
                        BasicActions.Heal,
                        BasicActions.RollForCard,
                        BasicActions.DoNothing
                    };

                    var action = player.PerformAction(GameplayEvent.AttackPhase, availablePlayerOptions, this);
                    PerformAttackPhaseAction(action, player);
                }
            });

            RemoveDeadPlayers();

            State = GameState.InProgress;
        }

        public void SetStartingPlayerAsOneWithLowestHealth()
        {
            if (Players.Count > 0)
            {
                var startingPlayer = Players.First();

                foreach (var player in Players.Where(player => player.Health < startingPlayer.Health))
                {
                    if (player.Health < startingPlayer.Health)
                        startingPlayer = player;
                }

                SetStartingPlayer(startingPlayer);
            }
        }

        private BasicActions ResolveDrawCardForPlayer(Player player)
        {
            var availablePlayerOptions = new List<BasicActions>();
            var diceRoll = new SixSidedDice().Roll();

            if (diceRoll < 3)
            {
                availablePlayerOptions.Add(BasicActions.DrawHeadCard);
                availablePlayerOptions.Add(BasicActions.DrawTorsoCard);
            }
            else if (diceRoll < 5)
            {
                availablePlayerOptions.Add(BasicActions.DrawArmsCard);
                availablePlayerOptions.Add(BasicActions.DrawLegsCard);
            }
            else
            {
                availablePlayerOptions.Add(BasicActions.DrawWeaponCard);
                availablePlayerOptions.Add(BasicActions.DrawSpecialCard);
            }

            return player.PerformAction(GameplayEvent.CardDrawn, availablePlayerOptions, this);
        }

        public void PerformDrawCardActionPhaseAction(BasicActions action, Player player)
        {
            var availablePlayerOptions = new List<CardActions>()
            {
                CardActions.EquiptCard,
                CardActions.StoreCardInBackpack,
                CardActions.DiscardCard
            };

            ICard card = null;

            switch (action)
            {
                case BasicActions.DrawHeadCard:
                    card = HeadsDeck.Draw();
                    break;
                case BasicActions.DrawTorsoCard:
                    card = TorsosDeck.Draw();
                    break;
                case BasicActions.DrawArmsCard:
                    card = ArmsDeck.Draw();
                    break;
                case BasicActions.DrawLegsCard:
                    card = LegsDeck.Draw();
                    break;
                case BasicActions.DrawWeaponCard:
                    card = WeaponsDeck.Draw();
                    break;
                case BasicActions.DrawSpecialCard:
                    card = SpecialsDeck.Draw();
                    break;
            }

            var drawnCardAction = player.PerformCardAction(GameplayEvent.CardDrawn, availablePlayerOptions, this, card);

            if (drawnCardAction.Item1 == CardActions.EquiptCard)
            {
                EquipCardForPlayer(player, card);
            }
            else if (drawnCardAction.Item1 == CardActions.StoreCardInBackpack)
            {
                var cardToRemoveFromBackpackSpecified = drawnCardAction.Item2 != null;

                if (cardToRemoveFromBackpackSpecified)
                {
                    var discardedCard = player.Backpack.RemoveFromBackpack(drawnCardAction.Item2);

                    if (discardedCard != null)
                        DiscardDeck.AddToDeck(discardedCard);
                }

                player.Backpack.StoreInBackpack(card);
            }
            else if (drawnCardAction.Item1 == CardActions.DiscardCard)
            {
                DiscardDeck.AddToDeck(card);
            }
        }

        private void EquipCardForPlayer(Player player, ICard card)
        {
            var unequiptCard = player.EquiptCard(card);

            if (unequiptCard != null)
            {
                DiscardDeck.AddToDeck(unequiptCard);
            }
        }

        private void RemoveDeadPlayers()
        {
            var deadPlayers = Players.Where(p => p.Health == 0).ToList();

            foreach (var deadPlayer in deadPlayers)
            {
                DeadPlayers.AddLast(deadPlayer);
                Players.Remove(deadPlayer);
            }
        }

        public void PerformAttackPhaseAction(BasicActions action, Player player)
        {
            switch (action)
            {
                case BasicActions.StrengthAttack:
                    var playerToAttack = GetRandomPlayerToAttack(player);
                    StrengthAttackPlayer(player, playerToAttack);
                    break;
                case BasicActions.PerceptionAttack:
                    //TODO: Implement
                    break;
                case BasicActions.Heal:
                    player.Health += GameConstants.AttackPhaseHealthGain;
                    break;
                case BasicActions.RollForCard:
                     var drawCardAction = ResolveDrawCardForPlayer(player);
                    PerformDrawCardActionPhaseAction(drawCardAction, player); //TODO: Needs to be replaced with simpler call
                    break;
            }
        }

        public Player GetRandomPlayerToAttack(Player playerToExclude)
        {
            var playersToAttack = Players.Where(p => p != playerToExclude).ToList();
            return playersToAttack[GameRandomGenerator.Next(playersToAttack.Count())];
        }

        public void StrengthAttackPlayer(Player attackingPlayer, Player defendingPlayer)
        {
            var attacking = true;

            while (attacking)
            {
                var attackingPlayerRoll = RollDiceForSkill(attackingPlayer.Strength);
                var defendingPlayerRoll = RollDiceForSkill(defendingPlayer.Agility);

                if (attackingPlayerRoll > defendingPlayerRoll)
                {
                    var attackDamage = new TwoSixSidedDice().Roll();
                    var bouncedAttackDamage = 0;

                    if (attackingPlayer.Weapon != null)
                    {
                        if (attackingPlayer.Weapon.Type == WeaponType.DoubleDamageIfEnemyHasNoWeapon && defendingPlayer.Weapon == null)
                        {
                            attackDamage += 2 * attackingPlayer.Weapon.Damage;
                        }

                        if (defendingPlayer.Weapon != null)
                        {
                            switch (defendingPlayer.Weapon.Type)
                            {
                                case WeaponType.DealWeaponDamageBackOnEnemyAttack:
                                    bouncedAttackDamage = defendingPlayer.Weapon.Damage;
                                    break;

                                case WeaponType.BlockAllWeaponDamage:
                                    break;

                                default:
                                    attackDamage += attackingPlayer.Weapon.Damage;
                                    break;
                            }
                        }
                    }


                    defendingPlayer.Health -= attackDamage;
                    attackingPlayer.Health -= bouncedAttackDamage;
                    break;
                }
                else if (attackingPlayerRoll <= defendingPlayerRoll && attackingPlayer.Weapon != null)
                {
                    if (attackingPlayer.Weapon.Type == WeaponType.IgnoreEnemyDefense)
                    {
                        defendingPlayer.Health -= attackingPlayer.Weapon.Damage;
                    }
                    break;
                }
            }
        }

        private static int RollDiceForSkill(int skillLevel)
        {
            if (skillLevel < GameConstants.MinimumSkillForBetterDice)
            {
                return new SixSidedDice().Roll();
            }
            else if (skillLevel < GameConstants.MinimumSkillForBestDice)
            {
                return new TwoSixSidedDice().Roll();
            }
            else
            {
                return new TwentySidedDice().Roll();
            }
        }

        private void DoActionForAllPlayersInOrder(Action<Player> action)
        {
            var startingPlayerNode = Players.Find(CurrentPlayer);
            var currentPlayerNode = startingPlayerNode;
            if (currentPlayerNode == null)
            {
                return;
            }

            var nextPlayerNode = currentPlayerNode.Next;

            do
            {
                var player = currentPlayerNode.Value;

                action.Invoke(player);

                currentPlayerNode = nextPlayerNode ?? Players.First;
                nextPlayerNode = currentPlayerNode.Next;
            } while (currentPlayerNode != startingPlayerNode);
        }
    }
}
