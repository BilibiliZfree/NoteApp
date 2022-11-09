using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 博客分类
    /// </summary>
    public enum ClassificationEnum
    {
        全部,
        CSharp,
        WPF,
        Web,
        Unity,
        数据库
    }
    /// <summary>
    /// 个人或全部分类
    /// </summary>
    public enum OnerEnum
    {
        全部,
        个人
    }

    public enum SortEnum
    {
        按发布时间升序,
        按发布时间降序,
        按修改时间升序,
        按修改时间降序
    }
}
