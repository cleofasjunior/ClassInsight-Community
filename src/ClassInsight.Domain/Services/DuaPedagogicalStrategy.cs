using ClassInsight.Domain.Entities;
using ClassInsight.Domain.Enums;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Domain.Services;

public class DuaPedagogicalStrategy : IDuaStrategy
{
    public SugestaoDua GerarSugestao(RegistroAprendizagem registro)
    {
        if (registro.Analise == null) 
            throw new InvalidOperationException("Análise necessária antes da sugestão.");

        var analise = registro.Analise;

        // REGRA 1: Frustração com Escrita -> Sugerir Oralidade (Ação e Expressão)
        bool odeiaEscrever = analise.SentimentoGeral == TipoSentimento.Negativo &&
                             (analise.PalavrasChave.Contains("escrever") || analise.PalavrasChave.Contains("redação"));

        if (odeiaEscrever)
        {
            return new SugestaoDua(
                "Adaptação de Expressão",
                "Aluno frustrado com escrita. Permitir resposta via áudio.",
                PrincipioDua.AcaoEExpressao,
                "Gravador / Speech-to-Text"
            );
        }

        // REGRA 2: Medo/Ansiedade -> Acolhimento Visual (Representação)
        if (analise.PalavrasChave.Contains("medo") || analise.PalavrasChave.Contains("não entendi"))
        {
            return new SugestaoDua(
                "Reforço Visual",
                "Aluno ansioso/inseguro. Oferecer vídeo explicativo antes da teoria.",
                PrincipioDua.Representacao,
                "Vídeo Curto / Infográfico"
            );
        }

        // REGRA PADRÃO: Manter engajamento
        return new SugestaoDua(
            "Manutenção",
            "Seguir planejamento, inserindo elementos lúdicos.",
            PrincipioDua.Engajamento,
            "Gamificação"
        );
    }
}