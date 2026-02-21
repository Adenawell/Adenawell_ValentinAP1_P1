using System.ComponentModel.DataAnnotations;

namespace Adenawell_ValentinAP1_P1.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public string NombreCliente { get; set; } = string.Empty;


    [Required(ErrorMessage = "El campo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Tiene que ser un valor positivo")]
    public int? Cantidad { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El campo debe ser un número positivo")]
    public double? Precio { get; set; }
}