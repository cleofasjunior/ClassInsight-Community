using ClassInsight.Domain.Entities;

namespace ClassInsight.Domain.Interfaces;

public interface IInsightRepository
{
    Task AdicionarAsync(Insight insight);
}