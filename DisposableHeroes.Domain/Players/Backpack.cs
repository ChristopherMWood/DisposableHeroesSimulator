using DisposableHeroes.Domain.Cards;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DisposableHeroes.Domain.Player
{
    public class Backpack
    {
        public List<ICard> Cards { get; private set; } = new List<ICard>();

        public bool StoreInBackpack(ICard card)
        {
            if (Cards.Count() < 3)
            {
                Cards.Add(card);
                return true;
            }

            return false;
        }

        public ICard RemoveFromBackpack(ICard card)
        {
            var index = Cards.IndexOf(card);
            var cardBeingRemoved = Cards[index];
            Cards.RemoveAt(index);

            return cardBeingRemoved;
        }

        // public ICard RemoveRandomFromBackpack()
        // {
        //     if (Cards.Count > 0)
        //     {
        //         var rand = new Random();
        //         var index = rand.Next(0, Cards.Count);
        //         var cardBeingRemoved = Cards[index];
        //         Cards.RemoveAt(index);

        //         return cardBeingRemoved;
        //     }
        // }
    }
}
