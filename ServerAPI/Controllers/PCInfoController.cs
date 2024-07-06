using Microsoft.AspNetCore.Mvc;
using ServerAPI.Models;
using ServerAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PCInfoController : ControllerBase
    {
        private readonly IPCInfoRepository _pcInfoRepository;

        public PCInfoController(IPCInfoRepository pcInfoRepository)
        {
            _pcInfoRepository = pcInfoRepository;
        }

        // GET: api/pcinfo
        [HttpGet]
        public async Task<IEnumerable<PCInfo>> Get()
        {
            return await _pcInfoRepository.GetAllAsync();
        }

        // GET: api/pcinfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCInfo>> GetById(int id)
        {
            var pcInfo = await _pcInfoRepository.GetByIdAsync(id);

            if (pcInfo == null)
            {
                return NotFound();
            }

            return pcInfo;
        }

        // POST: api/pcinfo
        [HttpPost]
        public async Task<IActionResult> Post(PCInfo pcInfo)
        {
            await _pcInfoRepository.AddAsync(pcInfo);

            return CreatedAtAction(nameof(GetById), new { id = pcInfo.Id }, pcInfo);
        }

        // PUT: api/pcinfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PCInfo pcInfo)
        {
            if (id != pcInfo.Id)
            {
                return BadRequest();
            }

            await _pcInfoRepository.UpdateAsync(pcInfo);

            return NoContent();
        }

        // DELETE: api/pcinfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pcInfoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
