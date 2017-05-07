using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.DataAccess
{
    public class AcquirementRepository : IAcquirementRepository
    {
        private readonly string _connectionString;

        public AcquirementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(Guid id, DateTime acquirementDateUtc, EDbAcquirementStatus status, string isbn, 
                                 int[] copyNumbers, Guid receivingId, Guid givingId)
        {
            const string queryAcquirement =
                @"INSERT INTO ItemAcquirements(Id, AcquirementDateUtc, Status, Item_ISBN, ReceivingLibrary_Id, GivingLibrary_Id)
                  VALUES(@id, @acquirementDateUtc, @status, @isbn, @receivingId, @givingId)";
            const string queryCopy = @"INSERT INTO AcquirementCopies(ItemAcquirement_Id, ItemCopy_Number, ItemCopy_ISBN)
                                       VALUES(@id, @copyNr, @isbn)";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync(queryAcquirement, new
                        {
                            id,
                            acquirementDateUtc,
                            status,
                            isbn,
                            receivingId,
                            givingId
                        }, trans);

                        foreach (var copyNr in copyNumbers)
                        {
                            await conn.ExecuteAsync(queryCopy, new {id, copyNr, isbn}, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<DbAcquirement> GetOne(Guid id)
        {
            const string query = @"SELECT Id, AcquirementDateUtc, Status, Item_ISBN as ItemIsbn, 
                                          ReceivingLibrary_Id as ReceivingLibraryId, GivingLibrary_Id as GivingLibraryId
                                   FROM ItemAcquirements
                                   WHERE Id = @id";
            const string queryCopyNrs = @"SELECT ItemCopy_Number FROM AcquirementCopies
                                          WHERE ItemAcquirement_Id = @id";
            using (var conn = new SqlConnection(_connectionString))
            {
                var dbAcquirement = (await conn.QueryAsync<DbAcquirement>(query, new {id})).SingleOrDefault();
                var copies = await conn.QueryAsync<int>(queryCopyNrs, new {id});
                dbAcquirement.CopyNumbers = copies.ToArray();
                return dbAcquirement;
            }
        }

        public async Task<DbAcquirement> UpdateStatus(Guid acquirementId, EDbAcquirementStatus newStatus)
        {
            const string procedure = "spUpdateAcquirementStatus";

            using (var conn = new SqlConnection(_connectionString))
            {
                var updatedAcquirement = (await conn.QueryAsync<DbAcquirement>(procedure, new
                {
                    AcquirementId = acquirementId,
                    NewStatus = newStatus
                }, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                return updatedAcquirement;
            }
        }
    }
}
