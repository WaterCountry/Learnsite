using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Courses 
	/// </summary>
	[Serializable]
	public class Courses
	{
		public Courses()
		{}
		#region Model
		private int _cid;
		private string _ctitle;
		private string _cclass;
		private string _ccontent;
		private DateTime? _cdate;
		private int? _chit;
		private int? _cobj;
		private int? _cterm;
		private int? _cks;
		private string _cfiletype;
		private bool _cupload;
		private int? _chid;
		private bool _cpublish;
		/// <summary>
		/// 
		/// </summary>
		public int Cid
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ctitle
		{
			set{ _ctitle=value;}
			get{return _ctitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cclass
		{
			set{ _cclass=value;}
			get{return _cclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ccontent
		{
			set{ _ccontent=value;}
			get{return _ccontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Cdate
		{
			set{ _cdate=value;}
			get{return _cdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Chit
		{
			set{ _chit=value;}
			get{return _chit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Cobj
		{
			set{ _cobj=value;}
			get{return _cobj;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Cterm
		{
			set{ _cterm=value;}
			get{return _cterm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Cks
		{
			set{ _cks=value;}
			get{return _cks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cfiletype
		{
			set{ _cfiletype=value;}
			get{return _cfiletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Cupload
		{
			set{ _cupload=value;}
			get{return _cupload;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Chid
		{
			set{ _chid=value;}
			get{return _chid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Cpublish
		{
			set{ _cpublish=value;}
			get{return _cpublish;}
		}
		#endregion Model

	}
}

