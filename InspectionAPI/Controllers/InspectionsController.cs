using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InspectionAPI;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Inspections
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Inspection>>> GetInspection()
        //{
        //    return await _context.Inspection                 
        //        .ToListAsync();
        //}



        // GET: api/Inspections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspection()
        {
            return await _context.Inspection.Include("InspectionType").ToListAsync();
        }



        // GET: api/Inspections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inspection>> GetInspection(int id)
        {
            var inspection = await _context.Inspection
                .FindAsync(id);  

            if (inspection == null)
            {
                return NotFound();
            }

            return inspection;
        }

        // PUT: api/Inspections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspection(int id, Inspection inspection)
        {
            if (id != inspection.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inspections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inspection>> PostInspection(Inspection inspection)
        {
            _context.Inspection.Add(inspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspection", new { id = inspection.Id }, inspection);
        }

        // DELETE: api/Inspections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspection(int id)
        {
            var inspection = await _context.Inspection.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }

            _context.Inspection.Remove(inspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspection.Any(e => e.Id == id);
        }
    }
}
