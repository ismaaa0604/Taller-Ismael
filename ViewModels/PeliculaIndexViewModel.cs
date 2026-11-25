namespace informacion;
using System.ComponentModel.DataAnnotations;

public class ProductoViewModel
{
    public int Id {get;set;}
    public string Titulo {get;set;}
    public int Anio {get;set;}
    public Categoria categoria{get;set;}
}