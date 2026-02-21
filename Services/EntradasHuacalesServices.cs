using System.Linq.Expressions;
using Adenawell_ValentinAP1_P1.DAL;
using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Adenawell_ValentinAP1_P1.Services;

public class EntradasHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{
   

    public async Task<bool> Guardar(EntradasHuacales entradasHuacales)
    {
        if (entradasHuacales.IdEntrada != 0)
        {
            return await Modificar(entradasHuacales);
        }

        else
        {
            if (await Existe(entradasHuacales))
            {
                return false;
            }

            return await Insertar(entradasHuacales);
        }
    }

    private async Task<bool> Insertar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var tipo = await contexto.TiposHuacales.FindAsync(entrada.TipoId);
        if (tipo != null)
            tipo.Existencia += entrada.Cantidad ?? 0;

        contexto.EntradasHuacales.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entradaAnterior = await contexto.EntradasHuacales
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == entrada.IdEntrada);

        if (entradaAnterior == null) return false;

        var tipo = await contexto.TiposHuacales.FindAsync(entrada.TipoId);
        if (tipo != null)
        {

            tipo.Existencia = (tipo.Existencia - (entradaAnterior.Cantidad ?? 0)) + (entrada.Cantidad ?? 0);
        }

        contexto.EntradasHuacales.Update(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(e => e.IdEntrada != entrada.IdEntrada
                      && e.NombreCliente.ToLower() == entrada.NombreCliente.ToLower());
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await contexto.EntradasHuacales.FindAsync(id);

        if (entrada != null)
        {
            var tipo = await contexto.TiposHuacales.FindAsync(entrada.TipoId);
            if (tipo != null)
                tipo.Existencia -= entrada.Cantidad ?? 0;

            contexto.EntradasHuacales.Remove(entrada);
        }
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<TiposHuacales>> GetTipos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.AsNoTracking().ToListAsync();
    }

    public async Task<EntradasHuacales?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == id);
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}