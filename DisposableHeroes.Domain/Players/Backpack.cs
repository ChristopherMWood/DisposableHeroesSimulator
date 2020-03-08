using DisposableHeroes.Domain.Cards;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Player
{
    public class Backpack
    {
        public List<ICard> Cards { get; private set; } = new List<ICard>();

        public void StoreInBackpack(ICard card)
        {
            if (Cards.Count() < 3)
                Cards.Add(card);
        }

        public ICard RemoveFromBackpack(ICard card)
        {
            var index = Cards.IndexOf(card);
            var cardBeingRemoved = Cards[index];
            Cards.RemoveAt(index);

            return cardBeingRemoved;
        }
    }
}
