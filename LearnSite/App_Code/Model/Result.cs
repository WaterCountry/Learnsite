using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Result: µÃÂ¿‡
	/// </summary>
	[Serializable]
	public partial class Result
	{
		public Result()
		{}
		#region Model
		private int _rid;
		private string _rnum;
		private int? _rscore;
		private DateTime? _rdate;
		private string _rhistory;
		private string _rwrong;
        private int _rgrade;
        private int _rterm;
        private int _rsid;
		/// <summary>
		/// 
		/// </summary>
		public int Rid
		{
			set{ _rid=value;}
			get{return _rid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rnum
		{
			set{ _rnum=value;}
			get{return _rnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rscore
		{
			set{ _rscore=value;}
			get{return _rscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Rdate
		{
			set{ _rdate=value;}
			get{return _rdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rhistory
		{
			set{ _rhistory=value;}
			get{return _rhistory;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rwrong
		{
			set{ _rwrong=value;}
			get{return _rwrong;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int Rgrade
        {
            set { _rgrade = value; }
            get { return _rgrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Rterm
        {
            set { _rterm = value; }
            get { return _rterm; }
        }		
        /// <summary>
        /// 
        /// </summary>
        public int Rsid
        {
            set { _rsid = value; }
            get { return _rsid; }
        }
		#endregion Model

	}
}

