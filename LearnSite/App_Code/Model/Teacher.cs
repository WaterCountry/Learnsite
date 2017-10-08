using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Teacher 
	/// </summary>
	[Serializable]
	public class Teacher
	{
		public Teacher()
		{}
		#region Model
		private int _hid;
		private string _hname;
		private string _hpwd;
		private bool _hpermiss;
		private string _hnote;
        private string _hpath;
        private string _hnick;
        private string _hroom;
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
		public string Hpwd
		{
			set{ _hpwd=value;}
			get{return _hpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Hpermiss
		{
			set{ _hpermiss=value;}
			get{return _hpermiss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hnote
		{
			set{ _hnote=value;}
			get{return _hnote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Hpath
		{
			set{ _hpath=value;}
			get{return _hpath;}
        }        
        /// <summary>
        /// 
        /// </summary>
        public string Hnick
        {
            set { _hnick = value; }
            get { return _hnick; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Hroom
        {
            set { _hroom = value; }
            get { return _hroom; }
        }
		#endregion Model

	}
}

