using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        // Enum for types of vehicles supported in the systems
        public enum eTypeOfVehicle
        {
            Motorcycle = 1,
            Car,
            Truck
        }

        // Enum for types energy of vehicles
        public enum eEnergyType
        {
           Fuel = 1,
           Battery
        }

        // Get required parameters for each type of vehicle and add it to the dictionary
        public static void GetRequiredVehicleParameters(int i_UserVehicleTypeChoice, Dictionary<string, Type> io_ParametersTypeDic)
        {
            eTypeOfVehicle vehicleType = (eTypeOfVehicle)i_UserVehicleTypeChoice;

            switch(vehicleType)
            {
                case eTypeOfVehicle.Car:
                    Car.GetCarRequiredParameters(io_ParametersTypeDic);
                    break;
                case eTypeOfVehicle.Motorcycle:
                    Motorcycle.GetMotorcycleRequiredParameters(io_ParametersTypeDic);
                    break;
                case eTypeOfVehicle.Truck:
                    Truck.GetTruckRequiredParameters(io_ParametersTypeDic);
                    break;
            }
        }

        // Get wheels data for each type of vehicle
        public static void GetVehicleWheelsData(int i_UserVehicleTypeChoice, out float o_MaxAirPressure, out int o_WheelsNum)
        {
            eTypeOfVehicle vehicleType = (eTypeOfVehicle)i_UserVehicleTypeChoice;
            o_WheelsNum = 0;
            o_MaxAirPressure = 0;

            switch(vehicleType)
            {
                case eTypeOfVehicle.Car:
                    o_WheelsNum = Car.Constants.k_NumOfWheels;
                    o_MaxAirPressure = Car.Constants.k_CarMaxAirPressure;
                    break;
                case eTypeOfVehicle.Motorcycle:
                    o_WheelsNum = Motorcycle.Constants.k_NumOfWheels;
                    o_MaxAirPressure = Motorcycle.Constants.k_MotorcycleMaxAirPressure;
                    break;
                case eTypeOfVehicle.Truck:
                    o_WheelsNum = Truck.Constants.k_NumOfWheels;
                    o_MaxAirPressure = Truck.Constants.k_TruckMaxAirPressure;
                    break;
            }
        }

        // Create an energy source and a new vehicle according to it's type
        public static Vehicle CreateVehicle(int i_UserVehicleTypeChoice, int i_UserEnergyTypeChoice, Dictionary<string, object> i_UserParamenters)
        {
            eTypeOfVehicle vehicleType = (eTypeOfVehicle)i_UserVehicleTypeChoice;
            eEnergyType EnergyType = (eEnergyType)i_UserEnergyTypeChoice;
            float currEnergyCapacityPercent = (float)i_UserParamenters["remain energy percent"];
            Energy energySource;
            Vehicle newVehicle = null;

            switch(vehicleType)
            {
                case eTypeOfVehicle.Car:
                    energySource = CreateEnergySource(
                                        EnergyType,
                                        currEnergyCapacityPercent,
                                        Car.Constants.k_MaxFuelCapacity,
                                        Car.Constants.k_MaxBatreryTime,
                                        Car.Constants.k_FuelType);
                    newVehicle = CreateNewCar(i_UserParamenters, energySource);
                    break;
                case eTypeOfVehicle.Motorcycle:
                    energySource = CreateEnergySource(
                                        EnergyType,
                                        currEnergyCapacityPercent,
                                        Motorcycle.Constants.k_MaxFuelCapacity,
                                        Motorcycle.Constants.k_MaxBatreryTime,
                                        Motorcycle.Constants.k_FuelType);
                    newVehicle = CreateNewMotorcycle(i_UserParamenters, energySource);
                    break;
                case eTypeOfVehicle.Truck:
                    energySource = CreateEnergySource(
                                                    eEnergyType.Fuel,
                                                    currEnergyCapacityPercent,
                                                    Truck.Constants.k_MaxFuelCapacity, 
                                                    0,
                                                    Truck.Constants.k_FuelType);
                    newVehicle = CreateNewTruck(i_UserParamenters, energySource);
                    break;
            }

            return newVehicle;
        }

        // Create new car
        public static Car CreateNewCar(Dictionary<string, object> i_UserParamenters, Energy i_EnergySource)
        {
            return new Car(
                            (string)i_UserParamenters["Model"],
                            (string)i_UserParamenters["vehicle license number"],
                            (float)i_UserParamenters["remain energy percent"],
                            (Wheel[])i_UserParamenters["WheelsArr"],
                            i_EnergySource,
                            (Car.eNumOfDoors)Enum.GetValues(typeof(Car.eNumOfDoors)).GetValue((int)i_UserParamenters["NumOfDoors"] - 1),
                            (Car.eColor)Enum.GetValues(typeof(Car.eColor)).GetValue((int)i_UserParamenters["Color"] - 1));
        }

        // Create new motercycle
        public static Motorcycle CreateNewMotorcycle(Dictionary<string, object> i_UserParamenters, Energy i_EnergySource)
        {
            return new Motorcycle(
                                   (string)i_UserParamenters["Model"],
                                   (string)i_UserParamenters["vehicle license number"],
                                   (float)i_UserParamenters["remain energy percent"],
                                   (Wheel[])i_UserParamenters["WheelsArr"],
                                   i_EnergySource,
                                   (Motorcycle.eLicenceType)Enum.GetValues(typeof(Motorcycle.eLicenceType)).GetValue((int)i_UserParamenters["LicenceType"] - 1),
                                   (int)i_UserParamenters["engine capacity"]);
        }

        // Create new truck
        public static Truck CreateNewTruck(Dictionary<string, object> i_UserParamenters, Energy i_EnergySource)
        {
            return new Truck(
                             (string)i_UserParamenters["Model"],
                             (string)i_UserParamenters["vehicle license number"],
                             (float)i_UserParamenters["remain energy percent"],
                             (Wheel[])i_UserParamenters["WheelsArr"],
                             (bool)i_UserParamenters["IsDangerous"],
                             (float)i_UserParamenters["cargo capacity"],
                             (Fuel)i_EnergySource);
        }

        // Calculate current energy capacity from given percent
        public static float CalculateCurrEnergyCapacity(float i_MaxEnergyCapacity, float i_CurrEnergyCapacityPercent)
        {
            return (i_CurrEnergyCapacityPercent * i_MaxEnergyCapacity) / 100;
        }

        // Create an energy source according to it's type
        public static Energy CreateEnergySource(eEnergyType i_EnergyType, float i_CurrEnergyCapacityPercent, float i_MaxFuelCapacity, float i_MaxBatteryCapacity, Fuel.eFuelType i_FuelType)
        {
            float currEnergyCapacity;
            Energy energySource;

            if(i_EnergyType == eEnergyType.Battery)
            {
                currEnergyCapacity = CalculateCurrEnergyCapacity(i_MaxBatteryCapacity, i_CurrEnergyCapacityPercent);
                energySource = CreateBattery(currEnergyCapacity, i_MaxBatteryCapacity);
            }
            else
            {
                currEnergyCapacity = CalculateCurrEnergyCapacity(i_MaxFuelCapacity, i_CurrEnergyCapacityPercent);
                energySource = CreateFuel(currEnergyCapacity, i_MaxFuelCapacity, i_FuelType);
            }

            return energySource;
        }

        // Create battery energy source
        public static Battery CreateBattery(float i_CurrEnergyCapacity, float i_MaxEnergyCapacity)
        {
            return new Battery(i_MaxEnergyCapacity, i_CurrEnergyCapacity);
        }

        // Create fuel energy source
        public static Fuel CreateFuel(float i_CurrEnergyCapacity, float i_MaxEnergyCapacity, Fuel.eFuelType i_FuelType)
        {
            return new Fuel(i_MaxEnergyCapacity, i_CurrEnergyCapacity, i_FuelType);
        }

        // Create a new vehicle in garage
        public static VehicleInGarage CrerateNewVehicleInGarage(Vehicle i_vehicleToAdd, string i_OwnerName, string i_OwnerPhone)
        {
            return new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_vehicleToAdd);
        }

        // Check if the vehicle can have different types of energy (fuel/battery)
        public static bool HasEnergyOptions(int i_UserVehicleTypeChoice)
        {
            eTypeOfVehicle vehicleType = (eTypeOfVehicle)i_UserVehicleTypeChoice;

            return vehicleType != eTypeOfVehicle.Truck;
        }
    }
}