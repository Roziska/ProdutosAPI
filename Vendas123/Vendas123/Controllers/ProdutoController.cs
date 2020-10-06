using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Vendas123.DAL;
using Vendas123.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vendas123.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IRepository<Produto> _repo;

        public ProdutoController(IRepository<Produto> repository)
        {
            _repo = repository;
        }
        [HttpGet]
        public IActionResult ListaDeProdutos()
        {
            var lista = _repo.All.ToList();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Recuperar(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public IActionResult Incluir([FromForm] Produto model)
        {
            if (ModelState.IsValid)
            {
                var produto = model;
                _repo.Incluir(produto);
                var uri = Url.Action("Recuperar", new { id = produto.Id });
                return Created(uri, produto); //201
            }
            return BadRequest();
        }

        // PUT api/<ProdutoController>/5
        [HttpPut]
        public IActionResult Alterar([FromForm] Produto produto)
        {
            if (ModelState.IsValid)
            {               
                _repo.Alterar(produto);
                return Ok(); //200
            }
            return BadRequest();
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Excluir(model);
            return NoContent(); //203
        }
    }
}
