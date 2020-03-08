using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public class HealerStrategy : IPlayerStrategy
    {
        public PhaseActions.AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<PhaseActions.AttackPhaseActions> availableActions, BasePlayer player, Game game)
        {
            if (game.Players.Count > 2)
                return PhaseActions.AttackPhaseActions.Heal;

            return PhaseActions.AttackPhaseActions.StrengthAttack;
        }

        public PhaseActions.BuildPhaseActions EvaluateBuildPhaseAction(IEnumerable<PhaseActions.BuildPhaseActions> availableActions, BasePlayer player, Game game)
        {
            return availableActions.First();
        }

        public PhaseActions.BuildPhaseActions EvaluateBuildPhaseDrawnCardAction(IEnumerable<PhaseActions.BuildPhaseActions> availableActions, BasePlayer player, Game game, ICard card)
        {
            return availableActions.First();
        }

        public override string ToString()
        {
            return "Healer Strategy";
        }
    }
}
