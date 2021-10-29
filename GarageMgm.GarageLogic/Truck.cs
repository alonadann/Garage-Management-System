using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        // Class members
        private readonly float r_CargoCapacity;
        private bool m_IsDangerous;

        // All constatnt values of truck type of vehicle
        public struct Constants 
        {
            public const int k_NumOfWheels = 16;
            public const float k_TruckMaxAirPressure = 28.0f;
            public const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;
            public const float k_MaxFuelCapacity = 120f;
        }

        // Properties
        public bool IsDangerous
        {
            get { return m_IsDangerous; }
            set { m_IsDangerous = value; }
        }

        public float CargoCapacity
        {
            get { return r_CargoCapacity; }
        }

        // Constructor
        public Truck(
                    string i_Model,
                    string i_VehicleLicenseNum,
                    float i_RemainEnergyPercent,
                    Wheel[] i_WheelsArr,
                    bool i_IsDangerous,
                    float i_CargoCapacity,
                    Fuel i_TruckFuel)
                    : base(i_Model, i_VehicleLicenseNum, i_RemainEnergyPercent, i_WheelsArr, i_TruckFuel)
        {
            m_IsDangerous = i_IsDangerous;
            r_CargoCapacity = i_CargoCapacity;
        }

        // Get extra parameters for truck type of vehicle
        public static void GetTruckRequiredParameters(Dictionary<string, Type> io_ParametersDic)
        {
            io_ParametersDic.Add("IsDangerous", typeof(bool));
            io_ParametersDic.Add("cargo capacity", typeof(float));
        }

        // Overide ToString method
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(base.ToString() + Environment.NewLine);

            str.AppendFormat(
                @"Is dangerous? {0}
cargo capacity : {1:F2}", 
                m_IsDangerous, 
                r_CargoCapacity);

            return str.ToString();
        }
    }
}
