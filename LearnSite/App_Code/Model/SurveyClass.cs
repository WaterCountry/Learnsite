using System;
namespace LearnSite.Model
{
	/// <summary>
	/// SurveyClass:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SurveyClass
	{
		public SurveyClass()
		{}
		#region Model
		private int _yid;
		private int? _yyear;
		private int? _ygrade;
		private int? _yclass;
		private int? _yterm;
		private int? _ycid;
		private int? _yvid;
		private string _yselect;
		private string _ycount;
		private int? _yscore;
		private DateTime? _ydate;
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
		public int? Yyear
		{
			set{ _yyear=value;}
			get{return _yyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ygrade
		{
			set{ _ygrade=value;}
			get{return _ygrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Yclass
		{
			set{ _yclass=value;}
			get{return _yclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Yterm
		{
			set{ _yterm=value;}
			get{return _yterm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ycid
		{
			set{ _ycid=value;}
			get{return _ycid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Yvid
		{
			set{ _yvid=value;}
			get{return _yvid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Yselect
		{
			set{ _yselect=value;}
			get{return _yselect;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ycount
		{
			set{ _ycount=value;}
			get{return _ycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Yscore
		{
			set{ _yscore=value;}
			get{return _yscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Ydate
		{
			set{ _ydate=value;}
			get{return _ydate;}
		}
		#endregion Model

	}
}

