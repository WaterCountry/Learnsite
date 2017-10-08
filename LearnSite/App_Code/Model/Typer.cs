using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡Typer 
	/// </summary>
	[Serializable]
	public class Typer
	{
		public Typer()
		{}
		#region Model
		private int _tid;
		private int? _ttype;
		private int? _tuse;
		private string _ttitle;
		private string _tcontent;
		/// <summary>
		/// 
		/// </summary>
		public int Tid
		{
			set{ _tid=value;}
			get{return _tid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ttype
		{
			set{ _ttype=value;}
			get{return _ttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tuse
		{
			set{ _tuse=value;}
			get{return _tuse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ttitle
		{
			set{ _ttitle=value;}
			get{return _ttitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tcontent
		{
			set{ _tcontent=value;}
			get{return _tcontent;}
		}
		#endregion Model

	}
}

