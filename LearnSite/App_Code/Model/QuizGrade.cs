using System;
namespace LearnSite.Model
{
	/// <summary>
	///  µÃÂ¿‡QuizGrade 
	/// </summary>
	[Serializable]
	public class QuizGrade
	{
		public QuizGrade()
		{}
        #region Model
        private int _qid;
        private int? _qobj;
        private string _qclass;
        private int? _qhid;
        private int? _qonly;
        private int? _qmore;
        private int? _qjudge;
        private bool? _qopen;
        private bool? _qanswer;
        /// <summary>
        /// 
        /// </summary>
        public int Qid
        {
            set { _qid = value; }
            get { return _qid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qobj
        {
            set { _qobj = value; }
            get { return _qobj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qclass
        {
            set { _qclass = value; }
            get { return _qclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qhid
        {
            set { _qhid = value; }
            get { return _qhid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qonly
        {
            set { _qonly = value; }
            get { return _qonly; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qmore
        {
            set { _qmore = value; }
            get { return _qmore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qjudge
        {
            set { _qjudge = value; }
            get { return _qjudge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Qopen
        {
            set { _qopen = value; }
            get { return _qopen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Qanswer
        {
            set { _qanswer = value; }
            get { return _qanswer; }
        }
        #endregion Model

    }
}