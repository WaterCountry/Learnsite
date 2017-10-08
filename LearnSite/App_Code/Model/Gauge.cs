using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Gauge:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Gauge
	{
		public Gauge()
		{}
		#region Model
		private int _gid;
		private int? _ghid;
		private string _gtype;
		private string _gtitle;
		private int? _gcount;
		private DateTime? _gdate;
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
		public int? Ghid
		{
			set{ _ghid=value;}
			get{return _ghid;}
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
		public string Gtitle
		{
			set{ _gtitle=value;}
			get{return _gtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Gcount
		{
			set{ _gcount=value;}
			get{return _gcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Gdate
		{
			set{ _gdate=value;}
			get{return _gdate;}
		}
		#endregion Model

	}
}

