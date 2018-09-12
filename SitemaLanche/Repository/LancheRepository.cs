using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitemaLanche.Context;
using SitemaLanche.Models;
using Microsoft.EntityFrameworkCore;

namespace SitemaLanche.Repository
{

    public class LancheRepository : IlanchesRepository
    {

        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => p.IsLanchePreferido).Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
           return _context.Lanches.FirstOrDefault(l => l.lancheId == lancheId);
        }
    }
}
