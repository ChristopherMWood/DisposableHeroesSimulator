namespace DisposableHeroes.Domain
{
    public static class PhaseActions
    {
        public enum CardActions
        {
            DrawHeadCard,
            DrawArmsCard,
            DrawTorsoCard,
            DrawLegsCard,
            DrawWeaponCard,
            DrawSpecialCard,
            EquiptCard,
            StoreCardInBackpack,
            DiscardCard
        }

        public enum PreparePhaseActions
        {
            UnequipCard,
            EquiptCardFromBackpack,
            DoNothing
        }

        public enum AttackPhaseActions
        {
            StrengthAttack,
            PerceptionAttack,
            Heal,
            RollForCard
        }

        public enum SuccessfulPerceptionAttackActions
        { 
            RemoveHead,
            RemoveArms,
            RemoveTorso,
            RemoveLegs,
            RemoveWeapon,
            RemoveSpecial
        }

        public enum LootPlayerActions
        {
            DrawHeadCard,
            DrawArmsCard,
            DrawTorsoCard,
            DrawLegsCard,
            DrawWeaponCard,
            DrawSpecialCard,
            EquiptCard,
            StoreCardInBackpack,
            DiscardCard
        }
    }
}
