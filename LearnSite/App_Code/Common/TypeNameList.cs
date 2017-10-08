using System;
using System.Collections.Generic;
using System.Collections;
namespace LearnSite.Common
{
    /// <summary>
    ///TypeNameList 的摘要说明
    /// </summary>
    public class TypeNameList
    {
        public TypeNameList()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取允许文件的后缀名
        /// </summary>
        /// <returns></returns>
        private static string[] GetTypeName(string typename)
        {
            string AllFileType = LearnSite.Common.XmlHelp.GetTypeName(typename);
            AllFileType = AllFileType.Replace(" ", "");           
            string[] GetFileTypes = AllFileType.Split(new char[] { '|' });
            return GetFileTypes;
        }
        /// <summary>
        /// 学案类型,包括全部, 测验专用
        /// </summary>
        /// <returns></returns>
        public static ArrayList QuizCourseType()
        {
            string[] typestr = GetTypeName("CourseType");
            ArrayList arr = new ArrayList();
            if (typestr.Length > 0)
            {
                string Quizall = "全部显示";
                arr.Add(Quizall);
                for (int i = 0; i < typestr.Length; i++)
                {
                    arr.Add(typestr[i].Trim());
                }
            }
            return arr;
        }
        /// <summary>
        /// 学案类型
        /// </summary>
        /// <returns></returns>
        public static ArrayList CourseType()
        {
            string[] typestr = GetTypeName("CourseType");
            ArrayList arr = new ArrayList();
            if (typestr.Length > 0)
            {
                for (int i = 0; i < typestr.Length; i++)
                {
                    arr.Add(typestr[i].Trim());
                }
            }
            return arr;
        }
        /// <summary>
        /// 学案节次
        /// </summary>
        /// <returns></returns>
        public static ArrayList CoursePeriod()
        {
            ArrayList arr = new ArrayList();
            string str = LearnSite.Common.XmlHelp.GetTypeName("CoursePeriod");
            if (LearnSite.Common.WordProcess.IsNum(str))
            {
                int ks = int.Parse(str);
                for (int i = 0; i < ks; i++)
                {
                    arr.Add(i + 1);
                }
            }
            return arr;
        }
        /// <summary>
        /// 学案节次
        /// </summary>
        /// <returns></returns>
        public static ArrayList ClassMaxSet()
        {
            ArrayList arr = new ArrayList();
            string str = LearnSite.Common.XmlHelp.GetTypeName("ClassMax");
            if (LearnSite.Common.WordProcess.IsNum(str))
            {
                int cmax = int.Parse(str);
                for (int i = 0; i < cmax; i++)
                {
                    arr.Add(i + 1);
                }
            }
            return arr;
        }
        /// <summary>
        /// 作品类型
        /// </summary>
        /// <returns></returns>
        public static ArrayList WorksType()
        {
            ArrayList arr = new ArrayList();
            string[] typestr = GetTypeName("WorksType");
            if (typestr.Length > 0)
            {
                for (int i = 0; i < typestr.Length; i++)
                {
                    arr.Add(typestr[i]);
                }
            }
            return arr;
        }

    }
}