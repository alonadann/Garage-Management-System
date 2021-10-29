using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        // Class members
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        // Properties
        public float MinValue
        {
            get { return r_MinValue; }
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }

        // Constructor
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}
