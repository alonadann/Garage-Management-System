using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class Motorcycle : Vehicle
    {
        // Enum for licence types of motorcycle
        public enum eLicenceType
        {
            A,
            A1,
            AA,
            B
        }

        // All constatnt values of motorcycle type of vehicle
        public struct Constants    
        {
            public const int k_NumOfWheels = 2;
            public const float k_MotorcycleMaxAirPressure = 30.0f;
            public const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;
            public const float k_MaxBatreryTime = 1.2f;
            public const float k_MaxFuelCapacity = 7f;
        }

        // Class methods
        private readonly eLicenceType r_LicenceType;
        private readonly int r_EngineCapacity;

        // Properties
        public eLicenceType LicenceType
        {
            get { return r_LicenceType; }
        }

        public int EngineCapacity
        {
            get { return r_EngineCapacity; }
        }

        // Constructor
        public Motorcycle(
                            string i_Model,
                            string i_VehicleLicenseNum,
                            float i_RemainEnergyPercent,
                            Wheel[] i_WheelsArr,
                            Energy i_EnergySource,
                            eLicenceType i_LicenceType,
                            int i_EngineCapacity)
                            : base(i_Model, i_VehicleLicenseNum, i_RemainEnergyPercent, i_WheelsArr, i_EnergySource)
        {
            r_LicenceType = i_LicenceType;
            r_EngineCapacity = i_EngineCapacity;
        }

        // Get extra parameters for motorcycle type of vehicle
        public static void GetMotorcycleRequiredParameters(Dictionary<string, Type> io_ParametersDic)
        {
            io_ParametersDic.Add("LicenceType", typeof(Motorcycle.eLicenceType));
            io_ParametersDic.Add("engine capacity", typeof(int));
        }

        // Overide ToString method
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(base.ToString() + Environment.NewLine);

            str.AppendFormat(
                @"License type: {0}
Engine Capacity: {1}", 
                r_LicenceType, 
                r_EngineCapacity);

            return str.ToString();
        }
    }
}