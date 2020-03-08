namespace DisposableHeroes.Domain
{
    public static class PhaseActions
    {
        public enum BuildPhaseActions
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


        public enum PreparePhaseEnums
        {

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
