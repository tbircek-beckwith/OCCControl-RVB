
using System;
using Automatak.DNP3.Interface;

namespace rvb_sim.dnp.Helpers
{
    /// <summary>
    ///
    /// </summary>
    public class MasterConfigControl : IMasterConfigControl
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MasterConfigControl()
        {

        }

        #endregion


        public MasterConfig Configuration
        {
            get
            {
                var config = new MasterConfig
                {
                    disableUnsolOnStartup = false,
                    startupIntegrityClassMask = ClassField.AllClasses, // this.classFieldControlIntegrity.ClassFieldValue,
                    unsolClassMask = ClassField.AllClasses, // this.classFieldControlEnableUnsol.ClassFieldValue,
                    timeSyncMode = TimeSyncMode.None, // (TimeSyncMode)this.comboBoxTimeSync.SelectedItem,
                    integrityOnEventOverflowIIN = false, // this.checkBoxIntegrityOnOverflow.Checked,
                    responseTimeout = TimeSpan.FromMilliseconds(5000),
                    taskRetryPeriod = TimeSpan.FromMilliseconds(5000),                    
                };
                return config;
            }
        }
    }
}
