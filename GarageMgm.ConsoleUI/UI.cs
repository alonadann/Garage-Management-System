using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class UI
    {
        // enum to check if to fulter by state 
        public enum eFilter
        {
            NoFilter = 1,
            Filter
        }

        // enum of options in menu 
        public enum eMenuOption
        {
            AddVehicle = 1,
            DisplayLicenseNumbers,
            ModifyVehicleState,
            InflateVehicleWheels,
            RefuelVehicle,
            ChargeElectricVehicle,
            DisplayVehicleDetails,
            Exit
        }

        // enum to boolean options 
        public enum eBoolChoice
        {
            True = 1,
            False
        }

        // Class members
        private static Garage s_Garage;

        // show menu of options to do at the garage and handle the chosen option
        public static void RunGarage()
        {
            s_Garage = new Garage();
            while(true)
            {
                showMenu();
                try
                {
                    handleMenu((eMenuOption)getUserChoiceFromEnumValues(typeof(eMenuOption)));
                }
                catch(Exception)
                {
                    Console.WriteLine("unexpected excepion occured");
                }                
            }
        }

        // print the menu 
        private static void showMenu()
        {
            string menuTxt =
    @"Please choose from the following options (1-9):
            1. Add a new vehicle to garage.
            2. Show license plate numbers for vehicles in the garage with option to filter by state.
            3. Change vehicle's state.
            4. Inflate vehicle's wheels to maximum.
            5. Refuel a fuel-powered vehicle.
            6. Charge an electric-powered vehicle.
            7. Display full details of a vehicle.
            8. Exit.
            ";

            Console.WriteLine(menuTxt);
        }

        // handle the chosen option from menu 
        private static void handleMenu(eMenuOption i_MenuOption)
        {
            switch(i_MenuOption)
            {
                case eMenuOption.AddVehicle:
                    addVehicleToGarage();
                    break;
                case eMenuOption.DisplayLicenseNumbers:
                    displayLicenseNumbers();
                    break;
                case eMenuOption.ModifyVehicleState:
                    modifyVehicleState();
                    break;
                case eMenuOption.InflateVehicleWheels:
                    tryInflateVehicleWheels();
                    break;
                case eMenuOption.RefuelVehicle:
                    tryRefuelVehicle();
                    break;
                case eMenuOption.ChargeElectricVehicle:
                    tryRechargeElectricVehicle();
                    break;
                case eMenuOption.DisplayVehicleDetails:
                    displayVehicleDetails();
                    break;
                case eMenuOption.Exit:
                    exitProgram();
                    break;
                default:
                    break;
            }
        }

        // add vehicle to the garage 
        private static void addVehicleToGarage()
        {
            string licenseNumber = getLicenseNumber();

            if(s_Garage.IsInGarage(licenseNumber))
            {
                // Update vehicle's state to "In Repair"
                s_Garage.ModifyState(licenseNumber, VehicleInGarage.eState.InRepair);
                Console.WriteLine("Vehicle already exists. Vehicle's state is updated to In Repair");
            }
            else
            {
                addNewVehicleToGarage(licenseNumber);
                Console.WriteLine("Vehicle added successfully");
            }
        }

        // get the choice of user from enum's options 
        private static int getUserChoiceFromEnumValues(Type i_Enum)
        {
            Array enumValues = Enum.GetValues(i_Enum);
            int numOfEnumValues = enumValues.Length;
            int indexOfEnumValue = 0;
            bool isNumber, isValidEnum = false;
            string textualIndexOfEnumValue;

            while (!isValidEnum)
            {
                int currentValueIndex = 1;
                if (i_Enum != typeof(eMenuOption))
                {
                    Console.WriteLine("Choose one of the following: ");
                    foreach(object enumValue in enumValues)
                    {
                        Console.WriteLine(string.Format("{0}- {1}", currentValueIndex, enumValue));
                        currentValueIndex++;
                    }
                }

                textualIndexOfEnumValue = Console.ReadLine();
                isNumber = int.TryParse(textualIndexOfEnumValue, out indexOfEnumValue);
                if(isNumber && indexOfEnumValue >= 1 && indexOfEnumValue <= numOfEnumValues)  
                {
                    isValidEnum = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }

            return indexOfEnumValue;
        }

        // get license number from user 
        private static string getLicenseNumber()
        {
            bool isValidLicenseNum = false;
            string licenseNumber = null;

            Console.WriteLine("Enter license number - 8 digit number");
            while(!isValidLicenseNum)
            {
                licenseNumber = Console.ReadLine();
                if(isValidLicense(licenseNumber))
                {
                    isValidLicenseNum = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }

            return licenseNumber;
        }

        // get phone from user 
        private static string getOwnerPhone()
        {
            bool isValidPhoneNum = false;
            string phoneNumber = null;

            while(!isValidPhoneNum)
            {
                phoneNumber = Console.ReadLine();
                if(isValidPhoneNumber(phoneNumber))
                {
                    isValidPhoneNum = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }

            return phoneNumber;
        }

        // exit program 
        private static void exitProgram()
        {
            Console.WriteLine("Goodbye");
            Environment.Exit(0);
        }

        // inflate wheels of wanted vehicle in the garage to maximum 
        private static void inflateVehicleWheels()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                string licenseNum = getLicenseNumInGarage();
                s_Garage.InflateVehicleInGarageWheelsToMax(licenseNum);
            }
        }

        // Get from user license number of vehicle in the garage 
        private static string getLicenseNumInGarage()
        {
            string licenseNum = getLicenseNumber();

            while(!s_Garage.IsInGarage(licenseNum))
            {
                Console.WriteLine("Vehicle with license number " + licenseNum + " is not in garage, please try again");
                licenseNum = getLicenseNumber();
            }

            return licenseNum;
        }

        // inflate wheels of wanted vehicle in the garage to maximum and return message if not succeed
        private static void tryInflateVehicleWheels()
        {
            try
            {
                inflateVehicleWheels();
                Console.WriteLine("Wheel inflated successfully to maximum");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("The given air amout is out of range! It should be between " + ex.MinValue + " to " + ex.MaxValue);
            }
        }

        // add new vehicle to the garage 
        private static void addNewVehicleToGarage(string i_LicenseNumber) 
        {
            string ownerName, ownerPhone;
            Vehicle newVehicle = createNewVehicle(i_LicenseNumber); 

            getVehicleInGarageUserData(out ownerName, out ownerPhone);
            VehicleInGarage newVehicleInGarage = VehicleCreator.CrerateNewVehicleInGarage(newVehicle, ownerName, ownerPhone);
            s_Garage.AddToGarage(newVehicleInGarage);
        }

        // create new vehicle with given license number 
        private static Vehicle createNewVehicle(string i_LicenseNumber)
        {
            Dictionary<string, Type> parametersTypesDic = new Dictionary<string, Type>();
            int energytype, vehicleType;

            vehicleType = getUserChoiceFromEnumValues(typeof(VehicleCreator.eTypeOfVehicle));
            energytype = (int)VehicleCreator.eEnergyType.Fuel;
            if(VehicleCreator.HasEnergyOptions(vehicleType))
            {
                energytype = getUserChoiceFromEnumValues(typeof(VehicleCreator.eEnergyType));
            }

            VehicleCreator.GetRequiredVehicleParameters(vehicleType, parametersTypesDic);
            Dictionary<string, object> userEnteredParameters = getVehiclsCommonData(i_LicenseNumber, vehicleType);
            getExtraParametersDataFromUser(parametersTypesDic, userEnteredParameters);
            Vehicle newVehicle = VehicleCreator.CreateVehicle(vehicleType, energytype, userEnteredParameters);

            return newVehicle;
        }

        // get data of vehicle in garage 
        private static void getVehicleInGarageUserData(out string o_OwnerName, out string o_OwnerPhone)
        {
            Console.WriteLine("Enter vehicle's owner name");
            o_OwnerName = getString();
            Console.WriteLine("Enter vehicle's owner phone - 10 digit number");
            o_OwnerPhone = getOwnerPhone();
        }

        // get from user data which is common to all vehicles 
        private static Dictionary<string, object> getVehiclsCommonData(string i_LicenseNumber, int i_VehicleType)
        {
            Dictionary<string, object> enteredUserParameters = new Dictionary<string, object>();

            enteredUserParameters["vehicle license number"] = i_LicenseNumber;
            Console.WriteLine("Enter vehicle's model");
            enteredUserParameters["Model"] = getString();
            getRemainEnergyPercent(enteredUserParameters);
            VehicleCreator.GetVehicleWheelsData(i_VehicleType, out float maxAirPressure, out int wheelsNum);
            Wheel[] wheelsArr = getUserWheelsData(wheelsNum, maxAirPressure);
            enteredUserParameters["WheelsArr"] = wheelsArr;

            return enteredUserParameters;
        }

        // get string input from user 
        private static string getString()
        {
            bool isValidStr = false;
            string str = null;

            while(!isValidStr)
            {
                str = Console.ReadLine();
                if(isValidString(str))
                {
                    isValidStr = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }

            return str;
        }

        // get energy percent from user 
        private static void getRemainEnergyPercent(Dictionary<string, object> io_EnteredUserParameters)
        {
            bool isValidFloat = false, success;
            float remainEnergyPercent = 0;
            string parameterAsString;

            Console.WriteLine("Enter vehicle's remain energy percent");
            while(!isValidFloat)
            {
                parameterAsString = Console.ReadLine();
                success = float.TryParse(parameterAsString, out remainEnergyPercent);
                if(success && remainEnergyPercent >= 0 && remainEnergyPercent <= 100)
                {
                    io_EnteredUserParameters["remain energy percent"] = remainEnergyPercent;
                    isValidFloat = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again ");
                }
            }
        }

        // get wheels data from user 
        private static Wheel[] getUserWheelsData(int i_WheelsNum, float i_MaxAirPressure)
        {
            Wheel[] wheelsArr = new Wheel[i_WheelsNum];
            string manufactureName;
            float currAirPressure;

            for(int i = 0; i < i_WheelsNum; i++) 
            {
                Console.WriteLine("Enter wheel " + (i + 1) + " data:");
                getWheelData(out manufactureName, out currAirPressure, i_MaxAirPressure);
                wheelsArr[i] = new Wheel(manufactureName, currAirPressure, i_MaxAirPressure);
            }

            return wheelsArr;
        }

        // get one wheel's data from user 
        private static void getWheelData(out string o_ManufactureName, out float o_CurrAirPressure, float i_MaxAirPressure)
        {
            Console.WriteLine("Enter manufacture name");
            o_ManufactureName = getString();
            o_CurrAirPressure = getCurrAirPressure(i_MaxAirPressure);
        }

        // get current air pressure from user 
        private static float getCurrAirPressure(float i_MaxAirPressure)
        {
            bool isValidFloat = false, success;
            float currAirPressure = 0;
            string parameterAsString;

            Console.WriteLine("Enter current Air Pressure");
            while(!isValidFloat)
            {
                parameterAsString = Console.ReadLine();
                success = float.TryParse(parameterAsString, out currAirPressure);
                if(success && currAirPressure >= 0 && currAirPressure <= i_MaxAirPressure)
                {
                     isValidFloat = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again ");
                }
            }

            return currAirPressure;
        }

        // get boolean input from user 
        private static void getBoolParameter(string i_CurrentParam, Dictionary<string, object> io_UserEnteredParameters)
        {
            string userInputAsString;
            bool boolValue = false, isValidBool = false;
            string msg = string.Format(@"Choose {0}? Press 1 for 'True' ,press 2 for 'False'.", i_CurrentParam);

            Console.WriteLine(msg);
            while(!isValidBool)
            {
                userInputAsString = Console.ReadLine();
                bool success = int.TryParse(userInputAsString, out int userInputAsInt);
                if(success && (userInputAsInt == (int)eBoolChoice.True || userInputAsInt == (int)eBoolChoice.False))
                {
                    if(userInputAsInt == (int)eBoolChoice.True)
                    {
                        boolValue = true;
                    }

                    io_UserEnteredParameters.Add(i_CurrentParam, boolValue);
                    isValidBool = true;
                }
                else
                {
                    Console.WriteLine("Wrong input , please try again ");
                }
            }
        }

        // get int input from user 
        private static void getIntParameter(string i_CurrentParam, Dictionary<string, object> io_UserEnteredParameters)
        {
            string msg = string.Format("Please enter {0} (an integer number):", i_CurrentParam);
            Console.WriteLine(msg);
            int value = getInt();

            io_UserEnteredParameters.Add(i_CurrentParam, value);
        }

        // get float input from user 
        private static void getFloatParameter(string i_CurrentParam, Dictionary<string, object> io_UserEnteredParameters)
        {
            float value;
            string msg = string.Format("Please enter {0} (a number):", i_CurrentParam);

            Console.WriteLine(msg);
            value = getFloat();
            io_UserEnteredParameters.Add(i_CurrentParam, value);
        }

        // get enum choice from user 
        private static void getEnumParameter(string i_CurrentParam, Dictionary<string, object> io_UserEnteredParameters, Type i_Enum)
        {
            Console.WriteLine(string.Format("Please enter {0}.", i_CurrentParam));
            io_UserEnteredParameters.Add(i_CurrentParam, getUserChoiceFromEnumValues(i_Enum));
        }

        // get wanted paarameters of the wanted vwhicle type 
        private static void getExtraParametersDataFromUser(Dictionary<string, Type> i_VehicleRequiredParamteres, Dictionary<string, object> io_UserEnteredParameters)
        {
            foreach(string currentParam in i_VehicleRequiredParamteres.Keys)
            {
                if(i_VehicleRequiredParamteres[currentParam] == typeof(bool))
                {
                    getBoolParameter(currentParam, io_UserEnteredParameters);
                }
                else if(i_VehicleRequiredParamteres[currentParam] == typeof(int))
                {
                    getIntParameter(currentParam, io_UserEnteredParameters);
                }
                else if(i_VehicleRequiredParamteres[currentParam] == typeof(float))
                {
                    getFloatParameter(currentParam, io_UserEnteredParameters);
                }
                else if(i_VehicleRequiredParamteres[currentParam].IsEnum)
                {
                    getEnumParameter(currentParam, io_UserEnteredParameters, i_VehicleRequiredParamteres[currentParam]);
                }
            }
        }

        // Show license numbers with filtering option
        private static void displayLicenseNumbers()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;
            eFilter filter;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                filter = (eFilter)getUserChoiceFromEnumValues(typeof(eFilter));
                if(filter == eFilter.Filter)
                {
                    displayFilteredLicenseNumbers(vehiclesInGarage); 
                }
                else
                {
                    foreach(KeyValuePair<string, VehicleInGarage> vehicle in vehiclesInGarage)
                    {
                        Console.WriteLine(vehicle.Key);
                    }
                }
            }
        }

        // Show filtered license numbers 
        private static void displayFilteredLicenseNumbers(Dictionary<string, VehicleInGarage> i_VehiclesInGarage)
        {
            int countVehicles = 0;
            VehicleInGarage.eState vehicleState = (VehicleInGarage.eState)getUserChoiceFromEnumValues(typeof(VehicleInGarage.eState));

            foreach(KeyValuePair<string, VehicleInGarage> vehicle in i_VehiclesInGarage)
            {
                if(vehicle.Value.State.Equals(vehicleState))
                {
                    Console.WriteLine(vehicle.Key);
                    countVehicles++;
                }
            }

            if (countVehicles == 0)
            {
                Console.WriteLine("No vehicles in state " + vehicleState);
            }
        }

        // Gets a license number and modifies the relevant vehicle to a new state
        private static void modifyVehicleState()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                string licenseNum = getLicenseNumInGarage();
                Console.WriteLine("Please select new state");
                VehicleInGarage.eState newState = (VehicleInGarage.eState)getUserChoiceFromEnumValues(typeof(VehicleInGarage.eState));
                s_Garage.ModifyState(licenseNum, newState);
                Console.WriteLine("Vehicle state changed successfully");
            }
        }

        // Gets a license number and checks if vehicle exists in garage
        private static bool isVehicleInGarage(string i_License)
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;
            bool licenseExists = false;

            foreach(KeyValuePair<string, VehicleInGarage> vehicle in vehiclesInGarage)
            {
                if(vehicle.Key == i_License)
                {
                    licenseExists = true;
                }
            }

            return licenseExists;
        }

        // Refuel vehicle - gets a license number, fuel type and amount of fuel to add and refuel the vehicle
        private static void refuelVehicle()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;
            Fuel.eFuelType fuelType;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                float amountToAdd;
                string licenseNum = getLicenseNumInGarage();
                fuelType = (Fuel.eFuelType)getUserChoiceFromEnumValues(typeof(Fuel.eFuelType));
                Console.WriteLine("Please enter number of liters to refuel:");
                amountToAdd = getFloat();
                s_Garage.AddFuelToVehicleInGarage(licenseNum, fuelType, amountToAdd);
                Console.WriteLine("Vehicle refueled successfully");
            }
        }

        // Recharge vehicle - gets a license number and amount of fuel to add and refuel the vehicle
        private static void rechargeElectricVehicle()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;
            float amountToAdd;
            string licenseNum;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                licenseNum = getLicenseNumInGarage();
                Console.WriteLine("Please enter number of minutes to charge:");
                amountToAdd = getFloat();
                s_Garage.RechargeVehicleInGarageBattery(licenseNum, amountToAdd);
                Console.WriteLine("Recharged successfully");
            }
        }
        
        // refuele vehicle and show message if it doesn't succeed 
        private static void tryRefuelVehicle()
        {
            try
            {
                refuelVehicle();
            }
            catch(FormatException)
            {
                Console.WriteLine("The vehicle's energy source is not fuel");
            }
            catch(ArgumentException)
            {
                Console.WriteLine("The given fuel type does not match the vehicle");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine("The refuel amout is out of range! It should be between " + ex.MinValue + " to " + ex.MaxValue);
            }
        }

        // recharge electric vehicle and show message if it doesn't succeed 
        private static void tryRechargeElectricVehicle()
        {
            try
            {
                rechargeElectricVehicle();
            }
            catch(FormatException)
            {
                Console.WriteLine("The vehicle's energy source is not battery");
            }
            catch(ArgumentException)
            {
                Console.WriteLine("The energy source battery doesn't have fuel type parameter");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine("The refuel amout of minutes is out of range! It should be between " + (ex.MinValue * 60) + " to " + (ex.MaxValue * 60));
            }
        }

        // Display vehicle details by license number
        private static void displayVehicleDetails()
        {
            Dictionary<string, VehicleInGarage> vehiclesInGarage = s_Garage.VehiclesInGarageDic;
            string licenseNumber;

            if(vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                licenseNumber = getLicenseNumInGarage();
                s_Garage.PrintVehicleDetails(licenseNumber);
            }
        }

        // Gets a float parameter
        private static float getFloat()
        {
            string parameterAsString;
            float floatParameter = 0;
            bool success, isValidFloat = false;

            while(!isValidFloat)
            {
                parameterAsString = Console.ReadLine();
                success = float.TryParse(parameterAsString, out floatParameter);
                if(success && floatParameter >= 0)
                {
                    isValidFloat = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again ");
                }
            }

            return floatParameter;
        }

        private static int getInt()
        {
            string parameterAsString;
            int intParameter = 0;
            bool success, isValidInt = false;

            while(!isValidInt)
            {
                parameterAsString = Console.ReadLine();
                success = int.TryParse(parameterAsString, out intParameter);
                if(success && intParameter > 0)
                {
                    isValidInt = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again ");
                }
            }

            return intParameter;
        }

        // check if given string is valid 
        private static bool isValidString(string i_Str)
        {
            return !i_Str.Equals(string.Empty);
        }

        // check if given phone number is valid
        private static bool isValidPhoneNumber(string i_Str)
        {
            return int.TryParse(i_Str, out int number) && i_Str.Length == 10;
        }

        // check id given license number is valid 
        private static bool isValidLicense(string i_Str)
        {
            return int.TryParse(i_Str, out int number) && i_Str.Length == 8;
        }
    }
}
