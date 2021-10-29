using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Fuel : Energy
    {
        // Enum for different types of fuel
        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler
        }

        // Class members
        private readonly eFuelType r_FuelType;

        // Properties
        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        // Constructor
        public Fuel(float i_MaxEnergyCapacity, float i_CurrEnergyCapacity, eFuelType i_FuelType) : base(i_MaxEnergyCapacity, i_CurrEnergyCapacity)
        {
            r_FuelType = i_FuelType;
        }

        // Refuel a fuel-powerd vehicle (add the given amount to the current amount of enery)
        public override void RefillEnergy(float i_EnergyToAdd, Fuel.eFuelType? i_FuelType)
        {
            if (r_FuelType != i_FuelType)
            {
                throw new ArgumentException();
            }

            if (!IsValidEnergyToAdd(i_EnergyToAdd))
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyCapacity);
            }

            m_CurrEnergyCapacity += i_EnergyToAdd;
        }

        // Overide ToString method
        public override string ToString()
        {
            return string.Format(@"Current fuel capacity: {0:F2}, Fuel type: {1}", m_CurrEnergyCapacity, FuelType);
        }
    }
}
