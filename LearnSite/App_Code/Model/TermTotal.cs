using System;
namespace LearnSite.Model
{
	/// <summary>
	/// 实体类TermTotal 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TermTotal
	{
		public TermTotal()
		{}
		#region Model
		private int _tid;
		private string _tnum;
		private int? _tterm;
		private int? _tgrade;
		private int? _tscore;
		private int? _tgscore;
		private int? _tquiz;
		private int? _tattitude;
		private int? _twscore;
		private int? _ttscore;
		private int? _tpscore;
		private int? _tallscore;
		private string _tape;
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
		public string Tnum
		{
			set{ _tnum=value;}
			get{return _tnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tterm
		{
			set{ _tterm=value;}
			get{return _tterm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tgrade
		{
			set{ _tgrade=value;}
			get{return _tgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tscore
		{
			set{ _tscore=value;}
			get{return _tscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tgscore
		{
			set{ _tgscore=value;}
			get{return _tgscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tquiz
		{
			set{ _tquiz=value;}
			get{return _tquiz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tattitude
		{
			set{ _tattitude=value;}
			get{return _tattitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Twscore
		{
			set{ _twscore=value;}
			get{return _twscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ttscore
		{
			set{ _ttscore=value;}
			get{return _ttscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tpscore
		{
			set{ _tpscore=value;}
			get{return _tpscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tallscore
		{
			set{ _tallscore=value;}
			get{return _tallscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tape
		{
			set{ _tape=value;}
			get{return _tape;}
		}
		#endregion Model

	}
}

