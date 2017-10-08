using System;
namespace LearnSite.Model
{
	/// <summary>
	/// ShareDisk:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShareDisk
	{
		public ShareDisk()
		{}
		#region Model
		private int _kid;
        private bool _kown = false;
		private int? _kyear;
		private int? _kgrade;
		private int? _kclass;
		private int? _kgroup;
		private string _knum;
		private string _kname;
		private string _kfilename;
		private int? _kfsize;
		private string _kfurl;
		private string _kftpe;
		private DateTime? _kfdate;
        /// <summary>
        /// 
        /// </summary>
        public int Kid
        {
            set { _kid = value; }
            get { return _kid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public bool Kown
		{
            set { _kown = value; }
            get { return _kown; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kyear
		{
			set{ _kyear=value;}
			get{return _kyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kgrade
		{
			set{ _kgrade=value;}
			get{return _kgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kclass
		{
			set{ _kclass=value;}
			get{return _kclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kgroup
		{
			set{ _kgroup=value;}
			get{return _kgroup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Knum
		{
			set{ _knum=value;}
			get{return _knum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Kname
		{
			set{ _kname=value;}
			get{return _kname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Kfilename
		{
			set{ _kfilename=value;}
			get{return _kfilename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kfsize
		{
			set{ _kfsize=value;}
			get{return _kfsize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Kfurl
		{
			set{ _kfurl=value;}
			get{return _kfurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Kftpe
		{
			set{ _kftpe=value;}
			get{return _kftpe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Kfdate
		{
			set{ _kfdate=value;}
			get{return _kfdate;}
		}
		#endregion Model

	}
}

