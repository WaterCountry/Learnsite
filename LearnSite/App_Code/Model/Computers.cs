using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Computers:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Computers
	{
		public Computers()
		{}
		#region Model
		private int _pid;
		private string _pip;
		private string _pmachine;
		private bool _plock;
		private DateTime? _pdate;
        private int _px;
        private int _py;
        private string _pm;
		/// <summary>
		/// 
		/// </summary>
		public int Pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pip
		{
			set{ _pip=value;}
			get{return _pip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pmachine
		{
			set{ _pmachine=value;}
			get{return _pmachine;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Plock
		{
			set{ _plock=value;}
			get{return _plock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Pdate
		{
			set{ _pdate=value;}
			get{return _pdate;}
		}

        /// <summary>
        /// 
        /// </summary>
        public int Px
        {
            set { _px = value; }
            get { return _px; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Py
        {
            set { _py = value; }
            get { return _py; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pm
        {
            set { _pm = value; }
            get { return _pm; }
        }
		#endregion Model

	}
}

