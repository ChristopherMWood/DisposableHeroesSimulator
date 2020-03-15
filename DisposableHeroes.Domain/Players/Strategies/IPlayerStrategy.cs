using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;
using static DisposableHeroes.Domain.PhaseActions;

namespace DisposableHeroes.Domain.Players.Strategies
{
    public interface IPlayerStrategy
    {
        CardActions EvaluateDrawCardAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game);
        CardActions EvaluateDrawnCardAction(IEnumerable<CardActions> availableActions, BasePlayer player, Game game, ICard card);
        PreparePhaseActions EvaluatePreparePhaseAction(IEnumerable<PreparePhaseActions> availableActions, BasePlayer player, Game game);
        ICard UnequipCardAction(BasePlayer player);
        ICard EquipCardFromBackpack(BasePlayer player);
        AttackPhaseActions EvaluateAttackPhaseAction(IEnumerable<AttackPhaseActions> availableActions, BasePlayer player, Game game);
        BasePlayer ChoosePlayerToAttack(AttackPhaseActions chosenAction, BasePlayer player, Game game);
        SuccessfulPerceptionAttackActions SuccessfulPerceptionAttack(SuccessfulPerceptionAttackActions actions, BasePlayer attackingPlayer, BasePlayer defendingPlayer, Game game);
        ICard LootPlayerAction(LootPlayerActions availableActions, BasePlayer player, BasePlayer defeatedPlayer, Game game);
    }
}
