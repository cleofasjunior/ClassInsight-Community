using ClassInsight.Domain.Entities;
using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Domain.Interfaces;

public interface IDuaStrategy
{
    SugestaoDua GerarSugestao(RegistroAprendizagem registro);
}