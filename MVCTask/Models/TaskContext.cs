using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCTask.Models
{
    public class TaskContext:DbContext
    {
        public TaskContext() : base("task") { }
        public DbSet<MessageDetails> msgDetails { get; set; }
    }
}