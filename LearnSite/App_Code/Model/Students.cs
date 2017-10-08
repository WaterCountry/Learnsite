using System;
namespace LearnSite.Model
{
	/// <summary>
	/// 实体类Students 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Students
	{
		public Students()
		{}
		#region Model
		private int _sid;
		private string _snum;
		private int? _syear;
		private int? _sgrade;
		private int? _sclass;
		private string _sname;
		private string _spwd;
		private string _sex;
		private string _saddress;
		private string _sphone;
		private string _sparents;
		private string _sheadtheacher;
		private int? _sscore;
		private int? _squiz;
		private int? _sattitude;
		private int? _swscore;
		private int? _stscore;
		private int? _sallscore;
		private string _sape;
        private int? _spscore;
        private int? _sgroup;
        private bool _sleader;
        private int? _svote;
        private int? _sgscore;
		/// <summary>
		/// 
		/// </summary>
		public int Sid
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Snum
		{
			set{ _snum=value;}
			get{return _snum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Syear
		{
			set{ _syear=value;}
			get{return _syear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sgrade
		{
			set{ _sgrade=value;}
			get{return _sgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sclass
		{
			set{ _sclass=value;}
			get{return _sclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sname
		{
			set{ _sname=value;}
			get{return _sname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Spwd
		{
			set{ _spwd=value;}
			get{return _spwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Saddress
		{
			set{ _saddress=value;}
			get{return _saddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sphone
		{
			set{ _sphone=value;}
			get{return _sphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sparents
		{
			set{ _sparents=value;}
			get{return _sparents;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sheadtheacher
		{
			set{ _sheadtheacher=value;}
			get{return _sheadtheacher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sscore
		{
			set{ _sscore=value;}
			get{return _sscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Squiz
		{
			set{ _squiz=value;}
			get{return _squiz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sattitude
		{
			set{ _sattitude=value;}
			get{return _sattitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Swscore
		{
			set{ _swscore=value;}
			get{return _swscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Stscore
		{
			set{ _stscore=value;}
			get{return _stscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sallscore
		{
			set{ _sallscore=value;}
			get{return _sallscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sape
		{
			set{ _sape=value;}
			get{return _sape;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Spscore
        {
            set { _spscore = value; }
            get { return _spscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sgroup
        {
            set { _sgroup = value; }
            get { return _sgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Sleader
        {
            set { _sleader = value; }
            get { return _sleader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Svote
        {
            set { _svote = value; }
            get { return _svote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sgscore
        {
            set { _sgscore = value; }
            get { return _sgscore; }
        }
		#endregion Model

	}
}

