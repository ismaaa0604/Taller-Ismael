namespace informacion;
public enum Categoria
{
    Accion,
    Drama,
    Comedia,
    SciFi
}
public class Peliculas
{
    public int Id{get;set;}
    public string? Titulo{get;set;}
    public int Anio{get;set;}
    public Categoria categoria{get;set;}
}