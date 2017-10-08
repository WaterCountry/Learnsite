using System;
namespace LearnSite.Model
{
	/// <summary>
	/// SurveyItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SurveyItem
	{
		public SurveyItem()
		{}
		#region Model
		private int _mid;
		private int? _mqid;
		private int? _mvid;
		private string _mitem;
		private int? _mscore=0;
		private int? _mcount=0;
        private int? _mcid;
		/// <summary>
		/// 
		/// </summary>
		public int Mid
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mqid
		{
			set{ _mqid=value;}
			get{return _mqid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mvid
		{
			set{ _mvid=value;}
			get{return _mvid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mitem
		{
			set{ _mitem=value;}
			get{return _mitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mscore
		{
			set{ _mscore=value;}
			get{return _mscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Mcount
		{
			set{ _mcount=value;}
			get{return _mcount;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? Mcid
        {
            set { _mcid = value; }
            get { return _mcid; }
        }
		#endregion Model

	}
}

