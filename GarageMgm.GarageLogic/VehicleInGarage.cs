using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        // Enum for different vehicle states in the garage
        public enum eState
        {
            InRepair = 1,
            Fixed,
            Payed
        }

        // Class members
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eState m_State;
        private Vehicle m_CurrVehicle;

        // Properties
        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
            set { m_OwnerPhone = value; }
        }

        public eState State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public Vehicle CurrVehicle
        {
            get { return m_CurrVehicle; }
            set { m_CurrVehicle = value; }
        }

        // Constructor
        public VehicleInGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_State = eState.InRepair;
            m_CurrVehicle = i_vehicle;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendFormat(
                @"Owner name: {0}
Owner phone: {1}
Vehicle state: {2}
Vehicle details:
{3}", 
m_OwnerName, 
m_OwnerPhone, 
m_State, 
m_CurrVehicle);

            return str.ToString();
        }
    }
}
