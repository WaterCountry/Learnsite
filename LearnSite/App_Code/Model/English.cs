using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡English 
	/// </summary>
	[Serializable]
	public class English
	{
        public English()
        { }
        #region Model
        private int _eid;
        private string _eword;
        private string _emeaning;
        private int? _elevel;
        /// <summary>
        /// 
        /// </summary>
        public int Eid
        {
            set { _eid = value; }
            get { return _eid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Eword
        {
            set { _eword = value; }
            get { return _eword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Emeaning
        {
            set { _emeaning = value; }
            get { return _emeaning; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Elevel
        {
            set { _elevel = value; }
            get { return _elevel; }
        }
        #endregion Model

    }
}