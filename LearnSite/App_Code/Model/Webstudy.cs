using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Webstudy 
	/// </summary>
	[Serializable]
	public class Webstudy
	{
		public Webstudy()
		{}
		#region Model
		private int _wid;
		private string _wnum;
		private string _wpwd;
		private int? _wvote;
		private int? _wegg;
		private int? _wscore;
		private bool _wcheck;
        private int? _wquotacurrent;
        private int _wsid;
		/// <summary>
		/// 
		/// </summary>
		public int Wid
		{
			set{ _wid=value;}
			get{return _wid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wnum
		{
			set{ _wnum=value;}
			get{return _wnum;}
		}		
		/// <summary>
		/// 
		/// </summary>
		public string Wpwd
		{
			set{ _wpwd=value;}
			get{return _wpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wvote
		{
			set{ _wvote=value;}
			get{return _wvote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wegg
		{
			set{ _wegg=value;}
			get{return _wegg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Wscore
		{
			set{ _wscore=value;}
			get{return _wscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Wcheck
		{
			set{ _wcheck=value;}
			get{return _wcheck;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WquotaCurrent
		{
			set{ _wquotacurrent=value;}
			get{return _wquotacurrent;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int Wsid
        {
            set { _wsid = value; }
            get { return _wsid; }
        }
		#endregion Model

	}
}

