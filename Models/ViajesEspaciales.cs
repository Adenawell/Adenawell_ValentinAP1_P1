using System.ComponentModel.DataAnnotations;

namespace Adenawell_ValentinAP1_P1.Models;

public class ViajesEspaciales
{
    [Key]
    public int VueloId { get; set; }


    [Required(ErrorMessage = "El campo es obligatorio")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo debe ser un numero positivo")]
    public double Costo { get; set; }



}
