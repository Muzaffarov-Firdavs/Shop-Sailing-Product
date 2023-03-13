using Dapper;
using Npgsql;
using ShopProductSailing.Data.Configurations;
using ShopProductSailing.Data.IRepositories;
using ShopProductSailing.Domain.Commons;
using System.Data;

namespace ShopProductSailing.Data.Repositories
{
    public class DapperRepository<TResult> : IDapperRepository<TResult> where TResult : Auditable
    {
        private readonly IDbConnection connection = new NpgsqlConnection(DatabasePath.CONNECTION_STRING);

        public async Task DeleteAsync(string query, DynamicParameters parametrs = null,
                                            CommandType type = CommandType.Text)
        {
            await connection.QueryAsync(query, param: parametrs, commandType: type);
        }

        public async Task InsertAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            await connection.ExecuteAsync(query, param: parametrs, commandType: commandType);
        }

        public async Task<List<TResult>> SelectAllAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            return (await connection.QueryAsync<TResult>(query, param: parametrs, commandType: commandType)).ToList();
        } 

        public async Task<TResult> SelectAsync(string query, DynamicParameters parametrs = null,   
                                            CommandType commandType = CommandType.Text)
        {
            return await connection.QueryFirstOrDefaultAsync<TResult>(query, param: parametrs, commandType: commandType);
        }

        public async Task UpdateAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            await connection.QueryAsync<TResult>(query, param: parametrs, commandType: commandType);
        }
    }
}
