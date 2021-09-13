using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;

namespace Roommates.Repositories
{
   public class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<Chore> GetAll()
        {
            using(SqlConnection conn = Connection)
           {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name From Chore";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Chore> chores = new List<Chore>();
                    while(reader.Read())
                    {
                        int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                        string nameValue = reader.GetString(reader.GetOrdinal("Name"));
                        Chore chore = new Chore()
                        {
                            Id = idValue,
                            Name = nameValue,
                        };
                        chores.Add(chore);
                    }

                    reader.Close();

                    return chores;
                }
            }
        }
    }

    public List<Chore> GetUnassignedChore()
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Chore.Name, RoommateChore.ChoreId FROM RoommateChore full join Chore on Chore.id = ChoreId WHERE ChoreId IS NULL";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Chore> chores = new List<Chore>();
                while (reader.Read())
                {
                    int idValue = reader.GetInt32(reader.GetOrdinal("Id"));
                    string nameValue = reader.GetString(reader.GetOrdinal("Name"));

                    Chore chore = new Chore
                    {
                        Id = idValue,
                        Name = nameValue,

                    };
                    chores.Add(chore);
                }
                reader.Close();

                return chores;
            }
        }
    }
}