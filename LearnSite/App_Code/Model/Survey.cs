using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Survey:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Survey
	{
		public Survey()
		{}
		#region Model
		private int _vid;
		private int? _vcid;
		private int? _vhid;
		private string _vtitle;
		private string _vcontent;
		private int? _vtype=0;
		private int? _vtotal=0;
		private int? _vscore=0;
		private int? _vaverage;
		private bool _vclose= false;
		private bool _vpoint= false;
		private DateTime? _vdate;
		/// <summary>
		/// 
		/// </summary>
		public int Vid
		{
			set{ _vid=value;}
			get{return _vid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vcid
		{
			set{ _vcid=value;}
			get{return _vcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vhid
		{
			set{ _vhid=value;}
			get{return _vhid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Vtitle
		{
			set{ _vtitle=value;}
			get{return _vtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Vcontent
		{
			set{ _vcontent=value;}
			get{return _vcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vtype
		{
			set{ _vtype=value;}
			get{return _vtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vtotal
		{
			set{ _vtotal=value;}
			get{return _vtotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vscore
		{
			set{ _vscore=value;}
			get{return _vscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Vaverage
		{
			set{ _vaverage=value;}
			get{return _vaverage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Vclose
		{
			set{ _vclose=value;}
			get{return _vclose;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Vpoint
		{
			set{ _vpoint=value;}
			get{return _vpoint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Vdate
		{
			set{ _vdate=value;}
			get{return _vdate;}
		}
		#endregion Model

	}
}

