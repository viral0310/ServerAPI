using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;
using ServerAPI.Models;

namespace ServerAPI.Repositories
{
    public class PCInfoRepository : IPCInfoRepository
    {
        private readonly PCInfoContext _context;

        public PCInfoRepository(PCInfoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PCInfo>> GetAllAsync()
        {
            return await _context.PCInfos.ToListAsync();
        }

        public async Task<PCInfo> GetByIdAsync(int id)
        {
            return await _context.PCInfos.FindAsync(id);
        }

        public async Task AddAsync(PCInfo pcInfo)
        {
            _context.PCInfos.Add(pcInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PCInfo pcInfo)
        {
            _context.Entry(pcInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pcInfo = await _context.PCInfos.FindAsync(id);
            if (pcInfo != null)
            {
                _context.PCInfos.Remove(pcInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
