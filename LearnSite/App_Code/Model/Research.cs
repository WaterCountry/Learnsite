using System;
namespace LearnSite.Model
{
	/// <summary>
	/// Research:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Research
	{
		public Research()
		{}
		#region Model
		private int _rid;
		private int? _rsid;
		private int? _ryear;
		private int? _rgrade;
		private int? _rclass;
		private int? _rterm;
		private decimal _rlearn;
        private decimal _rplay;
        private decimal _rsleep;
        private decimal _rfree;
		private DateTime _rdate;
		/// <summary>
		/// 
		/// </summary>
		public int Rid
		{
			set{ _rid=value;}
			get{return _rid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rsid
		{
			set{ _rsid=value;}
			get{return _rsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ryear
		{
			set{ _ryear=value;}
			get{return _ryear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rgrade
		{
			set{ _rgrade=value;}
			get{return _rgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rclass
		{
			set{ _rclass=value;}
			get{return _rclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rterm
		{
			set{ _rterm=value;}
			get{return _rterm;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Rlearn
		{
			set{ _rlearn=value;}
			get{return _rlearn;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Rplay
		{
			set{ _rplay=value;}
			get{return _rplay;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Rsleep
		{
			set{ _rsleep=value;}
			get{return _rsleep;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Rfree
		{
			set{ _rfree=value;}
			get{return _rfree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Rdate
		{
			set{ _rdate=value;}
			get{return _rdate;}
		}
		#endregion Model

	}
}

