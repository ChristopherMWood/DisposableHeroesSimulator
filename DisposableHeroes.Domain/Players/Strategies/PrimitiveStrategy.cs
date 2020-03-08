using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class PrimitiveStrategy : IPlayerStrategy
    {
        public BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<BuildPhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public BuildPhaseActions EvaluateBuildPhaseDrawnCardAction(IEnumerable<BuildPhaseActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            return availableActions.First();
        }

        public AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }
    }
}
