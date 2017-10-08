using System;
namespace LearnSite.Model
{
	/// <summary>
	/// House:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class House
	{
		public House()
		{}
		#region Model
		private int _hid;
		private string _hname;
		private string _hseat;
		/// <summary>
		/// 
		/// </summary>
		public int Hid
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hname
		{
			set{ _hname=value;}
			get{return _hname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hseat
		{
			set{ _hseat=value;}
			get{return _hseat;}
		}
		#endregion Model

	}
}

