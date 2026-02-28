using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adenawell_ValentinAP1_P1.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
    public string NombreCliente { get; set; } = string.Empty;

    // Llave foranea relacion de uno a muchos con DetallesHuacales
    [ForeignKey("IdEntrada")]
    public ICollection<DetallesHuacales> EntradasDetalle { get; set; } = new List<DetallesHuacales>();
}