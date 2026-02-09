using System.Linq.Expressions;
using Adenawell_ValentinAP1_P1.DAL;
using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Adenawell_ValentinAP1_P1.Services;

public class EntradasHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{

    
        public async Task<bool> Guardar()
        {
            return false;
        }

        private async Task<bool> Existe()
        {
            return false;
        }

        private async Task<bool> Insertar()
        {
            return false;
        }

        private async Task<bool> Modificar()
        {
            return false;
        }

        public async Task<bool> Buscar()
        {
            return false;
        }

        public async Task<bool> Eliminar()
        {
            return false;
        }

        public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.EntradasHuacales.Where(criterio).AsNoTracking().ToListAsync();
        }
    
}
