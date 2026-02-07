using System.Linq.Expressions;
using Adenawell_ValentinAP1_P1.DAL;
using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Adenawell_ValentinAP1_P1.Services;

public class ViajesEspacialesServices
{

    public class ViajesEspacialesService(IDbContextFactory<Contexto> DbFactory)
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

        public async Task<List<ViajesEspaciales>> Listar(Expression<Func<ViajesEspaciales, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.ViajesEspaciales.Where(criterio).AsNoTracking().ToListAsync();
        }
    }
}
