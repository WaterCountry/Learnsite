using System;
namespace LearnSite.Model
{
	/// <summary>
	/// SurveyFeedback:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SurveyFeedback
	{
		public SurveyFeedback()
		{}
		#region Model
		private int _fid;
		private string _fnum;
		private int? _fyear;
		private int? _fgrade;
		private int? _fclass;
		private int? _fterm;
		private int? _fcid;
		private int? _fvid;
		private int? _fvtype=0;
		private string _fselect;
		private int? _fscore=0;
		private DateTime? _fdate;
        private int? _fsid;
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
		public int? Fyear
		{
			set{ _fyear=value;}
			get{return _fyear;}
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
		public int? Fterm
		{
			set{ _fterm=value;}
			get{return _fterm;}
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
		public int? Fvid
		{
			set{ _fvid=value;}
			get{return _fvid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Fvtype
		{
			set{ _fvtype=value;}
			get{return _fvtype;}
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
		public DateTime? Fdate
		{
			set{ _fdate=value;}
			get{return _fdate;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Fsid
        {
            set { _fsid = value; }
            get { return _fsid; }
        }
		#endregion Model

	}
}

