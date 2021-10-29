using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // Class members
        private Dictionary<string, VehicleInGarage> m_VehiclesInGarageDic;

        // Properties
        public Dictionary<string, VehicleInGarage> VehiclesInGarageDic
        {
            get { return m_VehiclesInGarageDic; }
            set { m_VehiclesInGarageDic = value; }
        }

        // Constructor
        public Garage()
        {
            m_VehiclesInGarageDic = new Dictionary<string, VehicleInGarage>();
        }

        // Check if vehicle with the given licence number is in garage
        public bool IsInGarage(string i_LicenseNumber)
        {
            return VehiclesInGarageDic.ContainsKey(i_LicenseNumber);
        }

        // Add new vehicle to garage (includig owner details)
        public void AddToGarage(VehicleInGarage i_NewVehicleInGarage)
        {
            VehiclesInGarageDic.Add(i_NewVehicleInGarage.CurrVehicle.VehicleLicenseNum, i_NewVehicleInGarage);
        }

        // MOdify vehicle's state in garage
        public void ModifyState(string i_License, VehicleInGarage.eState i_NewState)
        {
            VehiclesInGarageDic[i_License].State = i_NewState;
        }

        // Refuel a fuel-powerd vehicle
        public void AddFuelToVehicleInGarage(string i_License, Fuel.eFuelType i_FuelType, float i_AmountToAdd)
        {
            Vehicle currVehicle = VehiclesInGarageDic[i_License].CurrVehicle;

            currVehicle.AddFuelToVehicle(i_FuelType, i_AmountToAdd);
        }

        // Recharge an electric-powerd vehicle
        public void RechargeVehicleInGarageBattery(string i_License, float i_AmountToAdd)
        {
            Vehicle currVehicle = VehiclesInGarageDic[i_License].CurrVehicle;

            currVehicle.RechargeBattery(i_AmountToAdd);
        }

        // Inflate all wheels in the vehicle's wheel collection to the max
        public void InflateVehicleInGarageWheelsToMax(string i_LicenseNum)
        {
            m_VehiclesInGarageDic[i_LicenseNum].CurrVehicle.InflateWheelsToMax();
        }

        // Displayes all relevant vehicle details
        public void PrintVehicleDetails(string i_License)
        {
            VehicleInGarage currVehicleInGarage = VehiclesInGarageDic[i_License];
            Console.WriteLine(currVehicleInGarage.ToString());
        }
    }
}
