using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Room 
	/// </summary>
	[Serializable]
	public class Room
	{
		public Room()
		{}
		#region Model
		private int _rid;
		private int? _rhid;
		private int? _rgrade;
		private int? _rclass;
		private bool _rset;
		private string _rpwd;
        private bool _rlock;
        private string _rip;
        private bool _rgauge;
        private bool _rclassedit;
        private bool _rphotoedit;
        private bool _rsexedit;
        private bool _rnameedit;
        private int? _rcid;
        private bool _ropen;
        private int? _rseat = 0;
        private bool _rshare;
        private bool _rpwdsee;
        private bool _rgroupshare;
        private string _rtyper;
        private bool _rreg;
        private bool _rscratch;

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
		public int? Rhid
		{
			set{ _rhid=value;}
			get{return _rhid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rgrade
		{
			set{ _rgrade=value;}
			get{return _rgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rclass
		{
			set{ _rclass=value;}
			get{return _rclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Rset
		{
			set{ _rset=value;}
			get{return _rset;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rpwd
		{
			set{ _rpwd=value;}
			get{return _rpwd;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool Rlock
        {
            set { _rlock = value; }
            get { return _rlock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Rip
        {
            set { _rip = value; }
            get { return _rip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Rgauge
        {
            set { _rgauge = value; }
            get { return _rgauge; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Rclassedit
        {
            set { _rclassedit = value; }
            get { return _rclassedit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Rphotoedit
        {
            set { _rphotoedit = value; }
            get { return _rphotoedit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Rsexedit
        {
            set { _rsexedit = value; }
            get { return _rsexedit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Rnameedit
        {
            set { _rnameedit = value; }
            get { return _rnameedit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Rcid
        {
            set { _rcid = value; }
            get { return _rcid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Ropen
        {
            set { _ropen = value; }
            get { return _ropen; }
        }       
        /// <summary>
        /// 
        /// </summary>
        public int? Rseat
        {
            set { _rseat = value; }
            get { return _rseat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Rshare
        {
            set { _rshare = value; }
            get { return _rshare; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Rpwdsee
        {
            set { _rpwdsee = value; }
            get { return _rpwdsee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Rgroupshare
        {
            set { _rgroupshare = value; }
            get { return _rgroupshare; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Rtyper
        {
            set { _rtyper = value; }
            get { return _rtyper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Rreg
        {
            set { _rreg = value; }
            get { return _rreg; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Rscratch
        {
            set { _rscratch = value; }
            get { return _rscratch; }
        }
		#endregion Model

	}
}

