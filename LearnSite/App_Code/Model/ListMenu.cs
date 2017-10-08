using System;
namespace LearnSite.Model
{
	/// <summary>
	/// ListMenu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ListMenu
	{
		public ListMenu()
		{}
		#region Model
		private int _lid;
		private int? _lcid;
		private int? _lsort=0;
		private int? _ltype;
		private int? _lxid;
		private bool _lshow= true;
		private string _ltitle;
		/// <summary>
		/// 
		/// </summary>
		public int Lid
		{
			set{ _lid=value;}
			get{return _lid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Lcid
		{
			set{ _lcid=value;}
			get{return _lcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Lsort
		{
			set{ _lsort=value;}
			get{return _lsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ltype
		{
			set{ _ltype=value;}
			get{return _ltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Lxid
		{
			set{ _lxid=value;}
			get{return _lxid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Lshow
		{
			set{ _lshow=value;}
			get{return _lshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ltitle
		{
			set{ _ltitle=value;}
			get{return _ltitle;}
		}
		#endregion Model

	}
}

