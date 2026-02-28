using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adenawell_ValentinAP1_P1.Models;

public class DetallesHuacales
{
    [Key]
    public int DetalleId { get; set; }

    [Required]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un tipo")]
    public int TipoId { get; set; }

    [ForeignKey("TipoId")]
    public TiposHuacales? TipoHuacal { get; set; }

    [Required(ErrorMessage = "Requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "Mayor a 0")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "Requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Mayor a 0")]
    public double Precio { get; set; }
}