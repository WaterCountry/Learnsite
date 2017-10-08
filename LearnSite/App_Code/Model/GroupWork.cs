using System;
namespace LearnSite.Model
{
	/// <summary>
	/// 实体类GroupWork 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class GroupWork
	{
		public GroupWork()
		{}
		#region Model
		private int _gid;
		private string _gnum;
		private string _gstudents;
		private int? _gterm;
		private int? _ggrade;
		private int? _gclass;
		private int? _gcid;
		private int? _gmid;
		private string _gfilename;
		private string _gtype;
		private string _gurl;
		private int? _glengh;
		private int? _gscore;
		private int? _gtime;
		private int? _gvote;
		private bool _gcheck;
		private string _gnote;
		private int? _grank;
		private int? _ghit;
		private string _gip;
        private DateTime? _gdate;
        private int  _ggroup;
		/// <summary>
		/// 
		/// </summary>
		public int Gid
		{
			set{ _gid=value;}
			get{return _gid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gnum
		{
			set{ _gnum=value;}
			get{return _gnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gstudents
		{
			set{ _gstudents=value;}
			get{return _gstudents;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gterm
		{
			set{ _gterm=value;}
			get{return _gterm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ggrade
		{
			set{ _ggrade=value;}
			get{return _ggrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gclass
		{
			set{ _gclass=value;}
			get{return _gclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gcid
		{
			set{ _gcid=value;}
			get{return _gcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gmid
		{
			set{ _gmid=value;}
			get{return _gmid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gfilename
		{
			set{ _gfilename=value;}
			get{return _gfilename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gtype
		{
			set{ _gtype=value;}
			get{return _gtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gurl
		{
			set{ _gurl=value;}
			get{return _gurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Glengh
		{
			set{ _glengh=value;}
			get{return _glengh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gscore
		{
			set{ _gscore=value;}
			get{return _gscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gtime
		{
			set{ _gtime=value;}
			get{return _gtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gvote
		{
			set{ _gvote=value;}
			get{return _gvote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Gcheck
		{
			set{ _gcheck=value;}
			get{return _gcheck;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gnote
		{
			set{ _gnote=value;}
			get{return _gnote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Grank
		{
			set{ _grank=value;}
			get{return _grank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ghit
		{
			set{ _ghit=value;}
			get{return _ghit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gip
		{
			set{ _gip=value;}
			get{return _gip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Gdate
		{
			set{ _gdate=value;}
			get{return _gdate;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int Ggroup
        {
            set { _ggroup = value; }
            get { return _ggroup; }
        }
		#endregion Model

	}
}

