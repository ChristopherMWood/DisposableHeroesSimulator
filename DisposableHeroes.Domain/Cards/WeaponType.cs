namespace DisposableHeroes.Domain.Cards
{
    public enum WeaponType
    {
        Normal,                                     // implemented
        CanEquipOneMorePermanentWeaponOrSpecial,    // NOT implemented
        DealWeaponDamageBackOnEnemyAttack,          // implemented
        BlockAllWeaponDamage,                       // implemented
        IgnoreEnemyDefense,                         // implemented
        DoubleDamageIfEnemyHasNoWeapon              // implemented
    }
}