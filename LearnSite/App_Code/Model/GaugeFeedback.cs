using System;
namespace LearnSite.Model
{
	/// <summary>
	/// GaugeFeedback:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GaugeFeedback
	{
		public GaugeFeedback()
		{}
		#region Model
		private int _fid;
		private string _fnum;
		private int? _fgrade;
		private int? _fclass;
		private int? _fcid;
		private int? _fmid;
		private int? _fwid;
		private int? _fgid;
		private string _fselect;
		private int? _fscore;
		private bool _fgood;
        private DateTime? _fdate;
        private int _fsid;

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
		public string Fnum
		{
			set{ _fnum=value;}
			get{return _fnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fgrade
		{
			set{ _fgrade=value;}
			get{return _fgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fclass
		{
			set{ _fclass=value;}
			get{return _fclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fcid
		{
			set{ _fcid=value;}
			get{return _fcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fmid
		{
			set{ _fmid=value;}
			get{return _fmid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fwid
		{
			set{ _fwid=value;}
			get{return _fwid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fgid
		{
			set{ _fgid=value;}
			get{return _fgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fselect
		{
			set{ _fselect=value;}
			get{return _fselect;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fscore
		{
			set{ _fscore=value;}
			get{return _fscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Fgood
		{
			set{ _fgood=value;}
			get{return _fgood;}
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
        public int Fsid
        {
            set { _fsid = value; }
            get { return _fsid; }
        }
		#endregion Model

	}
}

