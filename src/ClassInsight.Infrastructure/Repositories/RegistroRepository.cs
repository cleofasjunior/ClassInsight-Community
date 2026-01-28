using Microsoft.EntityFrameworkCore;
using ClassInsight.Domain.Entities;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Infrastructure.Data;

namespace ClassInsight.Infrastructure.Repositories;

public class RegistroRepository : IRegistroRepository
{
    private readonly AppDbContext _context;

    public RegistroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(RegistroAprendizagem registro)
    {
        await _context.Registros.AddAsync(registro);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RegistroAprendizagem>> ObterUltimosRegistrosAsync(string alunoIdHash, int quantidade)
    {
        return await _context.Registros
            .Where(r => r.AlunoIdHash == alunoIdHash)
            .OrderByDescending(r => r.DataCriacao)
            .Take(quantidade)
            .ToListAsync();
    }
}