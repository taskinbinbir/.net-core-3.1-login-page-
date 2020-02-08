using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        public trialContext _context;

        public PeopleController()
        {
            _context = new trialContext();
        }

        //GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> HowToPerson(Person person)
        {        
            try
            {
                Person p = (from k in _context.Person
                            where k.Name.Equals(person.Name) && k.Surname.Equals(person.Surname)
                            select k).FirstOrDefault();

                if (p != null)
                {
                    //return StatusCode(StatusCodes.Status200OK);
                    //return "200";
                    //return this.Content(HttpStatusCode.OK, new { response = "Hello" });
                    return Ok(p);
                }
                else
                {
                    //return StatusCode(StatusCodes.Status403Forbidden);
                    //return "403";
                    return Ok(0);
                }
            }
            catch(Exception e)
            {
                //return StatusCode(StatusCodes.Status404NotFound);
                //return "404";
                return NotFound();
            }



        }


        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Person>> PostPerson(Person person)
        //{
        //    _context.Person.Add(person);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        //}

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
