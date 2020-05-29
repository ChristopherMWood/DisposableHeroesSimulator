using DisposableHeroes.Domain.Actions;
using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Constants;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Player;
using DisposableHeroes.Domain.Players.Strategies;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;

namespace DisposableHeroes.Domain.Players
{
    public class Player
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

        public Player(string name, IPlayerStrategy strategy)
        {
            Name = name;
            Strategy = strategy;
        }

        private int health = GameConstants.InitialPlayerHealth;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > GameConstants.MaxPlayerHealth)
                    health = GameConstants.MaxPlayerHealth;
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
                var total = GameConstants.BaseStrengthStat;

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
                var total = GameConstants.BaseAgilityStat;

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
                var total = GameConstants.BasePerceptionStat;

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
            if (card == null)
                return null;

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
                Health += Torso.HealthBoost;
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

        public ICard UnequiptCard(ICard card)
        {
            if (card == null)
                return null;

            ICard previouslyEquiptCard = null;

            if (Head == card)
            {
                previouslyEquiptCard = Head;
                Head = null;
            }
            else if (Torso == card)
            {
                previouslyEquiptCard = Torso;
                Torso = null;
            }
            else if (Arms == card)
            {
                previouslyEquiptCard = Arms;
                Arms = null;
            }
            else if (Legs == card)
            {
                previouslyEquiptCard = Legs;
                Legs = null;
            }
            else if (Weapon == card)
            {
                previouslyEquiptCard = Weapon;
                Weapon = null;
            }
            else if (Special == card)
            {
                previouslyEquiptCard = Special;
                Special = null;
            }

            return previouslyEquiptCard;
        }

        public BasicActions PerformAction(GameplayEvent gameplayEvent, IEnumerable<BasicActions> availableActions, Game game)
        {
            //TODO: Log user action here for player history
            return this.Strategy.PerformAction(this, gameplayEvent, availableActions, game);
        }

        public Tuple<CardActions, ICard> PerformCardAction(GameplayEvent gameplayEvent, IEnumerable<CardActions> availableActions, Game game, ICard card = null)
        {
            //TODO: Log user action here for player history
            return this.Strategy.PerformCardAction(this, gameplayEvent, availableActions, game, card);
        }

        public Tuple<PlayerActions, Player> PerformPlayerAction(GameplayEvent gameplayEvent, IEnumerable<PlayerActions> availableActions, Game game)
        {
            //TODO: Log user action here for player history
            return this.Strategy.PerformPlayerAction(this, gameplayEvent, availableActions, game);
        }

        public Tuple<LootActions, ICard> LootPlayerAction(GameplayEvent gameplayEvent, IEnumerable<LootActions> availableActions, Game game, Player killedPlayer)
        {
            //TODO: Log user action here for player history
            return this.Strategy.LootPlayerAction(this, gameplayEvent, availableActions, game, killedPlayer);
        }

    }
}
