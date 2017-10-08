using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Pfinger: µÃÂ¿‡
	/// </summary>
	[Serializable]
	public class Pfinger
	{
		public Pfinger()
		{}
		#region Model
		private int _pid;
		private string _psnum;
		private decimal? _pspd;
		private int? _pyear;
		private int? _pmonth;
		private DateTime? _pdate;
        private int? _pdegree;
        private int _pgrade;
        private int _pterm;
        private int _psid;

		/// <summary>
		/// 
		/// </summary>
		public int Pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Psnum
		{
			set{ _psnum=value;}
			get{return _psnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Pspd
		{
			set{ _pspd=value;}
			get{return _pspd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pyear
		{
			set{ _pyear=value;}
			get{return _pyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pmonth
		{
			set{ _pmonth=value;}
			get{return _pmonth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Pdate
		{
			set{ _pdate=value;}
			get{return _pdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pdegree
		{
			set{ _pdegree=value;}
			get{return _pdegree;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int Pgrade
        {
            set { _pgrade = value; }
            get { return _pgrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pterm
        {
            set { _pterm = value; }
            get { return _pterm; }
        }		
        /// <summary>
        /// 
        /// </summary>
        public int Psid
        {
            set { _psid = value; }
            get { return _psid; }
        }
		#endregion Model

	}
}

