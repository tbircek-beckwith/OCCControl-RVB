
using System;
using Automatak.DNP3.Interface;

namespace rvb_sim.dnp
{
    /// <summary>
    ///
    /// </summary>
    public class LinkConfigControl
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LinkConfigControl()
        {

        }

        public LinkConfigControl(ushort source, ushort destination)
        {
            LocalAddr = source;
            RemoteAddr = destination;
        }

        #endregion

        public LinkConfig Configuration
        {
            get
            {                
                var config = new LinkConfig(isMaster: isMaster, useConfirms: true)
                {
                    localAddr = decimal.ToUInt16(LocalAddr),
                    remoteAddr = decimal.ToUInt16(RemoteAddr),
                    // config.numRetry = Decimal.ToUInt32(this.numericUpDownRetries.Value);
                    responseTimeout = TimeSpan.FromMilliseconds(5000),
                    keepAliveTimeout = TimeSpan.FromMilliseconds(10000),
                };
                return config;
            }
            set
            {
                Configure(value);

            }
        }

        //private void SetState()
        //{
        //    this.groupBoxConfirmed.Enabled = this.checkBoxConfirmed.Checked;
        //}

        private void Configure(LinkConfig config)
        {
            isMaster = config.isMaster;
            LocalAddr = config.localAddr;
            RemoteAddr = config.remoteAddr;
            ResponseTimeout = Convert.ToDecimal(config.responseTimeout.TotalMilliseconds);
            // this.numericUpDownRetries.Value = config.numRetry;
            // UseConfirms = config.us.useConfirms;
            KeepAliveTimeout = Convert.ToDecimal(config.keepAliveTimeout.TotalMilliseconds);

            //this.SetState();
        }

        private bool isMaster = true;
        private ushort LocalAddr;
        private ushort RemoteAddr;
        private decimal ResponseTimeout;
        // private object UseConfirms;
        private decimal KeepAliveTimeout;

        //private void checkBoxConfirmed_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.SetState();
        //}
    }
}
