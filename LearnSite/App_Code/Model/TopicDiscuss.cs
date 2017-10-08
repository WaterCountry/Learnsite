using System;
namespace LearnSite.Model
{
	/// <summary>
	/// TopicDiscuss:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TopicDiscuss
	{
		public TopicDiscuss()
		{}
		#region Model
		private int _tid;
		private int? _tcid;
		private string _ttitle;
		private string _tcontent;
		private int? _tcount;
		private int? _tteacher;
		private DateTime? _tdate;
		private bool _tclose;
		private string _tresult;
		/// <summary>
		/// 
		/// </summary>
		public int Tid
		{
			set{ _tid=value;}
			get{return _tid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tcid
		{
			set{ _tcid=value;}
			get{return _tcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ttitle
		{
			set{ _ttitle=value;}
			get{return _ttitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tcontent
		{
			set{ _tcontent=value;}
			get{return _tcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tcount
		{
			set{ _tcount=value;}
			get{return _tcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tteacher
		{
			set{ _tteacher=value;}
			get{return _tteacher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Tdate
		{
			set{ _tdate=value;}
			get{return _tdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Tclose
		{
			set{ _tclose=value;}
			get{return _tclose;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tresult
		{
			set{ _tresult=value;}
			get{return _tresult;}
		}
		#endregion Model

	}
}

