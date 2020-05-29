using DisposableHeroes.Domain.Cards;
using DisposableHeroes.Domain.Cards.BodyParts;
using DisposableHeroes.Domain.Gameplay;

namespace DisposableHeroes.Domain.GameSave
{
    public class GameSaveInitialDeck: ISaveComponent
    {
        public CardDeck<HeadCard> HeadCardDeck;
        public CardDeck<ArmsCard> ArmsCardDeck;
        public CardDeck<LegsCard> LegsCardDeck;
        public CardDeck<TorsoCard> TorsoCardDeck;
        public CardDeck<SpecialCard> SpecialCardDeck;
        public CardDeck<WeaponCard> WeaponCardDeck;
        public CardDeck<ICard> DiscardDeck;
        public string Print()
        {
            throw new System.NotImplementedException();
        }
    }
}