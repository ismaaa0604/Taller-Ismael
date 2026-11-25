using informacion;
using repositorios;
using interfaces;
using Microsoft.AspNetCore.Mvc;
public class PeliculasController: Controller
{
    private readonly IPeliculaRepository _peliculaRepository;
    public PeliculasController(IPeliculaRepository peliculaRepository)
    {
        _peliculaRepository = peliculaRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try{
            List<Peliculas> productos = _peliculaRepository.GetAll();
            return View(productos);
        }
        catch(Exception ex)
        {
            //_logger.LogError(ex.ToString());
            //TempData["Error"] = "No se pudieron cargar los productos.";
            return View(new List<Peliculas>());
        }
    }
    // GET /Pacientes/Crear → el formulario vacío
    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            //TempData["Error"] = "No se pudo abrir el formulario de creación.";
            return RedirectToAction("Index");
        }
    }
    // POST /Pacientes/Crear → los datos que cargó el usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PeliculaCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            //_logger.LogWarning("Intento de crear producto con datos inválidos.");
            return View(viewModel);
        }
        try
        {

            var pelicula = new Peliculas

            {
                Titulo = viewModel.Titulo,
                Anio = viewModel.Anio,
                categoria = viewModel.categoria
            };

            _peliculaRepository.Add(pelicula);
            //_logger.LogInformation($"Producto '{producto.descripcion}' creado correctamente.");
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el producto. Intente nuevamente");
            return View(viewModel);
        }
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            //if (VerificarAdmin() is IActionResult redirect) return redirect;

            var pelicula = _peliculaRepository.GetById(id);

            var viewModel = new PeliculaUpdateViewModel
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Anio = pelicula.Anio,
                categoria = pelicula.categoria
            };
            return View(viewModel);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            //TempData["Error"] = "No se encontró el producto solicitado o ocurrió un error al cargarlo.";
            return RedirectToAction("Index");
        }
    }
    // POST /Pacientes/Crear → los datos que cargó el usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id,PeliculaUpdateViewModel viewModel)
    {
        try
        {
            //if (VerificarAdmin() is IActionResult redirect) return redirect;

            if (id != viewModel.Id)
            {
                //_logger.LogWarning($"Inconsistencia de ID al editar producto. Ruta: {id}, Modelo: {viewModel.IdProducto}");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                //_logger.LogWarning($"Datos inválidos al intentar modificar el producto ID {id}.");
                return View(viewModel);
            }

            var pelicula = new Peliculas
            {
                Titulo = viewModel.Titulo,
                Anio = viewModel.Anio,
                categoria = viewModel.categoria
            };

            _peliculaRepository.Update(id, pelicula);
            //_logger.LogInformation($"Producto ID {id} modificado correctamente.");
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty, "No se pudo guardar los cambios.");
            return View(viewModel);
        }
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            //if (VerificarAdmin() is IActionResult redirect) return redirect;

            var pelicula = _peliculaRepository.GetById(id);
            return View(pelicula);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            //TempData["Error"] = "No se encontró el producto solicitado o ocurrió un error al cargarlo.";
            return RedirectToAction("Index");
        }
    }
    // POST /Pacientes/Crear → los datos que cargó el usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id,Peliculas pelicula)
    {
        try
        {
            //if (VerificarAdmin() is IActionResult redirect) return redirect;

            pelicula.Id = id;
            _peliculaRepository.Delete(id);
            //_logger.LogInformation($"Producto ID {id} eliminado correctamente.");
            return RedirectToAction("Index");

            //_logger.LogWarning($"No se pudo eliminar el producto con ID {id}.");
            //ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el producto. Intente nuevamente.");
            //return View(producto);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            //TempData["Error"] = "No se pudo eliminar el producto. Verifique que no esté asociado a un presupuesto.";
            return RedirectToAction("Index");
        }
    }
}