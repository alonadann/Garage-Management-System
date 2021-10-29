using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        // Class members
        protected readonly float r_MaxEnergyCapacity;
        protected float m_CurrEnergyCapacity;

        // Properties
        public float CurrEnergyCapacity
        {
            get { return m_CurrEnergyCapacity; }
            set { m_CurrEnergyCapacity = value; }
        }

        public float MaxEnergyCapacity
        {
            get { return r_MaxEnergyCapacity; }
        }

        // Constructor
        protected Energy(float i_MaxEnergyCapacity, float i_CurrEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrEnergyCapacity = i_CurrEnergyCapacity;
        }

        // Abstract method for refilling all energy types (implemented in Fuel and Battery classes)
        public abstract void RefillEnergy(float i_EnergyToAdd, Fuel.eFuelType?i_FuelType);

        // Check if the amount of energy to add is valid (amount > 0 && new current <= max)
        protected bool IsValidEnergyToAdd(float i_EnergyToAdd)
        {
            return i_EnergyToAdd > 0 && m_CurrEnergyCapacity + i_EnergyToAdd <= r_MaxEnergyCapacity;
        }
    }
}