using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Lib.Repos.Interfaces;

namespace Lib.Repos
{
    public class RaptorRepo : IRaptorRepo
    {
        private readonly string _cs;

        public RaptorRepo(string cs)
        {
            _cs = cs;
        }


        public Task<Raptor> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Insert(Raptor raptor)
        {
            using (SqlConnection connection = new SqlConnection(_cs))
            {
                var query = @"Insert into dbo.Raptor (Name, Anno) VALUES(@Name, @Anno)";
                await connection.QueryAsync(query, raptor);
            }
        }



        public async Task<List<TrackRaptExternal>> TrackAllRaptors()
        {
            using (SqlConnection connection = new SqlConnection(_cs))
            {
                var query =
                    @"SELECT DISTINCT  R.Id, G.X, G.Y, T.Tempo, R.Name, M.PersoneMangiate from dbo.GeoTracking as G join (	SELECT MAX(Tempo)as Tempo, IdRaptor
				FROM GeoTracking
					GROUP BY IdRaptor) as T
On T.Tempo=G.Tempo AND T.IdRaptor = G.IdRaptor right join dbo.Raptor as R on R.Id = G.idRaptor 
left join(
SELECT DISTINCT G.idRaptor, SUM(D.PersoneMangiate) as PersoneMangiate from dbo.GeoTracking as G 
join dbo.RaptorMetaData as D on G.Id =  D.IdTelemetria
GROUP BY G.idRaptor) as M on M.idRaptor = G.idRaptor";
                return (await connection.QueryAsync<TrackRaptExternal>(query)).ToList();
            }
        }
    }
}
