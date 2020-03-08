using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class StrengthStrategy : IPlayerStrategy
    {
        public PhaseActions.AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<PhaseActions.AttackPhaseActions> availableActions, BasePlayer player, Game game)
        {
            if (availableActions.Contains(PhaseActions.AttackPhaseActions.StrengthAttack))
                return PhaseActions.AttackPhaseActions.StrengthAttack;

            return availableActions.First();
        }

        public PhaseActions.BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<PhaseActions.BuildPhaseActions> availableActions, BasePlayer player, Game game)
        {
            if (availableActions.Contains(PhaseActions.BuildPhaseActions.DrawArmsCard))
                return PhaseActions.BuildPhaseActions.DrawArmsCard;
            else if (availableActions.Contains(PhaseActions.BuildPhaseActions.DrawWeaponCard))
                return PhaseActions.BuildPhaseActions.DrawWeaponCard;

            return availableActions.First();
        }

        public PhaseActions.BuildPhaseActions EvaluateBuildPhaseDrawnCardAction(IEnumerable<PhaseActions.BuildPhaseActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            if (availableActions.Contains(PhaseActions.BuildPhaseActions.EquiptCard) && NewCardIsStrongerThanCurrent(player, card))
                return PhaseActions.BuildPhaseActions.EquiptCard;

            return availableActions.First();
        }

        private bool NewCardIsStrongerThanCurrent(BasePlayer player, ICard card)
        {
            if (card is ArmsCard)
            {
                return player.Arms == null || player.Arms.Strength < (card as ArmsCard).Strength;
            }
            else if (card is WeaponCard)
            {
                return player.Weapon == null || player.Weapon.Damage < (card as WeaponCard).Damage;
            }

            return false;
        }

        public override string ToString()
        {
            return "Strength Strategy";
        }
    }
}
