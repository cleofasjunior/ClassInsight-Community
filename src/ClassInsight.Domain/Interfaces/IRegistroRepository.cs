using ClassInsight.Domain.Entities;

namespace ClassInsight.Domain.Interfaces;

public interface IRegistroRepository
{
    Task AdicionarAsync(RegistroAprendizagem registro);
    
    // Busca histórico para alimentar a IA (RAG Simplificado)
    Task<List<RegistroAprendizagem>> ObterUltimosRegistrosAsync(string alunoIdHash, int quantidade);
}