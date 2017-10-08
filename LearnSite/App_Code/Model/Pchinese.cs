
using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Pchinese:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Pchinese
	{
		public Pchinese()
		{}
		#region Model
		private int _pid;
		private int? _psid;
		private string _psnum;
		private int? _papple;
		private int? _ptotal;
        private int? _pspeed;
		private int? _pdegree;
		private int? _pyear;
		private int? _pgrade;
		private int? _pclass;
		private int? _pterm;
		private DateTime? _pdate;
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
		public int? Psid
		{
			set{ _psid=value;}
			get{return _psid;}
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
		public int? Papple
		{
			set{ _papple=value;}
			get{return _papple;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ptotal
		{
			set{ _ptotal=value;}
			get{return _ptotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pspeed
		{
            set { _pspeed = value; }
            get { return _pspeed; }
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
		public int? Pyear
		{
			set{ _pyear=value;}
			get{return _pyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pgrade
		{
			set{ _pgrade=value;}
			get{return _pgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pclass
		{
			set{ _pclass=value;}
			get{return _pclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Pterm
		{
			set{ _pterm=value;}
			get{return _pterm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Pdate
		{
			set{ _pdate=value;}
			get{return _pdate;}
		}
		#endregion Model

	}
}

