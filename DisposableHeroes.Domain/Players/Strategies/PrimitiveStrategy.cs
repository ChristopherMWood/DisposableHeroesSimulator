using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class PrimitiveStrategy : IPlayerStrategy
    {
        public BuildPhaseEnums.BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<BuildPhaseEnums.BuildPhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public BuildPhaseEnums.BuildPhaseActions EvaluateBuildPhaseDrawnCardAction(IEnumerable<BuildPhaseEnums.BuildPhaseActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            return availableActions.First();
        }
    }
}
