using Dapper;
using ShopProductSailing.Domain.Commons;
using System.Data;

namespace ShopProductSailing.Data.IRepositories
{
    public interface IDapperRepository<TResult> where TResult : Auditable
    {
        Task InsertAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task UpdateAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task DeleteAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task<TResult> SelectAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task<List<TResult>> SelectAllAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);

    }
}
