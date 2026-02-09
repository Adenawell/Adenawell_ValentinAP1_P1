using System.ComponentModel.DataAnnotations;

namespace Adenawell_ValentinAP1_P1.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }


    [Required(ErrorMessage = "El campo es obligatorio")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public string NombreCliente { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El campo debe ser un numero positivo")]
    public double Precio { get; set; }



}
