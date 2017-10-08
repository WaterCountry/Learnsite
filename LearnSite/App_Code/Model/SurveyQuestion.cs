using System;
namespace LearnSite.Model
{
	/// <summary>
	/// SurveyQuestion:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SurveyQuestion
	{
		public SurveyQuestion()
		{}
		#region Model
		private int _qid;
		private int? _qvid;
		private int? _qcid;
		private string _qtitle;
		private int? _qcount=0;
		/// <summary>
		/// 
		/// </summary>
		public int Qid
		{
			set{ _qid=value;}
			get{return _qid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qvid
		{
			set{ _qvid=value;}
			get{return _qvid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qcid
		{
			set{ _qcid=value;}
			get{return _qcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qtitle
		{
			set{ _qtitle=value;}
			get{return _qtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qcount
		{
			set{ _qcount=value;}
			get{return _qcount;}
		}
		#endregion Model

	}
}

