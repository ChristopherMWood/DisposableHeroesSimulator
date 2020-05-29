using DisposableHeroes.Domain.Cards;
using System.Collections.Generic;
using System.Linq;
using System;
using DisposableHeroes.Domain.Constants;

namespace DisposableHeroes.Domain.Player
{
    public class Backpack
    {
        public List<ICard> Cards { get; private set; } = new List<ICard>();

        public bool StoreInBackpack(ICard card)
        {
            if (Cards.Count() < GameConstants.MaxBackpackCapacity)
            {
                Cards.Add(card);
                return true;
            }

            return false;
        }

        public ICard RemoveFromBackpack(ICard card)
        {
            var index = Cards.IndexOf(card);

            if (index >= 0)
            {
                var cardBeingRemoved = Cards[index];
                Cards.RemoveAt(index);

                return cardBeingRemoved;
            }

            return null;
        }
    }
}
