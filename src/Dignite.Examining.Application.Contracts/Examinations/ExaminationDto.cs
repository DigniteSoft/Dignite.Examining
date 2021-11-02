using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Examinations
{
    public class ExaminationDto: FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 考卷标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 考卷说明、须知
        /// </summary>
        public string Announcement { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// 考卷设置项
        /// </summary>
        public ExaminationSetting Settings { get; set; }



        /// <summary>
        /// 考卷试题选项
        /// </summary>
        public ICollection<ExaminationQuestionSetting> QuestionSettings { get; private set; }

    }
}
