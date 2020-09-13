using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using rcn.api.Data;

namespace rcn.api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository repository;

        public ProdutosController(IProdutoRepository _repository)
        {
            repository = _repository;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Criar([FromBody]Produto produto){
            if(produto.Codigo == "")
                return BadRequest("Código do produto não informado!");

            if(string.IsNullOrEmpty(produto.Descricao))
                return BadRequest("Descrição do produto não informado!");

            repository.Inserir(produto);
            return Created(nameof(Criar), produto);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces("application/json", "application/xml")]
        public IActionResult Obter(){
            var lista = repository.Obter();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Obter(int id){
            var produto = repository.Obter(id);
            
            if(produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Atualizar([FromBody]Produto produto){
            var prod = repository.Obter(produto.Id);
            
            if(prod == null)
                return NotFound();

            if(produto.Codigo == "")
                return BadRequest("Código do produto não informado!");

            if(string.IsNullOrEmpty(produto.Descricao))
                return BadRequest("Descrição do produto não informado!");

            repository.Editar(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Apagar(int id){
            var produto = repository.Obter(id);

            if(produto == null)
                return NotFound();

            repository.Excluir(produto);
            return Ok();
        }

        [HttpGet("{codigo}")]
        [ApiVersion("2.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult ObterPorCodigo(string codigo){
            return Ok("Método obter por codigo - versão 2");
        }

        [HttpGet("")]
        [ApiVersion("3.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult ObterTodos(){
            List<string> lista = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                lista.Add($"índice: {i}");
            }

            return Ok(string.Join(",",lista));
        }
    }
}