using System.Collections.Generic;
using System.Threading.Tasks;
using ServerAPI.Models;

namespace ServerAPI.Repositories
{
    public interface IPCInfoRepository
    {
        Task<IEnumerable<PCInfo>> GetAllAsync();
        Task<PCInfo> GetByIdAsync(int id);
        Task AddAsync(PCInfo pcInfo);
        Task UpdateAsync(PCInfo pcInfo);
        Task DeleteAsync(int id);
    }
}
