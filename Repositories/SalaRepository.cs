using ApiReuniao.Data;
using ApiReuniao.Models;
using ApiReuniao.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiReuniao.Repositories
{
    /// <summary>
    /// SalaRepository - Classe de repositório. 
    /// Responsável por acessar os dados dos produtos no banco de dados
    /// </summary>
    public class SalaRepository : ISalaRepository
    {
        /// <summary>
        /// AppDbContext - Contexto do banco de dados 
        /// Responsável por gerenciar a conexão com o banco de dados e fornecer acesso
        /// às tabelas e entidades do banco de dados
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor da classe - Recebe o contexto do banco de dados
        /// </summary>
        /// <param name="context"></param>
        public SalaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sala>> GetAll()
            => await _context.Salas.ToListAsync();

        /// <summary>
        /// GetById - Responsável por retornar uma sala específica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Sala> GetById(int id)
            => await _context.Salas.FindAsync(id);

        /// <summary>
        /// Add é responsável por adicionar uma nova
        /// sala ao banco de dados
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        public async Task Add(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update é responsável por atualizar uma sala  
        /// existente no banco de dados
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        public async Task Update(Sala sala)
        {
            var existente = await _context.Salas.FindAsync(sala.Id);

            // Coloca um aviso genérico para não aparecer nenhuma informação importante
            if (existente == null)
                throw new Exception("Produto não encontrado!");

            // Atualiza os valores do objeto existente com os do novo objeto
            _context.Entry(existente).CurrentValues.SetValues(sala);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete é responsável por excluir uma sala do
        /// banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var p = await GetById(id);
            _context.Salas.Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
