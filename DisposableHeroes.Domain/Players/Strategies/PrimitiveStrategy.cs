using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class PrimitiveStrategy : IPlayerStrategy
    {
        public CardActions EvaluateBuildPhaseAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public CardActions EvaluateDrawnCardAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            return availableActions.First();
        }

        public AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public CardActions EvaluateDrawCardAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public PreparePhaseActions EvaluatePreparePhaseAction(IEnumerable<PreparePhaseActions> availableActions, BasePlayer player, Game game)
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

        public BasePlayer ChoosePlayerToAttack(AttackPhaseActions chosenAction, BasePlayer player, Game game)
        {
            throw new System.NotImplementedException();
        }

        public SuccessfulPerceptionAttackActions SuccessfulPerceptionAttack(SuccessfulPerceptionAttackActions action, BasePlayer attackingPlayer, BasePlayer defendingPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }

        public ICard LootPlayerAction(LootPlayerActions availableActions, BasePlayer player, BasePlayer defeatedPlayer, Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}
