using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using System.Collections.Generic;

namespace DisposableHeroes.Domain
{
    public static class PresetCards
    {
        public static List<HeadCard> AllHeadCards = new List<HeadCard>
        {
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0),
            new HeadCard("Tony Tiger", 0, 0, 0)
        };

        public static List<ArmsCard> AllArmsCards = new List<ArmsCard>
        {
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0),
            new ArmsCard("Tony Tiger", 0, 0, 0)
        };

        public static List<LegsCard> AllLegsCards = new List<LegsCard>
        {
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0),
            new LegsCard("Tony Tiger", 0, 0, 0)
        };

        public static List<TorsoCard> AllTorsoCards = new List<TorsoCard>
        {
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0),
            new TorsoCard("Tony Tiger", 0, 0, 0)
        };

        public static List<WeaponCard> AllWeaponCards = new List<WeaponCard>
        {
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1),
            new WeaponCard("", 1)
        };

        public static List<SpecialCard> AllSpecialCards = new List<SpecialCard>
        {
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse),
            new SpecialCard(SpecialType.OneTimeUse)
        };
    }
}
