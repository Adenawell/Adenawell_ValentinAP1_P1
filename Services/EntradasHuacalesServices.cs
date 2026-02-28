using System.Linq.Expressions;
using Adenawell_ValentinAP1_P1.DAL;
using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Adenawell_ValentinAP1_P1.Services;

public class EntradasHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(EntradasHuacales entradasHuacales)
    {
        if (entradasHuacales.IdEntrada == 0)
            return await Insertar(entradasHuacales);
        else
            return await Modificar(entradasHuacales);
    }

    private async Task<bool> Insertar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var detalle in entrada.EntradasDetalle)
        {
            var tipo = await contexto.TiposHuacales.FindAsync(detalle.TipoId);
            if (tipo != null)
                tipo.Existencia += detalle.Cantidad;
        }

        contexto.EntradasHuacales.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entradaAnterior = await contexto.EntradasHuacales
            .Include(e => e.EntradasDetalle)
            .FirstOrDefaultAsync(e => e.IdEntrada == entrada.IdEntrada);

        if (entradaAnterior == null) return false;

        foreach (var detalleAnt in entradaAnterior.EntradasDetalle)
        {
            var tipo = await contexto.TiposHuacales.FindAsync(detalleAnt.TipoId);
            if (tipo != null)
                tipo.Existencia -= detalleAnt.Cantidad;
        }

        contexto.Set<DetallesHuacales>().RemoveRange(entradaAnterior.EntradasDetalle);

        foreach (var detalleNuevo in entrada.EntradasDetalle)
        {
            var tipo = await contexto.TiposHuacales.FindAsync(detalleNuevo.TipoId);
            if (tipo != null)
                tipo.Existencia += detalleNuevo.Cantidad;
        }

        contexto.Entry(entradaAnterior).CurrentValues.SetValues(entrada);
        entradaAnterior.EntradasDetalle = entrada.EntradasDetalle;

        contexto.EntradasHuacales.Update(entradaAnterior);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(e => e.IdEntrada != entrada.IdEntrada
                      && e.NombreCliente.ToLower() == entrada.NombreCliente.ToLower());
    }



    public async Task<bool> ExisteHuacalRegistrado(string nombreCliente, int tipoId)
    {
        if (string.IsNullOrWhiteSpace(nombreCliente)) return false;

        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(e => e.NombreCliente.ToLower() == nombreCliente.ToLower()
                        && e.EntradasDetalle.Any(d => d.TipoId == tipoId));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await contexto.EntradasHuacales
            .Include(e => e.EntradasDetalle)
            .FirstOrDefaultAsync(e => e.IdEntrada == id);

        if (entrada != null)
        {
            foreach (var detalle in entrada.EntradasDetalle)
            {
                var tipo = await contexto.TiposHuacales.FindAsync(detalle.TipoId);
                if (tipo != null)
                    tipo.Existencia -= detalle.Cantidad;
            }

            contexto.EntradasHuacales.Remove(entrada);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
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
            .Include(e => e.EntradasDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == id);
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(e => e.EntradasDetalle)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}