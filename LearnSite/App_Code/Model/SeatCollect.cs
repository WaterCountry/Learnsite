using System;
using System.Collections.Generic;
using System.Data;
namespace LearnSite.Model
{
    /// <summary>
    ///SeatCollect 坐位表集合体
    /// </summary>
    public class SeatCollect
    {
        public SeatCollect()
        { }

        #region Model
        private DataTable _dt;
        private int _column;
        private int _online;

        /// <summary>
        /// 坐位表
        /// </summary>
        public DataTable Dt
        {
            set { _dt = value; }
            get { return _dt; }
        }
        /// <summary>
        /// 坐标列数
        /// </summary>
        public int Column
        {
            set { _column = value; }
            get { return _column; }
        }
        /// <summary>
        /// 签到人数
        /// </summary>
        public int Online
        {
            set { _online = value; }
            get { return _online; }
        }

        #endregion
    }
}