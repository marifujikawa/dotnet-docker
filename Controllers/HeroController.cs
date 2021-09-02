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
            var hero = await _context.Heroes.Where(b => b.Id == id).FirstOrDefaultAsync();

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
            ICollection<Power> powers = heroViewModel.Powers;
            Hero hero = heroViewModel.Hero;

            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            await SaveHeroPowersAsync(hero, powers);
            heroViewModel.Hero = hero;

            return heroViewModel;
        }

        private async Task SaveHeroPowersAsync(Hero hero, ICollection<Power> powers)
        {
            var heroPowers = new List<HeroPowers>();
            foreach (var power in powers)
            {
                var powerId = power.Id;
                if (power.Id == 0)
                {
                    var newPower = await SaveNewPowerAsync(power);
                    powerId = newPower.Id;

                }

                heroPowers.Add(new HeroPowers()
                {
                    HeroId = hero.Id,
                    PowerId = powerId
                });
            }

            _context.AddRange(heroPowers);
            await _context.SaveChangesAsync();
        }

        private async Task<Power> SaveNewPowerAsync(Power power)
        {
            _context.Powers.Add(power);
            await _context.SaveChangesAsync();
            return power;
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
