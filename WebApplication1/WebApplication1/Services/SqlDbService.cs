using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SqlDbService : IDbService
    {
        private readonly string _conString = "Data Source=db-mssql;Initial Catalog=s18410;Integrated Security=True";

        public void DeleteProject(int id)
        {
            using (var con = new SqlConnection(_conString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                try
                {
                    com.Transaction = con.BeginTransaction();

                    com.CommandText = "select 1 from Project where IdTeam = @id";
                    com.Parameters.AddWithValue("id", id);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        throw new Exception($"Project with id={id} does not exist!");
                    }
                    dr.Close();

                    var list = new List<int>();
                    com.CommandText = "select IdTask from Task where IdTeam = @id;";
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(int.Parse(dr["IdTask"].ToString()));
                    }
                    dr.Close();

                    if(list.Any())
                    {
                        foreach (var idTask in list)
                        {
                            com.CommandText = "delete from Task where idTask = @idTask";
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("idTask", idTask);
                            com.ExecuteNonQuery();
                        }
                    }

                    com.CommandText = "delete from Project where IdTeam = @id";
                    com.Parameters.AddWithValue("id", id);
                    com.ExecuteNonQuery();

                    com.Transaction.Commit();
                } catch (SqlException exc)
                {
                    com.Transaction.Rollback();
                    throw new Exception(exc.Message);
                }
            }
        }

        public TeamMember GetTeamMember(int id)
        {
            TeamMember teamMember = new TeamMember();
            using (var con = new SqlConnection(_conString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                com.CommandText = @"select * from TeamMember where IdTeamMember = @id";
                com.Parameters.AddWithValue("id", id);
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    throw new Exception($"Team member with id = {id} does not exist!");
                }
                com.Parameters.AddWithValue("id", id);
                teamMember.FirstName = dr["FirstName"].ToString();
                teamMember.LastName = dr["LastName"].ToString();
                teamMember.Email = dr["Email"].ToString();
                teamMember.IdTeamMember = int.Parse(dr["IdTeamMember"].ToString());
                dr.Close();

                var assigned = new List<TaskProject>();
                com.CommandText = @"select t.Name as TaskName, t.Description, p.Name as ProjectName, t.Deadline, tt.Name as TaskTypeName from Task t 
join Project p on p.IdTeam = t.IdTeam
join TaskType tt on tt.IdTaskType = t.IdTaskType
where t.IdAssignedTo = @id
order by t.Deadline desc";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("id", id);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var p = new TaskProject()
                    {
                        TaskTypeName = dr["TaskTypeName"].ToString(),
                        Deadline = DateTime.Parse(dr["Deadline"].ToString()),
                        Description = dr["Description"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        TaskName = dr["TaskName"].ToString()
                    };
                    assigned.Add(p);
                }
                teamMember.AssignedProjects = assigned;
                dr.Close();

                var createdProjects = new List<TaskProject>();
                com.CommandText = @"select t.Name as TaskName, t.Description, p.Name as ProjectName, t.Deadline, tt.Name as TaskTypeName from Task t 
join Project p on p.IdTeam = t.IdTeam
join TaskType tt on tt.IdTaskType = t.IdTaskType
where t.IdCreator = @id
order by t.Deadline desc";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("id", id);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var p = new TaskProject()
                    {
                        TaskTypeName = dr["TaskTypeName"].ToString(),
                        Deadline = DateTime.Parse(dr["Deadline"].ToString()),
                        Description = dr["Description"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        TaskName = dr["TaskName"].ToString()
                    };
                    createdProjects.Add(p);
                }
                teamMember.CreatedProjects = createdProjects;
                dr.Close();
            }
            return teamMember;
        }
    }
}
