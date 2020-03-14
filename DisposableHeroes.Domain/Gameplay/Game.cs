using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Dice;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Gameplay
{
    public class Game
    {
        public GameState State { get; private set; } = GameState.InProgress;
        public LinkedList<BasePlayer> Players { get; } = new LinkedList<BasePlayer>();
        public LinkedList<BasePlayer> DeadPlayers { get; } = new LinkedList<BasePlayer>();
        public BasePlayer CurrentPlayer { get; private set; }

        public CardDeck<HeadCard> HeadsDeck = new CardDeck<HeadCard>();
        public CardDeck<ArmsCard> ArmsDeck = new CardDeck<ArmsCard>();
        public CardDeck<LegsCard> LegsDeck = new CardDeck<LegsCard>();
        public CardDeck<TorsoCard> TorsosDeck = new CardDeck<TorsoCard>();
        public CardDeck<SpecialCard> SpecialsDeck = new CardDeck<SpecialCard>();
        public CardDeck<WeaponCard> WeaponsDeck = new CardDeck<WeaponCard>();
        public CardDeck<ICard> DiscardDeck = new CardDeck<ICard>();
        public Random GameRandomGenerator = new Random();

        public int Round { get; } = 1;

        public Game(IEnumerable<BasePlayer> players)
        {
            foreach (var player in players)
            {
                Players.AddLast(player);
            }
        }

        public void CheckForGameOver()
        {
            if (Players.Count() == 1)
                State = GameState.GameEnded;
        }

        public void SetStartingPlayer(BasePlayer player)
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

            });
        }

        public void PlayAttackRound()
        {
            DoActionForAllPlayersInOrder((player) =>
            {
                if (Players.Where(p => p.Health > 0).Count() > 1)
                {
                    var availablePlayerOptions = new List<AttackPhaseActions>()
                    {
                        AttackPhaseActions.StrengthAttack,
                        AttackPhaseActions.PerceptionAttack,
                        AttackPhaseActions.Heal,
                        AttackPhaseActions.RollForCard
                    };

                    var action = player.EvaluateAttackPhaseAction(availablePlayerOptions, this);
                    PerformAttackPhaseAction(action, player);
                }
            });

            RemoveDeadPlayers();
        }
        public void SetStartingPlayerAsOneWithLowestHealth()
        {
            var startingPlayer = Players.First();

            foreach (var player in Players)
            {
                if (player.Health < startingPlayer.Health)
                {
                    startingPlayer = player;
                }
            }

            SetStartingPlayer(startingPlayer);
        }

        private BuildPhaseActions ResolveDrawCardForPlayer(BasePlayer player)
        {
            var availablePlayerOptions = new List<BuildPhaseActions>();
            var diceRoll = new SixSidedDice().Roll();

            if (diceRoll < 3)
            {
                availablePlayerOptions.Add(BuildPhaseActions.DrawHeadCard);
                availablePlayerOptions.Add(BuildPhaseActions.DrawTorsoCard);
            }
            else if (diceRoll < 5)
            {
                availablePlayerOptions.Add(BuildPhaseActions.DrawArmsCard);
                availablePlayerOptions.Add(BuildPhaseActions.DrawLegsCard);
            }
            else
            {
                availablePlayerOptions.Add(BuildPhaseActions.DrawWeaponCard);
                availablePlayerOptions.Add(BuildPhaseActions.DrawSpecialCard);
            }

            return player.EvaluateBuildPhaseAction(availablePlayerOptions, this);
        }

        public void PerformDrawCardActionPhaseAction(BuildPhaseActions action, BasePlayer player)
        {
            var availablePlayerOptions = new List<BuildPhaseActions>()
            {
                BuildPhaseActions.EquiptCard,
                BuildPhaseActions.StoreCardInBackpack,
                BuildPhaseActions.DiscardCard
            };

            ICard card = null;

            switch (action)
            {
                case BuildPhaseActions.DrawHeadCard:
                    card = HeadsDeck.Draw();
                    break;
                case BuildPhaseActions.DrawTorsoCard:
                    card = TorsosDeck.Draw();
                    break;
                case BuildPhaseActions.DrawArmsCard:
                    card = ArmsDeck.Draw();
                    break;
                case BuildPhaseActions.DrawLegsCard:
                    card = LegsDeck.Draw();
                    break;
                case BuildPhaseActions.DrawWeaponCard:
                    card = WeaponsDeck.Draw();
                    break;
                case BuildPhaseActions.DrawSpecialCard:
                    card = SpecialsDeck.Draw();
                    break;
            }

            var drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);

            if (drawAction == BuildPhaseActions.EquiptCard)
            {
                var unequiptCard = player.EquiptCard(card);

                if (unequiptCard != null)
                {
                    DiscardDeck.AddToDeck(unequiptCard);
                }
            }
            else if (drawAction == BuildPhaseActions.StoreCardInBackpack)
            {
                if (!player.Backpack.StoreInBackpack(card)) {
                    ICard randCard = player.Backpack.Cards[GameRandomGenerator.Next(0, player.Backpack.Cards.Count)];
                    player.Backpack.RemoveFromBackpack(randCard);
                    player.Backpack.StoreInBackpack(card);
                }
                // TODO: If StoreInBackpack fails, descarding a random card and try again. Might need to change it to a helper method.
            }
            else if (drawAction == BuildPhaseActions.DiscardCard)
            {
                DiscardDeck.AddToDeck(card);
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

        public void PerformAttackPhaseAction(AttackPhaseActions action, BasePlayer player)
        {
            switch (action)
            {
                case AttackPhaseActions.StrengthAttack:
                    var playerToAttack = GetRandomPlayerToAttack(player);
                    StrengthAttackPlayer(player, playerToAttack);
                    break;
                case AttackPhaseActions.PerceptionAttack:

                    break;
                case AttackPhaseActions.Heal:
                    player.Health += 4;
                    break;
                case AttackPhaseActions.RollForCard:
                     var drawCardAction = ResolveDrawCardForPlayer(player);
                    PerformDrawCardActionPhaseAction(drawCardAction, player); //TODO: Needs to be replaced with simpler call
                    break;
            }
        }

        public BasePlayer GetRandomPlayerToAttack(BasePlayer playerToExclude)
        {
            var playersToAttack = Players.Where(p => p != playerToExclude).ToList();
            return playersToAttack[GameRandomGenerator.Next(playersToAttack.Count())];
        }

        public void StrengthAttackPlayer(BasePlayer attackingPlayer, BasePlayer defendingPlayer)
        {
            var attacking = true;

            while (attacking)
            {
                var attackingPlayerRoll = RollDiceForSkill(attackingPlayer.Strength);
                var defendingPlayerRoll = RollDiceForSkill(defendingPlayer.Agility);

                if (attackingPlayerRoll > defendingPlayerRoll)
                {
                    var attackDamage = new TwoSixSidedDice().Roll();

                    if (attackingPlayer.Weapon != null)
                        attackDamage += attackingPlayer.Weapon.Damage;

                    defendingPlayer.Health -= attackDamage;
                    break;
                }
                else if (attackingPlayerRoll <= defendingPlayerRoll)
                {
                    break;
                }
            }
        }

        private static int RollDiceForSkill(int skillLevel)
        {
            if (skillLevel < 4)
            {
                return new SixSidedDice().Roll();
            }
            else if (skillLevel < 8)
            {
                return new TwoSixSidedDice().Roll();
            }
            else
            {
                return new TwentySidedDice().Roll();
            }
        }

        private void DoActionForAllPlayersInOrder(Action<BasePlayer> action)
        {
            var startingPlayerNode = Players.Find(CurrentPlayer);
            var currentPlayerNode = startingPlayerNode;
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
