using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TeamMember
    {
        public int IdTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<TaskProject> AssignedProjects { get; set; }
        public List<TaskProject> CreatedProjects { get; set; }
    }
}
