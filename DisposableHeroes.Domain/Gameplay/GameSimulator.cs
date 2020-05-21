using DisposableHeroes.Domain.Players;
using DisposableHeroes.Domain.Stats;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Gameplay
{
    public static class GameSimulator
    {
        public static GameSummary SimulateGame(List<BasePlayer> players, BasePlayer startingPlayer)
        {
            var game = new Game(players);
            var round = 0;

            game.SetStartingPlayer(startingPlayer);

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards());
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards());
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards());
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards());
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards());
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards());

            while (game.State != GameState.GameEnded)
            {
                round++;

                if (round > 100)
                {
                    Console.WriteLine("GAME ENDED PREMATURELY (Infinite Game Loop Detected)");
                    return null;
                }

                if (round == 1)
                    game.GiveAllPlayersSpecialCard();

                if (game.Players.Count > 2)
                    game.PlayBuildRound();

                game.PlayPrepareRound();
                game.PlayAttackRound();

                game.CheckForGameOver();

                game.SetStartingPlayerAsOneWithLowestHealth();
            }

            return new GameSummary()
            {
                WinningPlayer = game.Players.Count > 0 ? game.Players.First(): null,
                RoundsInGame = round
            };
        }
    }
}
