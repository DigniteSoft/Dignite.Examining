using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Examining.Examinations
{
    public class ExaminationEditDto
    {
        /// <summary>
        /// 考试名称
        /// </summary>
        [Required]
        [StringLength(ExaminationConsts.MaxTitleLength)]
        public string Title { get; set; }

        /// <summary>
        /// 说明、须知
        /// </summary>
        public string Announcement { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// 考卷设置项
        /// </summary>
        [Required]
        public ExaminationSetting Settings { get; set; }



        /// <summary>
        /// 考卷试题选项
        /// </summary>
        [Required]
        public ICollection<ExaminationQuestionSetting> QuestionSettings { get; private set; }

    }
}
