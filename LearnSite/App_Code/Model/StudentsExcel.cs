using System;
namespace LearnSite.Model
{
	/// <summary>
	/// ÊµÌåÀàStudentsExcel ¡£
	/// </summary>
	[Serializable]
	public class StudentsExcel
	{
		public StudentsExcel()
		{}
		#region Model
		private int _sid;
		private string _snum;
		private int? _syear;
		private int? _sgrade;
		private int? _sclass;
		private string _sname;
		private string _spwd;
		private string _sex;
		private string _saddress;
		private string _sphone;
		private string _sparents;
		private string _sheadtheacher;
		private int? _sscore;
		private int? _squiz;
		private int? _sattitude;
		private string _sape;
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
		public string Snum
		{
			set{ _snum=value;}
			get{return _snum;}
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
		public string Sname
		{
			set{ _sname=value;}
			get{return _sname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Spwd
		{
			set{ _spwd=value;}
			get{return _spwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Saddress
		{
			set{ _saddress=value;}
			get{return _saddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sphone
		{
			set{ _sphone=value;}
			get{return _sphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sparents
		{
			set{ _sparents=value;}
			get{return _sparents;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sheadtheacher
		{
			set{ _sheadtheacher=value;}
			get{return _sheadtheacher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sscore
		{
			set{ _sscore=value;}
			get{return _sscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Squiz
		{
			set{ _squiz=value;}
			get{return _squiz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sattitude
		{
			set{ _sattitude=value;}
			get{return _sattitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sape
		{
			set{ _sape=value;}
			get{return _sape;}
		}
		#endregion Model

	}
}

