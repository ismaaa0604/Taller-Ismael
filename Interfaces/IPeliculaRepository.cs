using informacion;
namespace interfaces;
public interface IPeliculaRepository
{
    public List<Peliculas> GetAll();
    public Peliculas GetById(int idPelicula);
    public int Add(Peliculas peliculas);
    public void Update(int idPelicula, Peliculas pelicula);
    public void Delete(int idPelicula);
}