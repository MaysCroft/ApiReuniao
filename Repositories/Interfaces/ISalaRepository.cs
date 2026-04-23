using ApiReuniao.Models;

namespace ApiReuniao.Repositories.Interfaces
{
    /// <summary>
    /// Interface ISalaRepository - Define os métodos 
    /// que o repositório de produtos deve implementar.
    /// </summary>
    public interface ISalaRepository
    {
        Task<List<Sala>> GetAll();
        Task<Sala> GetById(int id);
        Task Add(Sala sala);
        Task Update(Sala sala);
        Task Delete(int id);
    }
}
