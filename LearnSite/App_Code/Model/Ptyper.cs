using System;
namespace LearnSite.Model
{
	/// <summary>
	/// 实体类Ptyper 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ptyper
	{
		public Ptyper()
		{}
		#region Model
		private int _pid;
		private int? _ptid;
		private string _psnum;
		private int? _pscore;
		private DateTime? _pdate;
		private string _pip;
		private int? _ptype;
        private int? _pdegree;
        private int _pgrade;
        private int _pterm;
        private int _psid;

		/// <summary>
		/// 
		/// </summary>
		public int Pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ptid
		{
			set{ _ptid=value;}
			get{return _ptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Psnum
		{
			set{ _psnum=value;}
			get{return _psnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pscore
		{
			set{ _pscore=value;}
			get{return _pscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Pdate
		{
			set{ _pdate=value;}
			get{return _pdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pip
		{
			set{ _pip=value;}
			get{return _pip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ptype
		{
			set{ _ptype=value;}
			get{return _ptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pdegree
		{
			set{ _pdegree=value;}
			get{return _pdegree;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int Pgrade
        {
            set { _pgrade = value; }
            get { return _pgrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pterm
        {
            set { _pterm = value; }
            get { return _pterm; }
        }		
        /// <summary>
        /// 
        /// </summary>
        public int Psid
        {
            set { _psid = value; }
            get { return _psid; }
        }
		#endregion Model

	}
}

