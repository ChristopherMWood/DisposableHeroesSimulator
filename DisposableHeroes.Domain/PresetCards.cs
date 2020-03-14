using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using System.Collections.Generic;

namespace DisposableHeroes.Domain
{
    public static class PresetCards
    {
        private static int TotalBodyCards = 62;

        public static List<HeadCard> AllHeadCards()
        {
            var cards = new List<HeadCard>();

            for (var i = 0; i < TotalBodyCards; i++)
            {
                if (i < 26)
                {
                    cards.Add(new HeadCard("First Tier", 0, 0, 1));
                }
                else if (i < 44)
                {
                    cards.Add(new HeadCard("Second Tier", 0, 0, 2));
                }
                else if (i < 54)
                {
                    cards.Add(new HeadCard("Third Tier", 0, 0, 3));
                }
                else
                {
                    cards.Add(new HeadCard("Fourth Tier", 0, 0, 4));
                }
            }

            return cards;
        }

        public static List<ArmsCard> AllArmsCards()
        {
            var cards = new List<ArmsCard>();

            for (var i = 0; i < TotalBodyCards; i++)
            {
                if (i < 26)
                {
                    cards.Add(new ArmsCard("First Tier", 1, 0, 0));
                }
                else if (i < 44)
                {
                    cards.Add(new ArmsCard("Second Tier", 2, 0, 0));
                }
                else if (i < 54)
                {
                    cards.Add(new ArmsCard("Third Tier", 3, 0, 0));
                }
                else
                {
                    cards.Add(new ArmsCard("Fourth Tier", 4, 0, 0));
                }
            }

            return cards;
        }

        public static List<LegsCard> AllLegsCards()
        {
            var cards = new List<LegsCard>();

            for (var i = 0; i < TotalBodyCards; i++)
            {
                if (i < 26)
                {
                    cards.Add(new LegsCard("First Tier", 0, 1, 0));
                }
                else if (i < 44)
                {
                    cards.Add(new LegsCard("Second Tier", 0, 2, 0));
                }
                else if (i < 54)
                {
                    cards.Add(new LegsCard("Third Tier", 0, 3, 0));
                }
                else
                {
                    cards.Add(new LegsCard("Fourth Tier", 0, 4, 0));
                }
            }

            return cards;
        }

        public static List<TorsoCard> AllTorsoCards()
        {
            var cards = new List<TorsoCard>();

            for (var i = 0; i < TotalBodyCards; i++)
            {
                if (i < 26)
                {
                    cards.Add(new TorsoCard("First Tier", 0, 0, 0, 1));
                }
                else if (i < 44)
                {
                    cards.Add(new TorsoCard("Second Tier", 0, 0, 0, 1));
                }
                else if (i < 54)
                {
                    cards.Add(new TorsoCard("Third Tier", 0, 0, 0, 1));
                }
                else
                {
                    cards.Add(new TorsoCard("Fourth Tier", 0, 0, 0, 1));
                }
            }

            return cards;
        }

        public static List<WeaponCard> AllWeaponCards()
        {
            var cards = new List<WeaponCard>();

            for (var i = 0; i < 54; i++)
            {
                if (i < 16)
                {
                    cards.Add(new WeaponCard("First Tier", 1));
                }
                else if (i < 32)
                {
                    cards.Add(new WeaponCard("Second Tier", 2));
                }
                else if (i < 46)
                {
                    cards.Add(new WeaponCard("Third Tier", 3));
                }
                else
                {
                    cards.Add(new WeaponCard("Fourth Tier", 4));
                }
            }

            return cards;
        }

        public static List<SpecialCard> AllSpecialCards()
        {
            return new List<SpecialCard>()
            {
                new SpecialCard(SpecialType.OneTimeUse)
            };
        }
    }
}
