using DisposableHeroes.Domain;
using DisposableHeroes.Domain.Dice;
using DisposableHeroes.Domain.Players;
using DisposableHeroes.Domain.Players.Strategies;
using DisposableHeroes.Gameplay;
using System;
using System.Collections.Generic;

namespace DisposableHeroes
{
    public static class Program
    {
        public static void Main()
        {
            var players = new List<BasePlayer>()
            {
                new BasePlayer("Player One", new PrimitiveStrategy()),
                new BasePlayer("Player Two", new PrimitiveStrategy()),
                new BasePlayer("Player Three", new PrimitiveStrategy()),
                new BasePlayer("Player Four", new PrimitiveStrategy()),
                new BasePlayer("Player Five", new PrimitiveStrategy()),
                new BasePlayer("Player Six", new PrimitiveStrategy())
            };

            var game = new Game(players);

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards);
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards);
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards);
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards);
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards);
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards);
            game.SetStartingPlayer(players[0]);

            game.PlayBuildRound();

            Console.ReadLine();
        }
    }
}
