using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Summary: µÃÂ¿‡
	/// </summary>
	[Serializable]
	public partial class Summary
	{
		public Summary()
		{}
		#region Model
		private int _sid;
		private int? _scid;
		private int? _shid;
		private string _scontent;
		private DateTime? _sdate;
		private int? _sgrade;
		private int? _sclass;
		private int? _syear;
		private bool? _sshow;
		/// <summary>
		/// 
		/// </summary>
		public int Sid
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Scid
		{
			set{ _scid=value;}
			get{return _scid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Shid
		{
			set{ _shid=value;}
			get{return _shid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Scontent
		{
			set{ _scontent=value;}
			get{return _scontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Sdate
		{
			set{ _sdate=value;}
			get{return _sdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sgrade
		{
			set{ _sgrade=value;}
			get{return _sgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sclass
		{
			set{ _sclass=value;}
			get{return _sclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Syear
		{
			set{ _syear=value;}
			get{return _syear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? Sshow
		{
			set{ _sshow=value;}
			get{return _sshow;}
		}
		#endregion Model

	}
}

