using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        // Class members
        private readonly string r_Model;
        private readonly string r_VehicleLicenseNum;
        private readonly Energy r_Energy;
        private float m_RemainEnergyPercent;
        private Wheel[] m_WheelsArr;

        // Properties
        public string Model
        {
            get { return r_Model; }
        }

        public string VehicleLicenseNum
        {
            get { return r_VehicleLicenseNum; }
        }
    
        public float RemainEnergyPercent
        {
            get { return m_RemainEnergyPercent; }
            set { m_RemainEnergyPercent = value; }
        }

        public Wheel[] WheelsArr
        {
            get { return m_WheelsArr; }
            set { m_WheelsArr = value; }
        }

        public Energy EnergySorce
        {
            get { return r_Energy; }
        }

        // Constructor
        protected Vehicle(
                        string i_Model,
                        string i_VehicleLicenseNum,
                        float i_RemainEnergy,
                        Wheel[] i_WheelsArr,
                        Energy i_EnergySource)
        {
            r_Model = i_Model;
            r_VehicleLicenseNum = i_VehicleLicenseNum;
            m_RemainEnergyPercent = i_RemainEnergy;
            m_WheelsArr = i_WheelsArr;
            r_Energy = i_EnergySource;
        }

        // Get data for each wheel in the vehicles wheel collection
        public string GetWheelsArrData()
        {
            StringBuilder wheelDetalis = new StringBuilder();
            int wheelNumber = 1;

            foreach(Wheel wheel in WheelsArr)
            {
                wheelDetalis.Append(Environment.NewLine + wheelNumber + ": ");
                wheelDetalis.Append(wheel);
                wheelNumber++;
            }

            return wheelDetalis.ToString();
        }

        // Update vehicle's energy persent after refullin/recharging
        private void updateEnergyPercent()
        {
            m_RemainEnergyPercent = (EnergySorce.CurrEnergyCapacity * 100) / EnergySorce.MaxEnergyCapacity;
        }

        // Refuel a vehicle (with energy type = fuel)
        public void AddFuelToVehicle(Fuel.eFuelType i_FuelType, float i_AmountToAdd)
        {
            Fuel currFuel = r_Energy as Fuel;

            if(currFuel == null)
            {
                throw new FormatException();
            }

            currFuel.RefillEnergy(i_AmountToAdd, i_FuelType);
            updateEnergyPercent(); 
        }

        // Recharge a vehicle (with energy type = battery)
        public void RechargeBattery(float i_AmountToAdd)
        {
            Battery currBaterry = r_Energy as Battery;

            if(currBaterry == null)
            {
                throw new FormatException();
            }

            currBaterry.RefillEnergy(i_AmountToAdd);        
            updateEnergyPercent();
        }

        // Inflate each wheel in the vehicle's wheen collection
        public void InflateWheelsToMax()
        {
            Wheel[] currVehicleWheelsArr = m_WheelsArr;
            float airToAdd;

            foreach(Wheel currWheel in currVehicleWheelsArr)
            {
                airToAdd = currWheel.MaxAirPressure - currWheel.CurrAirPressure;
                currWheel.InflateWheel(airToAdd);
            }
        }

        // Overide ToString method
        public override string ToString()
        {
            return string.Format(
                @"Model: {0}
License number: {1}
Remain energy percent: {2}
Wheels Data: {3}
Energy Data: 
{4}", 
                r_Model, 
                r_VehicleLicenseNum, 
                RemainEnergyPercent, 
                GetWheelsArrData(), 
                r_Energy.ToString());
        }
    }   
}
