using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        // Enum for different color options for car
        public enum eColor
        {
            Red = 1,
            White,
            Black,
            Silver
        }

        // Enum for different number of doors options for car
        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        // All constatnt values of car type of vehicle
        public struct Constants
        {
            public const int k_NumOfWheels = 4;
            public const float k_CarMaxAirPressure = 32f;
            public const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan96;
            public const float k_MaxBatreryTime = 2.1f;
            public const float k_MaxFuelCapacity = 60f;
        }

        // Class members
        private readonly eNumOfDoors r_NumOfDoors;
        private readonly eColor r_Color;

        // Properties
        public eNumOfDoors NumOfDoors
        {
            get { return r_NumOfDoors; }
        }

        public eColor Color
        {
            get { return r_Color; }
        }

        // Constructor
        public Car(
                    string i_Model,
                    string i_VehicleLicenseNum,
                    float i_RemainEnergyPercent,
                    Wheel[] i_WheelsArr,
                    Energy i_EnergySource,
                    eNumOfDoors i_NumOfDoors,
                    eColor i_Color)
                    : base(i_Model, i_VehicleLicenseNum, i_RemainEnergyPercent, i_WheelsArr, i_EnergySource)
        {
            r_NumOfDoors = i_NumOfDoors;
            r_Color = i_Color;
        }

        // Get extra parameters for car type of vehicle
        public static void GetCarRequiredParameters(Dictionary<string, Type> io_ParametersDic)
        {
            io_ParametersDic.Add("NumOfDoors", typeof(Car.eNumOfDoors));
            io_ParametersDic.Add("Color", typeof(Car.eColor));
        }

        // Overide ToString method
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(base.ToString() + Environment.NewLine);

            str.AppendFormat(
                @"Num of doors : {0}
color: {1}",
                r_NumOfDoors,
                r_Color);

            return str.ToString();
        }
    }
}