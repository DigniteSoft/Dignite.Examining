using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Examining.Examinations
{

    /// <summary>
    /// 考试的设定项
    /// 作为试卷的设置选项，以JSON方式存储，不映射至数据库；
    /// </summary>
    public class ExaminationSetting
    {
        public ExaminationSetting()
        {
            this.LimitExaminationTime = 60;
            this.MaxAnswerNumber = 1;
            this.IsPublicRanking = true;
        }

        /// <summary>
        /// 最多考试次数
        /// </summary>
        [Range(1, 100)]
        public int MaxAnswerNumber { get; set; }

        /// <summary>
        /// 设定有效试卷的方式
        /// </summary>
        [Required]
        public ActiveScoreMode ActiveScoreMode { get; set; }


        /// <summary>
        /// 开始考试时间
        /// </summary>
        public DateTime? EffectivelyTime { get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? ExpiryTime { get; set; }

        /// <summary>
        /// 答题时长限定（分钟），默认值60；
        /// </summary>
        [Required]
        public int LimitExaminationTime { get; set; }


        /// <summary>
        /// 是否公开排行榜
        /// </summary>
        public bool IsPublicRanking { get; set; }
    }

    /// <summary>
    /// 有效成绩的设定方式
    /// </summary>
    public enum ActiveScoreMode
    {
        /// <summary>
        /// 以最高分为准
        /// </summary>
        Highest,

        /// <summary>
        /// 以最后一次考试为准
        /// </summary>
        Lasted
    }
}
