using ApiReuniao.Models;
using ApiReuniao.Repositories.Interfaces;

namespace ApiReuniao.Services
{
    /// <summary>
    /// SalaService - Classe de serviço responsável
    /// por toda a lógica de negócios relacionada a salas de reunião
    /// </summary>
    public class SalaService
    {
        /// <summary>
        /// Repository de salas - Responsável por acessar os
        /// dados das salas no banco de dados
        /// </summary>
        private readonly ISalaRepository _repo;

        /// <summary>
        /// Construtor da classe - Recebe o repository de salas
        /// via injeção de dependência
        /// </summary>
        /// <param name="repo"></param>
        public SalaService(ISalaRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Lista todoas as salas - Chama o método GetAll do
        /// repository para obter a lista de salas do banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sala>> Listar()
            => await _repo.GetAll();

        /// <summary>
        /// Obter uma sala por id - Chama o método GetById
        /// do repository para obter uma sala específica
        /// do banco de dados com base no id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Sala> ObterPorId(int id)
            => await _repo.GetById(id);

        /// <summary>
        /// Criar uma nova sala - Chama o método Add
        /// do repository para adicionar uma nova sala ao banco de dados
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        public async Task Criar(Sala sala)
            => await _repo.Add(sala);

        /// <summary>
        /// Atualizar uma sala existente - Chama 
        /// o método Update do repository
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        public async Task Atualizar(Sala sala)
            => await _repo.Update(sala);

        /// <summary>
        /// Deletar um a sala por id -  Chama 
        /// o método Delete do repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Deletar(int id)
            => await _repo.Delete(id);
    }
}
