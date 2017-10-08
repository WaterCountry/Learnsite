using System;
namespace LearnSite.Model
{
	/// <summary>
	/// TopicReply:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TopicReply
	{
		public TopicReply()
		{}
		#region Model
		private int _rid;
		private int? _rtid;
		private string _rsnum;
		private string _rwords;
		private DateTime? _rtime;
		private string _rip;
		private int? _rscore;
		private bool _rban;
        private int? _rgrade;
        private int? _rterm;
        private int _rcid;
        private int _rclass;
        private int _rsid;
        private int _ryear;
        private bool _redit;
        private int _ragree;
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
		public int? Rtid
		{
			set{ _rtid=value;}
			get{return _rtid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rsnum
		{
			set{ _rsnum=value;}
			get{return _rsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rwords
		{
			set{ _rwords=value;}
			get{return _rwords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Rtime
		{
			set{ _rtime=value;}
			get{return _rtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rip
		{
			set{ _rip=value;}
			get{return _rip;}
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
		public bool Rban
		{
			set{ _rban=value;}
			get{return _rban;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Rgrade
        {
            set { _rgrade = value; }
            get { return _rgrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Rterm
        {
            set { _rterm = value; }
            get { return _rterm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Rcid
        {
            set { _rcid = value; }
            get { return _rcid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Rclass
        {
            set { _rclass = value; }
            get { return _rclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Rsid
        {
            set { _rsid = value; }
            get { return _rsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Ryear
        {
            set { _ryear = value; }
            get { return _ryear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Redit
        {
            set { _redit = value; }
            get { return _redit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Ragree
        {
            set { _ragree = value; }
            get { return _ragree; }
        }
		#endregion Model

	}
}

