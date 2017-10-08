using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Works 
	/// </summary>
	[Serializable]
	public class Works
	{
		public Works()
		{}
		#region Model
		private int _wid;
		private string _wnum;
		private int? _wcid;
		private int? _wmid;
		private int? _wmsort;
		private string _wfilename;
		private string _wtype;
		private string _wurl;
		private int? _wlength;
		private int? _wscore;
		private DateTime? _wdate;
		private string _wip;
		private string _wtime;
		private int? _wvote;
		private int? _wegg;
		private bool _wcheck;
		private string _wself;
		private bool _wcan;
		private bool _wgood;
        private int? _wgrade;
        private int? _wterm;
        private int? _whit;
        private int? _wlscore;
        private int? _wlemotion;
        private bool? _woffice;
        private bool? _wflash;
        private bool? _werror;
        private int? _wfscore;
        private int? _wsid;
        private int? _wclass;
        private string _wname;
        private int? _wyear;
        private int? _wdscore;
        private string _wthumbnail="";
        private string _wtitle = "";
		/// <summary>
		/// 
		/// </summary>
		public int Wid
		{
			set{ _wid=value;}
			get{return _wid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wnum
		{
			set{ _wnum=value;}
			get{return _wnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wcid
		{
			set{ _wcid=value;}
			get{return _wcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wmid
		{
			set{ _wmid=value;}
			get{return _wmid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wmsort
		{
			set{ _wmsort=value;}
			get{return _wmsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wfilename
		{
			set{ _wfilename=value;}
			get{return _wfilename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wtype
		{
			set{ _wtype=value;}
			get{return _wtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wurl
		{
			set{ _wurl=value;}
			get{return _wurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wlength
		{
			set{ _wlength=value;}
			get{return _wlength;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wscore
		{
			set{ _wscore=value;}
			get{return _wscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Wdate
		{
			set{ _wdate=value;}
			get{return _wdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wip
		{
			set{ _wip=value;}
			get{return _wip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wtime
		{
			set{ _wtime=value;}
			get{return _wtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wvote
		{
			set{ _wvote=value;}
			get{return _wvote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wegg
		{
			set{ _wegg=value;}
			get{return _wegg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Wcheck
		{
			set{ _wcheck=value;}
			get{return _wcheck;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wself
		{
			set{ _wself=value;}
			get{return _wself;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Wcan
		{
			set{ _wcan=value;}
			get{return _wcan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Wgood
		{
			set{ _wgood=value;}
			get{return _wgood;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Wgrade
        {
            set { _wgrade = value; }
            get { return _wgrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wterm
        {
            set { _wterm = value; }
            get { return _wterm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Whit
        {
            set { _whit = value; }
            get { return _whit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wlscore
        {
            set { _wlscore = value; }
            get { return _wlscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wlemotion
        {
            set { _wlemotion = value; }
            get { return _wlemotion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Woffice
        {
            set { _woffice = value; }
            get { return _woffice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Wflash
        {
            set { _wflash = value; }
            get { return _wflash; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Werror
        {
            set { _werror = value; }
            get { return _werror; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wfscore
        {
            set { _wfscore = value; }
            get { return _wfscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wsid
        {
            set { _wsid = value; }
            get { return _wsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wclass
        {
            set { _wclass = value; }
            get { return _wclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Wname
        {
            set { _wname = value; }
            get { return _wname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wyear
        {
            set { _wyear = value; }
            get { return _wyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Wdscore
        {
            set { _wdscore = value; }
            get { return _wdscore; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Wthumbnail
        {
            set { _wthumbnail = value; }
            get { return _wthumbnail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Wtitle
        {
            set { _wtitle = value; }
            get { return _wtitle; }
        }

		#endregion Model
        
	}
}

