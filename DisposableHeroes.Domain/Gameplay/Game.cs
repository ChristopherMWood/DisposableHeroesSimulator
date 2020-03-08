using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Dice;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Players;
using System.Collections.Generic;
using static DisposableHeroes.Domain.BuildPhaseEnums;

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

        public void PlayBuildRound()
        {
            var startingPlayerNode = Players.Find(CurrentPlayer);
            var currentPlayerNode = startingPlayerNode;
            var nextPlayerNode = currentPlayerNode.Next;
            var availablePlayerOptions = new List<BuildPhaseActions>();

            do
            {
                var player = currentPlayerNode.Value;
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
                PerformBuildPhaseAction(action, player);

                availablePlayerOptions.Clear();

                //MOVE TO NEXT PLAYER
                currentPlayerNode = nextPlayerNode ?? Players.First;
                nextPlayerNode = currentPlayerNode.Next;
            } while (currentPlayerNode != startingPlayerNode);
        }

        public void PerformBuildPhaseAction(BuildPhaseActions action, BasePlayer player)
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

        public void PlayAttackRound()
        { 
        
        }
    }
}
