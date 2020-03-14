namespace DisposableHeroes.Domain
{
    public static class PhaseActions
    {
        public enum BuildPhaseActions
        {
            EquiptCard,
            StoreCardInBackpack,
            DiscardCard,
            DrawHeadCard,
            DrawArmsCard,
            DrawTorsoCard,
            DrawLegsCard,
            DrawWeaponCard,
            DrawSpecialCard
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
    }
}
