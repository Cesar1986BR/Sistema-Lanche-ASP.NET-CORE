using SitemaLanche.Models;
using System.Collections.Generic;

namespace SitemaLanche.Repository
{
    public interface IlanchesRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheById(int lancheId);
    }
}
