
using System;

namespace rvb_sim.dnp
{
    /// <summary>
    ///
    /// </summary>
    public static class SafeConvert
    {
            public static Int16 ToInt16(decimal value)
            {
                if (value > Int16.MaxValue)
                {
                    return Int16.MaxValue;
                }
                else if (value < Int16.MinValue)
                {
                    return Int16.MinValue;
                }
                else
                {
                    return Convert.ToInt16(value);
                }
            }

            public static Int32 ToInt32(decimal value)
            {
                if (value > Int32.MaxValue)
                {
                    return Int32.MaxValue;
                }
                else if (value < Int32.MinValue)
                {
                    return Int32.MinValue;
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }

            public static System.Single ToSingle(decimal value)
            {
                return Convert.ToSingle(value);
            }

            public static System.Double ToDouble(decimal value)
            {
                return Convert.ToDouble(value);
            }
        }        
}