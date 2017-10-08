using System;
namespace LearnSite.Model
{
	/// <summary>
	/// ÊµÌåÀàNotSign ¡£
	/// </summary>
	[Serializable]
	public class NotSign
	{
		public NotSign()
		{}
		#region Model
		private int _nid;
		private string _nnum;
		private DateTime? _ndate;
		private int? _nyear;
		private int? _nmonth;
		private int? _nday;
		private string _nweek;
        private string _nnote;
        private int? _ngrade;
        private int? _nterm;
		/// <summary>
		/// 
		/// </summary>
		public int Nid
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nnum
		{
			set{ _nnum=value;}
			get{return _nnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Ndate
		{
			set{ _ndate=value;}
			get{return _ndate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Nyear
		{
			set{ _nyear=value;}
			get{return _nyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Nmonth
		{
			set{ _nmonth=value;}
			get{return _nmonth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Nday
		{
			set{ _nday=value;}
			get{return _nday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nweek
		{
			set{ _nweek=value;}
			get{return _nweek;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nnote
		{
			set{ _nnote=value;}
			get{return _nnote;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Ngrade
        {
            set { _ngrade = value; }
            get { return _ngrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Nterm
        {
            set { _nterm = value; }
            get { return _nterm; }
        }
		#endregion Model

	}
}

