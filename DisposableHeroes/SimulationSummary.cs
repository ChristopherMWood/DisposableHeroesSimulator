using System.Collections.Generic;

namespace DisposableHeroes
{
    public class SimulationSummary
    {
        public int NumberOfSimulations;
        public Dictionary<string, int> StrategiesUsed = new Dictionary<string, int>();
        public double TotalRounds;
        public double TotalHealth;
        public double TotalStrength;
        public double TotalAgility;
        public double TotalPerception;
    }
}
