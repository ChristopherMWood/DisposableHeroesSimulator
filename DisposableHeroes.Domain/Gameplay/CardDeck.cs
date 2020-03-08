using DisposableHeroes.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisposableHeroes.Domain.Gameplay
{
    public class CardDeck<T> where T: ICard
    {
        private Stack<T> deck = new Stack<T>();

        public CardDeck()
        {
        }

        public T Draw()
        {
            return deck.Pop();
        }

        public void AddToDeck(T card)
        {
            deck.Push(card);
        }

        public void AddToDeck(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                deck.Push(card);
            }
        }

        public void Shuffle()
        {
            var cardsArray = deck.ToList();
            Random random = new Random();

            for (var i = cardsArray.Count; i > 0; i--)
                cardsArray = Swap(cardsArray, 0, random.Next(0, i));

            deck.Clear();

            AddToDeck(cardsArray);
        }

        private List<T> Swap(List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;

            return list;
        }
    }
}
