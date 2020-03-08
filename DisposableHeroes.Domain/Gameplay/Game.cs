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

        public void SetStartingPlayer(BasePlayer player)
        {
            CurrentPlayer = player;
        }

        public BasePlayer SetStartingPlayerAsOneWithLowestHealth()
        {
            var startingPlayer = Players.First();

            foreach (var player in Players)
            {
                if (player.Health < startingPlayer.Health)
                {
                    startingPlayer = player;
                }
            }

            return startingPlayer;
        }

        public void PlayBuildRound()
        {
            var startingPlayerNode = Players.Find(CurrentPlayer);
            var currentPlayerNode = startingPlayerNode;
            var nextPlayerNode = currentPlayerNode.Next;

            do
            {
                var player = currentPlayerNode.Value;
                ResolveDrawCardForPlayer(player);

                currentPlayerNode = nextPlayerNode ?? Players.First;
                nextPlayerNode = currentPlayerNode.Next;
            } while (currentPlayerNode != startingPlayerNode);
        }

        public void ResolveDrawCardForPlayer(BasePlayer player)
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

            var action = player.EvaluateBuildPhaseAction(availablePlayerOptions, this);
            PerformDrawCardActionPhaseAction(action, player);
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
            BuildPhaseActions drawAction = BuildPhaseActions.DiscardCard;

            switch (action)
            {
                case BuildPhaseActions.DrawHeadCard:
                    card = HeadsDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
                case BuildPhaseActions.DrawTorsoCard:
                    card = TorsosDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
                case BuildPhaseActions.DrawArmsCard:
                    card = ArmsDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
                case BuildPhaseActions.DrawLegsCard:
                    card = LegsDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
                case BuildPhaseActions.DrawWeaponCard:
                    card = WeaponsDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
                case BuildPhaseActions.DrawSpecialCard:
                    card = SpecialsDeck.Draw();
                    drawAction = player.EvaluateBuildPhaseDrawnCardAction(availablePlayerOptions, this, card);
                    break;
            }

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
                player.Backpack.StoreInBackpack(card);
            }
            else if (drawAction == BuildPhaseActions.DiscardCard)
            {
                DiscardDeck.AddToDeck(card);
            }
        }

        public void PlayPrepareRound()
        { 
        
        }

        public void PlayAttackRound()
        {
            var startingPlayerNode = Players.Find(CurrentPlayer);
            var currentPlayerNode = startingPlayerNode;
            var nextPlayerNode = currentPlayerNode.Next;
            var availablePlayerOptions = new List<AttackPhaseActions>()
            {
                AttackPhaseActions.StrengthAttack,
                AttackPhaseActions.PerceptionAttack,
                AttackPhaseActions.Heal,
                AttackPhaseActions.RollForCard
            };

            do
            {
                var player = currentPlayerNode.Value;

                var action = player.EvaluateAttackPhaseAction(availablePlayerOptions, this);
                PerformAttackPhaseAction(action, player);

                if (Players.Where(p => p.Health > 0).Count() == 1)
                {
                    break;
                }

                //MOVE TO NEXT PLAYER
                currentPlayerNode = nextPlayerNode ?? Players.First;
                nextPlayerNode = currentPlayerNode.Next;
            } while (currentPlayerNode != startingPlayerNode);

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
                    ResolveDrawCardForPlayer(player);
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
                    defendingPlayer.Health -= attackDamage;
                    break;
                }
                else if (attackingPlayerRoll <= defendingPlayerRoll)
                {
                    break;
                }
            }
        }

        public int RollDiceForSkill(int skillLevel)
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
    }
}
