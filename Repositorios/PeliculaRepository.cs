using informacion;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using interfaces;
using datos;
namespace repositorios;
public class PeliculaRepository : IPeliculaRepository
{
    private readonly string _connectionString;
    public PeliculaRepository(ConexionDb conexionDb)
    {
        _connectionString = conexionDb.Cadena;
    }
    public List<Peliculas> GetAll()
    {
        var lista = new List<Peliculas>();
        using var conexion = new SqliteConnection(_connectionString);
        conexion.Open();
        string sql = "SELECT Id,Titulo,Anio,Categoria FROM Peliculas";
        using var comando = new SqliteCommand(sql,conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Peliculas{
                Id = lector.GetInt32(0),
                Titulo = lector.GetString(1),
                Anio = lector.GetInt32(2),
                categoria = Enum.Parse<Categoria>(lector.GetString(3))
            });
        }
        return lista;
    }
    public Peliculas GetById(int idPelicula)
    {
        Peliculas? pelicula = null;
        using var conexion = new SqliteConnection(_connectionString);
        conexion.Open();
        string sql = "SELECT Id,Titulo,Anio,Categoria FROM Peliculas WHERE Id = @id";
        using var comando = new SqliteCommand(sql,conexion);
        comando.Parameters.AddWithValue("@id", idPelicula);
        using var lector = comando.ExecuteReader();
        if(lector.Read())
        {
            pelicula = new Peliculas{
                Id = lector.GetInt32(0),
                Titulo = lector.GetString(1),
                Anio = lector.GetInt32(2),
                categoria = Enum.Parse<Categoria>(lector.GetString(3))
            };
        }
        if (pelicula == null){
            throw new Exception($"No se encontró la pelicula con ID {idPelicula}.");
        }
        return pelicula;
    }
    public int Add(Peliculas pelicula)
    {
        using var conexion = new SqliteConnection(_connectionString); 
        conexion.Open();
        string sql = "INSERT INTO Peliculas (Titulo,Anio,Categoria) VALUES (@titulo, @anio, @categoria)";
        using var comando = new SqliteCommand(sql,conexion);
        comando.Parameters.AddWithValue("@titulo", pelicula.Titulo);
        comando.Parameters.AddWithValue("@anio", pelicula.Anio);
        comando.Parameters.AddWithValue("@categoria", pelicula.categoria.ToString());
        int filas = comando.ExecuteNonQuery();
        if(filas == 0)
        {
            throw new Exception($"No se pudo crear la pelicula.");
        }
        comando.CommandText = "SELECT last_insert_rowid()";
        return Convert.ToInt32(comando.ExecuteScalar());
    }
    public void Update(int idPelicula,Peliculas pelicula)
    {
        using var conexion = new SqliteConnection(_connectionString); 
        conexion.Open();
        string sql = "UPDATE Peliculas SET Titulo = @titulo, Anio = @anio, Categoria = @categoria WHERE Id = @Id";
        using var comando = new SqliteCommand(sql,conexion);
        comando.Parameters.AddWithValue("@titulo", pelicula.Titulo);
        comando.Parameters.AddWithValue("@anio", pelicula.Anio);
        comando.Parameters.AddWithValue("@categoria", pelicula.categoria.ToString());
        comando.Parameters.AddWithValue("@Id", idPelicula);
        int filas = comando.ExecuteNonQuery();
        if(filas == 0)
        {
            throw new Exception($"No se encontró la pelicula con ID {idPelicula}.");
        }
    }
    public void Delete(int idPelicula)
    {
        using var conexion = new SqliteConnection(_connectionString); 
        conexion.Open();
        string sql = "DELETE FROM Peliculas WHERE Id = @Id";
        using var comando = new SqliteCommand(sql,conexion);
        comando.Parameters.AddWithValue("@Id", idPelicula);
        int filas = comando.ExecuteNonQuery();
        if(filas == 0)
        {
            throw new Exception($"No se encontró la pelicula con ID {idPelicula}.");
        }
    }
}