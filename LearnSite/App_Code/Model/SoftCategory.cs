using System;
namespace LearnSite.Model
{
	/// <summary>
	/// SoftCategory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SoftCategory
	{
		public SoftCategory()
		{}
		#region Model
		private int _yid;
		private int? _ysort=0;
		private string _ytitle;
		private string _ycontent;
		private bool _yopen= false;
		/// <summary>
		/// 
		/// </summary>
		public int Yid
		{
			set{ _yid=value;}
			get{return _yid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ysort
		{
			set{ _ysort=value;}
			get{return _ysort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ytitle
		{
			set{ _ytitle=value;}
			get{return _ytitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ycontent
		{
			set{ _ycontent=value;}
			get{return _ycontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Yopen
		{
			set{ _yopen=value;}
			get{return _yopen;}
		}
		#endregion Model

	}
}

