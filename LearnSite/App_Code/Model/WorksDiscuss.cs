using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡WorksDiscuss °£
	/// </summary>
	[Serializable]
	public class WorksDiscuss
	{
		public WorksDiscuss()
		{}
		#region Model
		private int _did;
		private int? _dwid;
		private string _dsnum;
		private string _dwords;
		private DateTime? _dtime;
		private string _dip;
		/// <summary>
		/// 
		/// </summary>
		public int Did
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Dwid
		{
			set{ _dwid=value;}
			get{return _dwid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dsnum
		{
			set{ _dsnum=value;}
			get{return _dsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dwords
		{
			set{ _dwords=value;}
			get{return _dwords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Dtime
		{
			set{ _dtime=value;}
			get{return _dtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dip
		{
			set{ _dip=value;}
			get{return _dip;}
		}
		#endregion Model

	}
}

