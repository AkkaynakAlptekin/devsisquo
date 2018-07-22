using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Lib.Repos.Interfaces;

namespace Lib.Repos
{
    public class GeoTrackingRepo : IGeoTrackingRepo
    {
        private readonly string _cs;

        public GeoTrackingRepo(string cs)
        {
            _cs = cs;
        }


        public async Task Insert(GeoTracking gt)
        {
            using (SqlConnection connection = new SqlConnection(_cs))
            {
                var query = @"Insert into dbo.GeoTracking (X, Y, IdRaptor, Tempo, IdWinningGeneticMutation) VALUES
                            (@X, @Y, @IdRaptor, @Tempo, @IdWinningGeneticMutation)";
                await connection.QueryAsync(query, gt);

            }
        }

        public async Task InsertWithMetaData(GeoTracking gt, RaptorMetaData rmd)
        {
            using (SqlConnection connection = new SqlConnection(_cs))
            {
                var query = @"declare @idGeoTracking as int;
                            Insert into dbo.GeoTracking (X, Y, IdRaptor, Tempo, IdWinningGeneticMutation) VALUES
                                    (@X, @Y, @IdRaptor, @Tempo, @IdWinningGeneticMutation);
                            SET @idGeoTracking = SCOPE_IDENTITY();
                            Insert into dbo.RaptorMetaData (PersoneMangiate, FauciAperte, IdTelemetria) VALUES (@PersoneMangiate,
                                @FauciAperte, @idGeoTracking);";
                await connection.QueryAsync(query, new
                {
                    X = gt.X,
                    Y = gt.Y,
                    IdRaptor = gt.IdRaptor,
                    Tempo = gt.Tempo.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    IdWinningGeneticMutation = gt.IdWinningGeneticMutation,
                    PersoneMangiate = rmd.PersoneMangiate,
                    FauciAperte = rmd.FauciAperte
            });

            }
        }
    }
}
