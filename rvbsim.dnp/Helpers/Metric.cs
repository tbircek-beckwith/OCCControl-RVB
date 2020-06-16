
using System;

namespace rvb_sim.dnp
{
    /// <summary>
    ///
    /// </summary>
    public class Metric
    {

        #region Private Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Metric(String id, String value)
        {
            Id = id;
            Value = value;
        }


        #endregion

        #region Public Properties

        public String Id { get; }

        public String Value { get; }

        #endregion
    }
}
