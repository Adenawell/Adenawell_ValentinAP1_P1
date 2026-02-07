using Adenawell_ValentinAP1_P1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Adenawell_ValentinAP1_P1.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }

    public DbSet<ViajesEspaciales> ViajesEspaciales { get; set; }
}
