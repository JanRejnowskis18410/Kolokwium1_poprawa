using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Project
    {
        public int ProjectName { get; set; }
        public DateTime Deadline { get; set; }
        public string  Description { get; set; }
        public string TaskName { get; set; }
    }
}
