using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Quiz: µÃÂ¿‡
	/// </summary>
	[Serializable]
	public partial class Quiz
	{
		public Quiz()
		{}
		#region Model
		private int _qid;
		private int? _qtype;
		private string _question;
		private string _qanswer;
		private string _qanalyze;
		private int? _qscore;
		private string _qclass;
		private bool? _qselect= false;
		private int? _qright=0;
		private int? _qwrong=0;
		private int? _qaccuracy=0;
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
		public int? Qtype
		{
			set{ _qtype=value;}
			get{return _qtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Question
		{
			set{ _question=value;}
			get{return _question;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qanswer
		{
			set{ _qanswer=value;}
			get{return _qanswer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qanalyze
		{
			set{ _qanalyze=value;}
			get{return _qanalyze;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qscore
		{
			set{ _qscore=value;}
			get{return _qscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qclass
		{
			set{ _qclass=value;}
			get{return _qclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? Qselect
		{
			set{ _qselect=value;}
			get{return _qselect;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qright
		{
			set{ _qright=value;}
			get{return _qright;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qwrong
		{
			set{ _qwrong=value;}
			get{return _qwrong;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qaccuracy
		{
			set{ _qaccuracy=value;}
			get{return _qaccuracy;}
		}
		#endregion Model

	}
}

