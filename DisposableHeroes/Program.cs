using DisposableHeroes.Domain;
using DisposableHeroes.Domain.Players;
using DisposableHeroes.Gameplay;
using System.Collections.Generic;

namespace DisposableHeroes
{
    public static class Program
    {
        public static void Main()
        {
            var players = new List<BasePlayer>()
            {
                new BasePlayer("Player One"),
                new BasePlayer("Player Two"),
                new BasePlayer("Player Three"),
                new BasePlayer("Player Four"),
                new BasePlayer("Player Five"),
                new BasePlayer("Player Six")
            };

            var game = new Game(players);

            game.HeadsDeck.AddToDeck(PresetCards.AllHeadCards);
            game.ArmsDeck.AddToDeck(PresetCards.AllArmsCards);
            game.LegsDeck.AddToDeck(PresetCards.AllLegsCards);
            game.TorsosDeck.AddToDeck(PresetCards.AllTorsoCards);
            game.WeaponsDeck.AddToDeck(PresetCards.AllWeaponCards);
            game.SpecialsDeck.AddToDeck(PresetCards.AllSpecialCards);
            game.CurrentPlayer = players[0];
        }
    }
}
