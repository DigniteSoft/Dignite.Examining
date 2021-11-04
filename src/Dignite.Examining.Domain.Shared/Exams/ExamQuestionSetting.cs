
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 考试试题的选项
    /// </summary>
    public class ExamQuestionSetting
    {
        /// <summary>
        /// 题库
        /// </summary>
        [Required]
        public Guid LibraryId { get; set; }

        /// <summary>
        /// 抽题模式
        /// </summary>
        [Required]
        public ExamQuestionSourceMode QuestionSourceMode { get; set; }

        /// <summary>
        /// QuestionSourceMode为<see cref="ExamQuestionSourceMode.Random"/>时，从题库中随机抽取的题型设定
        /// </summary>
        public List<ExamQuestionType> QuestionTypes { get; set; }

        /// <summary>
        /// QuestionSourceMode为<see cref="ExamQuestionSourceMode.Stably"/>时，题库中的固定试题
        /// </summary>
        public Guid[] Questions { get; set; }


        /// <summary>
        /// 试卷的题型
        /// </summary>
        [NotMapped]
        public class ExamQuestionType
        {
            /// <summary>
            /// 题型
            /// </summary>
            [Required]
            public string QuestionTypeName { get; set; }

            /// <summary>
            /// 随机抽取数据
            /// </summary>
            [Required]
            public int Number { get; set; }

            /// <summary>
            /// 题型的分值；
            /// </summary>
            [Required]
            public float Score { get; set; }
        }
    }
}
