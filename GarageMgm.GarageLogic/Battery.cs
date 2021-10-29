using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
  public class Battery : Energy
  {
        // Constructor
        public Battery(float i_MaxBatreryTime, float i_RemainingBatreryTime) : base(i_MaxBatreryTime, i_RemainingBatreryTime)
        {
        }

        // Refuel a battery-powerd vehicle (add the given amount to the current amount of enery)
        public override void RefillEnergy(float i_MinutesToAdd, Fuel.eFuelType? i_FuelType = null)
        {
            if(i_FuelType != null)
            {
                throw new ArgumentException();
            }

            float hoursToAdd = i_MinutesToAdd / 60;

            if(!IsValidEnergyToAdd(hoursToAdd))
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyCapacity);
            }

            m_CurrEnergyCapacity += hoursToAdd;
        }

        // Overide ToString method
        public override string ToString()
        {
            return string.Format(@"Current battery capacity: {0:F2}", m_CurrEnergyCapacity);
        }
    }
}