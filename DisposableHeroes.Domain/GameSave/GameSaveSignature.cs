using System;

namespace DisposableHeroes.Domain.GameSave
{
    public class GameSaveSignature: ISaveComponent
    {
        public DateTime GameStartedAt;
        public DateTime GameEndedAt;
        public bool IsFinished;
        public string Remarks;
        public string ID;

        public string Print()
        {
            throw new System.NotImplementedException();
        }
    }
}