using System.Linq.Expressions;
using Adenawell_ValentinAP1_P1.DAL;
using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Adenawell_ValentinAP1_P1.Services;

public class EntradasHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{

    
        public async Task<bool> Guardar(EntradasHuacales entradasHuacales)
        {
            if(await Existe(entradasHuacales))
            {
              return false;
            }
            if(!await Existe(entradasHuacales))
            {
              return await Insertar(entradasHuacales);
            }
           else
           {
             return await Modificar(entradasHuacales);
           }

        }

        public async Task<bool> Existe(EntradasHuacales entradasHuacales)
        {
          await using var contexto = await DbFactory.CreateDbContextAsync();
          return await contexto.EntradasHuacales.AnyAsync(e => e.IdEntrada == entradasHuacales.IdEntrada);
        }

        private async Task<bool> Insertar(EntradasHuacales entradasHuacales)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.EntradasHuacales.Add(entradasHuacales);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(EntradasHuacales entradasHuacales)
        {
         await using var contexto = await DbFactory.CreateDbContextAsync();
         contexto.EntradasHuacales.Update(entradasHuacales);
         return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<EntradasHuacales> Buscar(EntradasHuacales entradasHuacales)
        {
         await using var contexto = await DbFactory.CreateDbContextAsync();
         return await contexto.EntradasHuacales.FirstOrDefaultAsync(e => e.NombreCliente == entradasHuacales.NombreCliente);
        }

        public async Task<bool> Eliminar(EntradasHuacales entradasHuacales)
        {
          await using var contexto = await DbFactory.CreateDbContextAsync();
          return await contexto.EntradasHuacales.Where(e => e.IdEntrada == entradasHuacales.IdEntrada).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.EntradasHuacales.Where(criterio).AsNoTracking().ToListAsync();
        }
    
}
