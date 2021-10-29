using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        // Class members
        private readonly string r_ManufactureName;
        private readonly float r_MaxAirPressure;
        private float m_CurrAirPressure;

        // Properties
        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public float CurrAirPressure
        {
            get { return m_CurrAirPressure; }
            set { m_CurrAirPressure = value; }
        }

        public string ManufactureName
        {
            get { return r_ManufactureName; }
        }

        // Constructor
        public Wheel(string i_ManufactureName, float i_CurrAirPressure, float i_MaxAirPressure)
        {
            r_ManufactureName = i_ManufactureName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrAirPressure = i_CurrAirPressure;
        }

        // Gets an amount of air to add, and inlates the wheel if the air amount is valid
        public void InflateWheel(float i_AirAmountToAdd)
        {
            if(i_AirAmountToAdd < 0 || m_CurrAirPressure + i_AirAmountToAdd > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure);
            }

            m_CurrAirPressure += i_AirAmountToAdd;
        }

        // Overide ToString method
        public override string ToString()
        {
            return string.Format(@"Manufacturer: {0}, Air Pressure: {1:F2}", r_ManufactureName, m_CurrAirPressure);
        }
    }
}
