
using System.Threading.Tasks;
using Automatak.DNP3.Interface;

namespace rvb_sim.dnp
{
    /// <summary>
    ///
    /// </summary>
    public class StringCallback : ITaskCallback

    {
        readonly TaskCompletionSource<TaskCompletion> tcs = new TaskCompletionSource<TaskCompletion>();

        public Task<TaskCompletion> Task => tcs.Task;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public StringCallback()
        {

        }

        public void OnComplete(TaskCompletion result)
        {
            tcs.SetResult(result);
        }

        public void OnDestroyed()
        {
            //throw new System.NotImplementedException();
        }

        public void OnStart()
        {
           // throw new System.NotImplementedException();
        }

        #endregion
    }
}
