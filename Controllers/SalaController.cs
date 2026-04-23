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
        /// GET: api/sala - Retorna a lista de salas
        ///     
        /// Observação: 
        /// - O ID deve ser um número inteiro positivo. 
        /// Se um ID inválido for fornecido (como um número negativo ou zero), 
        /// a API retornará um erro 400 Bad Request com uma mensagem explicativa.
        /// </remarks>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Salas encontradas</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var salas = await _service.Listar();
                return Ok(new { mensagem = "Salas de reuniões encontradas!", salas });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao listar salas : {ex.Message}" });
            }
        }

        /// <summary>
        /// GET: api/sala/Id - Retorna uma sala específica por ID
        /// </summary>
        /// 
        /// <remarks>
        /// GET: api/sala/Id - Retorna uma sala específica por ID
        /// 
        /// Exemplo de requisição:
        ///     {
        ///        "id": 1
        ///     }
        ///     
        /// Observação: 
        /// - O ID deve ser um número inteiro positivo. 
        /// Se um ID inválido for fornecido (como um número negativo ou zero), 
        /// a API retornará um erro 400 Bad Request com uma mensagem explicativa.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala encontrada</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensagem = "O ID informado deve ser maior que zero" });

            try
            {
                var sala = await _service.ObterPorId(id);

                if (sala == null)
                    return NotFound(new { mensagem = $"Sala com ID {id} não encontrada." });

                return Ok(new { mensagem = "Sala de reuniões encontrada!", sala });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao buscar sala : {ex.Message}" });
            }
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
        /// <response code="201">Sala criada com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Sala sala)
        {
            try
            {
                if (sala == null ||
                    string.IsNullOrWhiteSpace(sala.Nome) ||
                    sala.Capacidade <= 0 ||
                    sala.PrecoHora <= 0 ||
                    string.IsNullOrWhiteSpace(sala.PossuiProjetor) ||
                    string.IsNullOrWhiteSpace(sala.Status))
                {
                    return BadRequest(new { mensagem = "Todos os campos são obrigatórios!" });
                }

                await _service.Criar(sala);
                return CreatedAtAction(nameof(GetById), new { id = sala.Id }, sala);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao criar registro : {ex.Message}" });
            }
        }

        /// <summary>
        /// PUT: api/sala/Id - Atualiza um produto existente por ID
        /// </summary>
        /// 
        /// <remarks>
        /// PUT: api/sala/Id - Atualiza todos os campos de uma sala existente.
        /// 
        /// Obrigatório que o **ID enviado na URL** seja exatamente igual ao **ID enviado no corpo (JSON)**.
        /// Se os IDs forem diferentes, a requisição retornará um erro 400 Bad Request.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <param name="sala"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Sala de reuniões atualizada com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(int id, [FromBody] Sala sala)
        {
            try
            {
                if (id != sala.Id)
                    return BadRequest(new { mensagem = "ID da URL diferente do corpo" });

                var existente = await _service.ObterPorId(id);

                if (existente == null)
                    return NotFound(new { mensagem = "Sala não encontrada!" });

                await _service.Atualizar(sala);
                return Ok("Sala de reuniões atualizada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro na atualização : {ex.Message}" });
            }
        }

        /// <summary>
        /// DELETE: api/sala/Id - Deleta uma sala por ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sala de reuniões excluída com sucesso</response>
        /// <response code="400">ID inválido</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                    return NotFound(new { mensagem = "Sala de reuniões não encontrada!" });

                await _service.Deletar(id);
                return Ok(new { mensagem = "Sala deletada!!!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao deletar pacote de registro {id} : {ex.Message}" });
            }
        }
    }
}
