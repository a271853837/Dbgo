﻿using Dbgo.Core.Domain.Tasks;
using System;

namespace Dbgo.Data.Mapping.Tasks
{
    public class ScheduleTaskMap:DbgoEntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            this.ToTable("ScheduleTask");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Type).IsRequired();
        }
    }
}
