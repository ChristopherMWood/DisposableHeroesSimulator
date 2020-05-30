namespace DisposableHeroes.Domain.Constants
{
    public static class GameConstants
    {
        #region Gameplay Constants
        public const int AttackPhaseHealthGain = 4;
        public const int SuccessfulPerceptionAttackDamage = 2;
        public const int DamageFromPlayerRemovingBodyPart = 2;
        public const int MinimumNumberOfPlayersRequiredForBuildPhase = 3;
        public const bool DealSpecialCardsOnFirstRound = true;
        public const bool SetStartingPlayerAsOneWithLowestHealth = true;
        public const int MaxRoundsBeforeInvalidGame = 100;
        #endregion

        #region Player Constants
        public const int MinimumSkillForBetterDice = 4;
        public const int MinimumSkillForBestDice = 8;
        public const int InitialPlayerHealth = 20;
        public const int MaxPlayerHealth = 25;
        public const int BaseStrengthStat = 4;
        public const int BaseAgilityStat = 4;
        public const int BasePerceptionStat = 4;
        public const int MaxStatValue = 10;
        public const int MaxBackpackCapacity = 3;
        #endregion
    }
}