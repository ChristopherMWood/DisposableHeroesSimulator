using System.Collections.Generic;

namespace DisposableHeroes
{
    public class SimulationSummary
    {
        public int NumberOfSimulations;
        public SortedDictionary<string, double> StrategiesUsed = new SortedDictionary<string, double>();
        public SortedDictionary<string, double> PlayerWinsByName = new SortedDictionary<string, double>();
        public double TotalRounds;
        public double TotalHealth;
        public double TotalStrength;
        public double TotalAgility;
        public double TotalPerception;
        public double TotalWeaponDamage;
    }
}
