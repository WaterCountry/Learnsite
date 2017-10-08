using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Mission 
	/// </summary>
	[Serializable]
	public class Mission
	{
		public Mission()
		{}
		#region Model
		private int _mid;
		private string _mtitle;
		private int? _mcid;
		private string _mcontent;
		private DateTime? _mdate;
		private int? _mhit;
		private string _mfiletype;
		private bool _mupload;
		private int? _msort;
		private bool _mpublish;
        private bool _mgroup;
        private int? _mgid = 0;
        private string _mexample = "";
        private int _mcategory = 0;
        private bool _microworld = false;
		/// <summary>
		/// 
		/// </summary>
		public int Mid
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mtitle
		{
			set{ _mtitle=value;}
			get{return _mtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mcid
		{
			set{ _mcid=value;}
			get{return _mcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mcontent
		{
			set{ _mcontent=value;}
			get{return _mcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Mdate
		{
			set{ _mdate=value;}
			get{return _mdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mhit
		{
			set{ _mhit=value;}
			get{return _mhit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mfiletype
		{
			set{ _mfiletype=value;}
			get{return _mfiletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Mupload
		{
			set{ _mupload=value;}
			get{return _mupload;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Msort
		{
			set{ _msort=value;}
			get{return _msort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Mpublish
		{
			set{ _mpublish=value;}
			get{return _mpublish;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool Mgroup
        {
            set { _mgroup = value; }
            get { return _mgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Mgid
        {
            set { _mgid = value; }
            get { return _mgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mexample
        {
            set { _mexample = value; }
            get { return _mexample; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Mcategory
        {
            set { _mcategory = value; }
            get { return _mcategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Microworld
        {
            set { _microworld = value; }
            get { return _microworld; }
        }
		#endregion Model

	}
}

