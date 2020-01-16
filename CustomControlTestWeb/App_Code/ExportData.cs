using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxPro;

namespace AjaxServer
{
    /// <summary>
    /// ExportData 的摘要描述
    /// </summary>
    public class ExportData
    {
        static int iProgress;
        int icount;
        static int iWorkSeconds = 50;
        static DateTime StartTime;

        public int Progress
        {
            get { return iProgress; }
            set { iProgress = value; }
        }

        /// <summary>
        /// Long running background operation
        /// </summary>
        [AjaxMethod()]
        public bool ProgressBar2_DoWork()
        {
            bool result = false;
            try
            {
                //CancelPreviousRequest();
                //this.Progress = 0;
                this.Progress = 0;
                StartTime = DateTime.Now;
                for (int i = 0; i <= iWorkSeconds; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    this.Progress = i;
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Progress Statistics Info
        /// </summary>
        [AjaxMethod()]
        public int ProgressBar2_StatisticProgress()
        {
            //if (i == iWorkSeconds)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            //DateTime CurrentTime = DateTime.Now;
            //TimeSpan StartTimeSpan = new TimeSpan(StartTime.Ticks);
            //TimeSpan CurrentTimeSpan = new TimeSpan(CurrentTime.Ticks);
            //Double UsedSeconds = CurrentTimeSpan.Subtract(StartTimeSpan).TotalSeconds;
            //int Progress = Convert.ToInt32(UsedSeconds * 100 / iWorkSeconds);
            int Progress = Convert.ToInt32(this.Progress * 100 / iWorkSeconds);
            return Progress;
        }

        public void CancelPreviousRequest()
        {
            //i = iWorkSeconds;
            System.Threading.Thread.Sleep(1000);
        }
    }
}