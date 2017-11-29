using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Domain.Tasks
{
    public partial class ScheduleTask : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置运行周期（以秒为单位）
        /// </summary>
        public int Seconds { get; set; }

        public string Type { get; set; }

        public bool Enabled { get; set; }
        public bool StopOnError { get; set; }
        public string LeasedByMachineName { get; set; }

        public DateTime? LeasedUntilUtc { get; set; }
        /// <summary>
        /// 任务最近运行开始时间
        /// </summary>
        public DateTime? LastStartUtc { get; set; }
        /// <summary>
        /// 任务最新运行结束时间
        /// </summary>
        public DateTime? LastEndUtc { get; set; }
        /// <summary>
        /// 任务运行成功时间
        /// </summary>
        public DateTime? LastSuccessUtc { get; set; }
    }
}
