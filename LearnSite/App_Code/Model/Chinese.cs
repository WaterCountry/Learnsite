
using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Chinese:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Chinese
	{
		public Chinese()
		{}
		#region Model
		private int _nid;
		private string _ntitle;
		private string _ncontent;
		/// <summary>
		/// 
		/// </summary>
		public int Nid
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ntitle
		{
			set{ _ntitle=value;}
			get{return _ntitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ncontent
		{
			set{ _ncontent=value;}
			get{return _ncontent;}
		}
		#endregion Model

	}
}

