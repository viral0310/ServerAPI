using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;
using ServerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCInfoController : ControllerBase
    {
        private readonly PCInfoContext _context;

        public PCInfoController(PCInfoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCInfo>>> GetPCInfos()
        {
            return await _context.PCInfos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PCInfo>> GetPCInfo(int id)
        {
            var pcInfo = await _context.PCInfos.FindAsync(id);

            if (pcInfo == null)
            {
                return NotFound();
            }

            return pcInfo;
        }

        [HttpPost]
        public async Task<ActionResult<PCInfo>> PostPCInfo(PCInfo pcInfo)
        {
            _context.PCInfos.Add(pcInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPCInfo), new { id = pcInfo.Id }, pcInfo);
        }
    }
}
