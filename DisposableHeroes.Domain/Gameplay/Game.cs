using DisposableHeroes.Domain;
using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Gameplay;
using DisposableHeroes.Domain.Players;
using System.Collections.Generic;

namespace DisposableHeroes.Gameplay
{
    public class Game
    {
        public LinkedList<BasePlayer> Players { get; }
        public BasePlayer CurrentPlayer;

        public CardDeck<HeadCard> HeadsDeck = new CardDeck<HeadCard>();
        public CardDeck<ArmsCard> ArmsDeck = new CardDeck<ArmsCard>();
        public CardDeck<LegsCard> LegsDeck = new CardDeck<LegsCard>();
        public CardDeck<TorsoCard> TorsosDeck = new CardDeck<TorsoCard>();
        public CardDeck<SpecialCard> SpecialsDeck = new CardDeck<SpecialCard>();
        public CardDeck<WeaponCard> WeaponsDeck = new CardDeck<WeaponCard>();

        public int Round { get; } = 1;

        public Game(IEnumerable<BasePlayer> players)
        {
            foreach (var player in players)
            {
                Players.AddLast(player);
            }
        }

        public void PlayGameRound()
        { 
        
        }
    }
}
