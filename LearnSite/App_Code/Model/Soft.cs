using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Soft 
	/// </summary>
	[Serializable]
	public class Soft
	{
		public Soft()
		{}
		#region Model
		private int _fid;
		private string _ftitle;
		private string _fcontent;
		private string _furl;
		private int? _fhit;
		private DateTime? _fdate;
		private string _ffiletype;
		private string _fclass;
        private bool _fhide;
        private int? _fopen;
        private int _fhid = -1;
        private int _fyid = 1;
        private bool _fup;
		/// <summary>
		/// 
		/// </summary>
		public int Fid
		{
			set{ _fid=value;}
			get{return _fid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ftitle
		{
			set{ _ftitle=value;}
			get{return _ftitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fcontent
		{
			set{ _fcontent=value;}
			get{return _fcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Furl
		{
			set{ _furl=value;}
			get{return _furl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fhit
		{
			set{ _fhit=value;}
			get{return _fhit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Fdate
		{
			set{ _fdate=value;}
			get{return _fdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ffiletype
		{
			set{ _ffiletype=value;}
			get{return _ffiletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fclass
		{
			set{ _fclass=value;}
			get{return _fclass;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool Fhide
        {
            set { _fhide = value; }
            get { return _fhide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fopen
        {
            set { _fopen = value; }
            get { return _fopen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Fhid
        {
            set { _fhid = value; }
            get { return _fhid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Fyid
        {
            set { _fyid = value; }
            get { return _fyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fup
        {
            set { _fup = value; }
            get { return _fup; }
        }
		#endregion Model

	}
}

