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

        public PhaseActions.CardActions EvaluateBuildPhaseAction(IEnumerable<PhaseActions.CardActions> availableActions, BasePlayer player, Game game)
        {
            if (availableActions.Contains(PhaseActions.CardActions.DrawArmsCard))
                return PhaseActions.CardActions.DrawArmsCard;
            else if (availableActions.Contains(PhaseActions.CardActions.DrawWeaponCard))
                return PhaseActions.CardActions.DrawWeaponCard;

            return availableActions.First();
        }

        public PhaseActions.CardActions EvaluateDrawnCardAction(IEnumerable<PhaseActions.CardActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            if (availableActions.Contains(PhaseActions.CardActions.EquiptCard) && NewCardIsStrongerThanCurrent(player, card))
                return PhaseActions.CardActions.EquiptCard;

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

        public PhaseActions.CardActions EvaluateDrawCardAction(IEnumerable<PhaseActions.CardActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public PhaseActions.PreparePhaseActions EvaluatePreparePhaseAction(IEnumerable<PhaseActions.PreparePhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public ICard UnequipCardAction(BasePlayer player)
        {
            throw new System.NotImplementedException();
        }

        public ICard EquipCardFromBackpack(BasePlayer player)
        {
            throw new System.NotImplementedException();
        }

        public BasePlayer ChoosePlayerToAttack(PhaseActions.AttackPhaseActions chosenAction, BasePlayer player, Game game)
        {
            throw new System.NotImplementedException();
        }

        public PhaseActions.SuccessfulPerceptionAttackActions SuccessfulPerceptionAttack(PhaseActions.SuccessfulPerceptionAttackActions action, BasePlayer attackingPlayer, BasePlayer defendingPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }

        public ICard LootPlayerAction(PhaseActions.LootPlayerActions availableActions, BasePlayer player, BasePlayer defeatedPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}
