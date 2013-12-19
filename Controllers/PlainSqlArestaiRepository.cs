using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using hue2.Models;
using Oracle.ManagedDataAccess.Client;

namespace hue2.Controllers
{
    public class PlainSqlArestaiRepository : IArestaiRepository
    {
        public IEnumerable<ArestaiModels> GetArestai()
        {
            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ZALTYS"].ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select t.VLD_ARE_ID, v.VALDOS_KODAS, t.DATA_NUO, t.DATA_IKI 
                                            from system.VALDU_ARESTAI t,
                                                 system.VALDOS v
                                            where t.vld_id = v.vld_id
                                            order by 2,3 desc";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new ArestaiModels
                            {
                                Id = reader.GetInt32(0),
                                VldId = reader.GetInt64(1),
                                DataNuo = reader.GetDateTime(2),
                                DataIki = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
        }


        public void ArestuotiValda(long valdoskodas)
        {
            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ZALTYS"].ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    string query = @"INSERT INTO system.valdu_arestai
                                     (vld_id, data_nuo)
                                     SELECT t.vld_id, sysdate
                                     FROM system.valdos t
                                     WHERE t.valdos_kodas = @valdos_kodas;
                                     commit;";
                    cmd.CommandText = query;
                    OracleParameter param = new OracleParameter("@valdos_kodas", valdoskodas);
                    cmd.Parameters.Add(param);

                }
            }
        }

    }
}