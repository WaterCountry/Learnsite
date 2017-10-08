using System;
namespace LearnSite.Model
{
	/// <summary>
	/// DelStudents:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class DelStudents
	{
		public DelStudents()
		{}
		#region Model
		private int _did;
		private string _dnum;
		private int? _dyear;
		private int? _dgrade;
		private int? _dclass;
		private string _dname;
		private string _dsex;
		private string _daddress;
		private string _dphone;
		private string _dparents;
		private string _dheadtheacher;
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
		public string Dnum
		{
			set{ _dnum=value;}
			get{return _dnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Dyear
		{
			set{ _dyear=value;}
			get{return _dyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Dgrade
		{
			set{ _dgrade=value;}
			get{return _dgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Dclass
		{
			set{ _dclass=value;}
			get{return _dclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dname
		{
			set{ _dname=value;}
			get{return _dname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dsex
		{
			set{ _dsex=value;}
			get{return _dsex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Daddress
		{
			set{ _daddress=value;}
			get{return _daddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dphone
		{
			set{ _dphone=value;}
			get{return _dphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dparents
		{
			set{ _dparents=value;}
			get{return _dparents;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Dheadtheacher
		{
			set{ _dheadtheacher=value;}
			get{return _dheadtheacher;}
		}
		#endregion Model

	}
}

