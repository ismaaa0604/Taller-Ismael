namespace informacion;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
public class PeliculaUpdateViewModel
{
    public int Id{get;set;}
    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El título debe tener entre 2 y 100 caracteres.")]
    public string Titulo{get;set;}
    [Required(ErrorMessage = "El año es obligatorio.")]
    [Range(1888, int.MaxValue, ErrorMessage = "Ingrese un año válido.")]
    public int Anio{get;set;}
    [Required(ErrorMessage = "La categoria es obligatoria.")]
    public Categoria categoria{get;set;}
    public SelectList ListaCategoria{get;set;}
}