using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportCheckInSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int economyPassengers = 100;
            int businessPassengers = 20;
            int economyCounters = 5;
            int businessCounters = 5;
            int timePerEconomyPassenger = 10; // minutes
            int timePerBusinessPassenger = 7; // minutes

            // Level 1 Simulation
            var level1Simulation = new CheckInSimulation(
                economyPassengers, businessPassengers, economyCounters, businessCounters,
                timePerEconomyPassenger, timePerBusinessPassenger, false);
            var level1TotalTime = level1Simulation.CalculateTotalTime();
            Console.WriteLine($"Level 1 Total Time: {level1TotalTime} minutes");

            // Level 2 Simulation
            var level2Simulation = new CheckInSimulation(
                economyPassengers, businessPassengers, economyCounters, businessCounters,
                timePerEconomyPassenger, timePerBusinessPassenger, true);
            var level2TotalTime = level2Simulation.CalculateTotalTime();
            Console.WriteLine($"Level 2 Total Time: {level2TotalTime} minutes");
        }
    }

    public class CheckInSimulation
    {
        private int economyPassengers;
        private int businessPassengers;
        private int economyCounters;
        private int businessCounters;
        private int timePerEconomyPassenger;
        private int timePerBusinessPassenger;
        private bool canReallocateBusinessCounters;

        public CheckInSimulation(
            int economyPassengers, int businessPassengers, int economyCounters, int businessCounters,
            int timePerEconomyPassenger, int timePerBusinessPassenger, bool canReallocateBusinessCounters)
        {
            this.economyPassengers = economyPassengers;
            this.businessPassengers = businessPassengers;
            this.economyCounters = economyCounters;
            this.businessCounters = businessCounters;
            this.timePerEconomyPassenger = timePerEconomyPassenger;
            this.timePerBusinessPassenger = timePerBusinessPassenger;
            this.canReallocateBusinessCounters = canReallocateBusinessCounters;
        }

        public int CalculateTotalTime()
        {
            int businessCheckInTime = (businessPassengers + businessCounters - 1) / businessCounters * timePerBusinessPassenger;
            int remainingEconomyPassengers = economyPassengers;

            if (canReallocateBusinessCounters)
            {
                // Calculate time for business class check-in
                int economyCheckInTimeWithReallocation = (remainingEconomyPassengers + (economyCounters + businessCounters) - 1) / (economyCounters + businessCounters) * timePerEconomyPassenger;

                // Total time is max of business class check-in time and reallocated economy check-in time
                return Math.Max(businessCheckInTime, economyCheckInTimeWithReallocation);
            }
            else
            {
                // Calculate time for economy class check-in
                int economyCheckInTimeWithoutReallocation = (remainingEconomyPassengers + economyCounters - 1) / economyCounters * timePerEconomyPassenger;

                // Total time is max of economy and business class check-in times
                return Math.Max(economyCheckInTimeWithoutReallocation, businessCheckInTime);
            }
        }
    }
}
