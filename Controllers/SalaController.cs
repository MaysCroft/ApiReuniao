using ApiReuniao.Models;
using ApiReuniao.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiReuniao.Controllers
{
    /// <summary>
    /// API de produtos - Controller para gerenciar salas de reunião
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        /// <summary>
        /// Instancia do serviço de salas - Responsável por toda 
        /// a lógica de negócios relacionada a salas
        /// </summary>
        private readonly SalaService _service;

        /// <summary>
        /// Construtor da classe - Recebe o serviço de salas via 
        /// injeção de dependência
        /// </summary>
        /// <param name="service"></param>
        public SalaController(SalaService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET: api/sala - Retorna a lista de salas
        /// </summary>
        /// 
        /// <remarks>
        /// GET: api/sala:
        /// 
        /// Retorna uma lista completa de todas as salas de reunião 
        /// cadastradas no banco de dados.
        /// 
        /// Se não houver salas cadastradas, a API retornará uma 
        /// mensagem com o status 200 OK.
        /// </remarks>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala excluída com sucesso!!!</response>
        /// <response code="204">Conteúdo inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var salas = await _service.Listar();
            return Ok(salas);
        }

        /// <summary>
        /// GET: api/sala/Id - Retorna uma sala específica por ID
        /// </summary>
        /// 
        /// <remarks>
        /// GET: api/sala/Id: 
        /// 
        /// Busca os detalhes de uma sala específica utilizando seu identificador único (ID).
        /// Caso o ID informado não exista na base de dados, será retornado o status 404 Not Found.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala excluída com sucesso!!!</response>
        /// <response code="204">Conteúdo inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var sala = await _service.ObterPorId(id);

            if (sala == null)
                return NotFound();

            return Ok(sala);
        }

        /// <summary>
        /// POST: api/sala - Cria uma nova sala de reunião
        /// </summary>
        /// 
        /// <remarks>
        /// POST: api/sala - Cria uma nova sala de reunião
        /// 
        /// Exemplo de requisição:
        ///     {
        ///        "nome": "Sala de Reunião 1",
        ///        "capacidade": 20,
        ///        "precoHora": 750,
        ///        "possuiProjetor": "Sim",
        ///        "status": "Reservada"
        ///     }
        /// </remarks>
        /// 
        /// <param name="sala"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala excluída com sucesso!!!</response>
        /// <response code="204">Conteúdo inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Sala sala)
        {
            await _service.Criar(sala);
            return CreatedAtAction(nameof(GetById), new { id = sala.Id }, sala);
        }

        /// <summary>
        /// PUT: api/sala/Id - Atualiza um produto existente por ID
        /// </summary>
        /// 
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <param name="sala"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala excluída com sucesso!!!</response>
        /// <response code="204">Conteúdo inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(int id, [FromBody] Sala sala)
        {
            try
            {
                if (id != sala.Id)
                    return BadRequest("ID da URL diferente do corpo");

                var existente = await _service.ObterPorId(id);

                if (existente == null)
                    return NotFound("Produto não encontrado!");

                await _service.Atualizar(sala);
                return Ok("Sala de reuniões atuializada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro na atualização : {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/sala/Id - Deleta uma sala por ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sala excluída com sucesso!!!</response>
        /// <response code="204">Conteúdo inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensagem = "Código Inválido!! Apenas números positivos!!" });

            try
            {
                var existence = await _service.ObterPorId(id);

                if (existence == null)
                    return NotFound("Não encontrado!!!");

                await _service.Deletar(id);
                return Ok("Sala deletada!!!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar pacote de registro {id} : {ex.Message}");
            }
        }
    }
}
