using System;
namespace LearnSite.Model
{
	/// <summary>
	/// GaugeItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GaugeItem
	{
		public GaugeItem()
		{}
		#region Model
		private int _mid;
		private int? _mgid;
		private string _mitem;
		private int? _mscore;
		private int? _msort;
		/// <summary>
		/// 
		/// </summary>
		public int Mid
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mgid
		{
			set{ _mgid=value;}
			get{return _mgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mitem
		{
			set{ _mitem=value;}
			get{return _mitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mscore
		{
			set{ _mscore=value;}
			get{return _mscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Msort
		{
			set{ _msort=value;}
			get{return _msort;}
		}
		#endregion Model

	}
}

