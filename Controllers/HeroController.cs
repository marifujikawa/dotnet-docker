using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste.Models;
using teste.ViewModel;

namespace teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly testeContext _context;

        public HeroController(testeContext context)
        {
            _context = context;
        }

        // GET: api/Hero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
        {
            return await _context.Heroes.ToListAsync();
        }

        // GET: api/Hero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        // PUT: api/Hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
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

        // POST: api/Hero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeroPowersViewModel>> PostHero(HeroPowersViewModel heroViewModel)
        {
            Hero hero = new Hero()
            {
                Name = heroViewModel.Name
            };
            List<Power> powers = heroViewModel.Powers;

            List<HeroPower> heroPowers = new List<HeroPower>();
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            if (powers != null)
            {


                foreach (Power power in powers)
                {
                    heroPowers.Add(new HeroPower()
                    {
                        HeroId = hero.Id,
                        PowerId = power.Id
                    });
                }
                _context.AddRange(heroPowers);
                await _context.SaveChangesAsync();

            }
            return heroViewModel;
        }


        // DELETE: api/Hero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
