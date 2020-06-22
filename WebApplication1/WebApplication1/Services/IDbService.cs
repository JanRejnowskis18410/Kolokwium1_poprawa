using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        public TeamMember GetTeamMember(int id);
        public void DeleteProject(int id);
    }
}
