using System.Collections.Generic;

namespace DisposableHeroes.Domain.GameSave
{
    public class GameSaveInitialPlayer: ISaveComponent
    {
        public LinkedList<Players.Player> Players;
        public string Print()
        {
            throw new System.NotImplementedException();
        }
    }
}