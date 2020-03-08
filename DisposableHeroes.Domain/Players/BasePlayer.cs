using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Player;
using DisposableHeroes.Domain.Players.Strategies;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players
{
    public class BasePlayer
    {
        public string Name { get; }
        public HeadCard Head { get; private set; }
        public ArmsCard Arms { get; private set; }
        public TorsoCard Torso { get; private set; }
        public LegsCard Legs { get; private set; }
        public WeaponCard Weapon { get; private set; }
        public SpecialCard Special { get; private set; }
        public Backpack Backpack { get; } = new Backpack();
        public IPlayerStrategy Strategy { get; private set; }

        public BasePlayer(string name, IPlayerStrategy strategy)
        {
            Name = name;
            Health = 25;
            Strategy = strategy;
        }

        private int health = 20;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 25)
                    health = 25;
                else if (value < 0)
                    health = 0;
                else
                    health = value;
            }
        }

        public int Strength
        {
            get
            {
                var total = 4;

                if (Head != null)
                    total += Head.Strength;

                if (Torso != null)
                    total += Torso.Strength;

                if (Arms != null)
                    total += Arms.Strength;

                if (Legs != null)
                    total += Legs.Strength;

                return total;
            }
        }

        public int Agility
        {
            get
            {
                var total = 4;

                if (Head != null)
                    total += Head.Agility;

                if (Torso != null)
                    total += Torso.Agility;

                if (Arms != null)
                    total += Arms.Agility;

                if (Legs != null)
                    total += Legs.Agility;

                return total;
            }
        }

        public int Perception
        {
            get
            {
                var total = 4;

                if (Head != null)
                    total += Head.Perception;

                if (Torso != null)
                    total += Torso.Perception;

                if (Arms != null)
                    total += Arms.Perception;

                if (Legs != null)
                    total += Legs.Perception;

                return total;
            }
        }

        public ICard EquiptCard(ICard card)
        {
            ICard previouslyEquiptCard = null;

            if (card is HeadCard)
            {
                previouslyEquiptCard = Head;
                Head = card as HeadCard;
            }
            else if (card is TorsoCard)
            {
                previouslyEquiptCard = Torso;
                Torso = card as TorsoCard;
            }
            else if (card is ArmsCard)
            {
                previouslyEquiptCard = Arms;
                Arms = card as ArmsCard;
            }
            else if (card is LegsCard)
            {
                previouslyEquiptCard = Legs;
                Legs = card as LegsCard;
            }
            else if (card is WeaponCard)
            {
                previouslyEquiptCard = Weapon;
                Weapon = card as WeaponCard;
            }
            else if (card is SpecialCard)
            {
                previouslyEquiptCard = Special;
                Special = card as SpecialCard;
            }

            return previouslyEquiptCard;
        }

        public BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<BuildPhaseActions> availableActions, Game game)
        {
            var chosenAction = Strategy.EvaluateBuildPhaseAction(availableActions, this, game);
            //Console.WriteLine(Name + ": " + chosenAction.ToString());
            //LOG PLAYER ACTION HERE IN HISTORY
            return chosenAction;
        }

        public BuildPhaseActions EvaluateBuildPhaseDrawnCardAction(IEnumerable<BuildPhaseActions> availableActions, Game game, ICard card)
        {
            var chosenAction = Strategy.EvaluateBuildPhaseDrawnCardAction(availableActions, this, game, card);
            //Console.WriteLine(Name + ": " + chosenAction.ToString());
            //LOG PLAYER ACTION HERE IN HISTORY
            return chosenAction;
        }

        public AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, Game game)
        {
            var chosenAction = Strategy.EvaluateAttackPhaseAction(availableActions, this, game);
            //Console.WriteLine(Name + ": " + chosenAction.ToString());
            //LOG PLAYER ACTION HERE IN HISTORY
            return chosenAction;
        }
    }
}
