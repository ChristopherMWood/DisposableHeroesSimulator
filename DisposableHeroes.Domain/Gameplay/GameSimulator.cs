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
            game.SetStartingPlayer(startingPlayer);

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards());
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards());
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards());
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards());
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards());
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards());

            var round = 1;

            while (game.Players.Count > 1)
            {
                if (round > 100)
                {
                    Console.WriteLine("GAME ENDED PREMATURELY (Infinite Game Loop Detected)");
                    return null;
                }

                game.PlayBuildRound();
                game.PlayPrepareRound();
                game.PlayAttackRound();
                game.SetStartingPlayerAsOneWithLowestHealth();

                round++;
            }

            return new GameSummary()
            {
                WinningPlayer = game.Players.First(),
                RoundsInGame = round
            };
        }
    }
}
