using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDBZ.Data;
using ProjetoDBZ.Models;

namespace ProjetoDBZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PersonagensController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonagemById(int id)
        {
            var personagem = await _appDbContext.DBZ.FindAsync(id);
            if (personagem == null)
            {
                return NotFound("Personagem não encontrado.");
            }
            return Ok(personagem);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personagem>>> GetPersonagens()
        {
            var personagens = await _appDbContext.DBZ.ToListAsync();
            return Ok(personagens);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagem(Personagem personagem)
        {
            _appDbContext.DBZ.Add(personagem);
            await _appDbContext.SaveChangesAsync();

            return Ok(personagem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonagem(Personagem personagem)
        {
            var existingPersonagem = await _appDbContext.DBZ.FindAsync(personagem.Id);
            if (existingPersonagem == null)
            {
                return NotFound("Personagem não encontrado.");
            }
            existingPersonagem.Nome = !string.IsNullOrWhiteSpace(personagem.Nome) ? personagem.Nome : existingPersonagem.Nome;
            existingPersonagem.Tipo = !string.IsNullOrWhiteSpace(personagem.Tipo) ? personagem.Tipo : existingPersonagem.Tipo;
            await _appDbContext.SaveChangesAsync();
            return Ok(existingPersonagem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePersonagem(int id)
        {
            var personagem = await _appDbContext.DBZ.FindAsync(id);
            if (personagem == null)
            {
                return NotFound("Personagem não encontrado.");
            }
            _appDbContext.DBZ.Remove(personagem);
            await _appDbContext.SaveChangesAsync();
            return Ok("Personagem deletado com sucesso.");
        }
    }
}
