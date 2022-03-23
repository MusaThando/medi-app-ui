using Dapper;
using MedicalAppUI.Models;
using MedicalAppUI.SQL;
using System.Data;
using System.Data.SqlClient;

namespace MedicalAppUI.DAL
{
    public class DataAccess
    {
        public void PostDocument(CDocumentRepository cDocumentRepository)
        {
            string query = "[dbo].[PostDocument]";

            using (IDbConnection db = new SqlConnection(Helper.GetConnectionString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@FileNamefld", cDocumentRepository.FileNamefld, DbType.String, ParameterDirection.Input);
                parameter.Add("@FileTypefld", cDocumentRepository.FileTypefld, DbType.String, ParameterDirection.Input);
                parameter.Add("@Publicfld", cDocumentRepository.Publicfld, DbType.Boolean, ParameterDirection.Input);
                parameter.Add("@FileContentfld", cDocumentRepository.FileContentfld, DbType.Binary, ParameterDirection.Input);
                parameter.Add("@SourceRecordGUIDfld", cDocumentRepository.SourceRecordGUIDfld, DbType.Guid, ParameterDirection.Input);

                db.Execute(query, parameter, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
