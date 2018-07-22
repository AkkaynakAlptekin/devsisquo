using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Lib.Repos.Interfaces;

namespace Lib.Repos
{
    public class GeneticMutationRepo : IGeneticMutationRepo
    {
        private readonly string _cs;

        public GeneticMutationRepo(string cs)
        {
            _cs = cs;
        }

        public async Task Insert(GeneticMutation gm)
        {
            using (SqlConnection connection = new SqlConnection(_cs))
            {
                var query = @"Insert into dbo.GeneticMutation (IdRaptor, Anno) VALUES (@IdRaptor, @Anno)";

                await connection.QueryAsync(query, gm);
            }
        }
    }
}
