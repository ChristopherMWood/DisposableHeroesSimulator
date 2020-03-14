using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public interface IPlayerStrategy
    {
        BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<BuildPhaseActions> availableActions, BasePlayer player, Game game);
        //BuildPhaseActions EvaluatePreparePhaseAction(IEnumerable<PreparePhaseActions> availableActions, BasePlayer player, Game game, ICard card);
        AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, BasePlayer player, Game game);
        BuildPhaseActions EvaluateDrawnCardAction(IEnumerable<PhaseActions.BuildPhaseActions> availableActions, BasePlayer player, Game game, ICard card);
    }
}
