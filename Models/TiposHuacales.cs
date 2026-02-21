using System.ComponentModel.DataAnnotations;

namespace Adenawell_ValentinAP1_P1.Models;

public class TiposHuacales
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Descripcion { get; set; } = string.Empty;

    public int Existencia { get; set; }
}