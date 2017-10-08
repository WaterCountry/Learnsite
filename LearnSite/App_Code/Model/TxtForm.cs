
using System;
namespace LearnSite.Model
{
	/// <summary>
	/// TxtForm:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TxtForm
	{
		public TxtForm()
		{}
		#region Model
		private int _mid;
		private string _mtitle;
		private int? _mcid;
		private string _mcontent;
		private DateTime? _mdate;
		private int? _mhit;
		private bool _mpublish;
		private bool _mdelete;
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
		public string Mtitle
		{
			set{ _mtitle=value;}
			get{return _mtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mcid
		{
			set{ _mcid=value;}
			get{return _mcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mcontent
		{
			set{ _mcontent=value;}
			get{return _mcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Mdate
		{
			set{ _mdate=value;}
			get{return _mdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mhit
		{
			set{ _mhit=value;}
			get{return _mhit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Mpublish
		{
			set{ _mpublish=value;}
			get{return _mpublish;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Mdelete
		{
			set{ _mdelete=value;}
			get{return _mdelete;}
		}
		#endregion Model

	}
}

