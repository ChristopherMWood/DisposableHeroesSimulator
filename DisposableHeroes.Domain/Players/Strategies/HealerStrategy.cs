using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class HealerStrategy : IPlayerStrategy
    {
        public BasePlayer ChoosePlayerToAttack(AttackPhaseActions chosenAction, BasePlayer player, Game game)
        {
            return null;
        }

        public ICard EquipCardFromBackpack(BasePlayer player)
        {
            return null;
        }

        public AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, BasePlayer player, Game game)
        {
            if (game.Players.Count > 2)
                return PhaseActions.AttackPhaseActions.Heal;

            return PhaseActions.AttackPhaseActions.StrengthAttack;
        }

        public PhaseActions.CardActions EvaluateBuildPhaseAction(IEnumerable<PhaseActions.CardActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public PhaseActions.CardActions EvaluateDrawCardAction(IEnumerable<PhaseActions.CardActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public CardActions EvaluateDrawnCardAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            return availableActions.First();
        }

        public PreparePhaseActions EvaluatePreparePhaseAction(IEnumerable<PreparePhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public ICard LootPlayerAction(LootPlayerActions availableActions, BasePlayer player, BasePlayer defeatedPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }

        public SuccessfulPerceptionAttackActions SuccessfulPerceptionAttack(SuccessfulPerceptionAttackActions action, BasePlayer attackingPlayer, BasePlayer defendingPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }

        public ICard UnequipCardAction(BasePlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}
