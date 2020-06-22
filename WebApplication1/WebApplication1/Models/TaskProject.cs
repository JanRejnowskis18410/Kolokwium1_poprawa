using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TaskProject
    {
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public DateTime Deadline { get; set; }
        public string TaskTypeName { get; set; }
        public string TaskName { get; set; }
    }
}
