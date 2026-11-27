using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tienda_Refaciones_computadora.Data;
using Tienda_Refaciones_computadora.Models;


namespace Tienda_Refaciones_computadora.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string? buscar)
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscar) || p.Marca.Contains(buscar));
            }

            ViewData["Buscar"] = buscar;
            return View(await productos.OrderBy(p => p.Nombre).ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Marca,Precio,Stock,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Producto agregado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCategorias(producto.CategoriaId);
            return View(producto);
        }

        private async Task CargarCategorias(int? seleccionada = null)
        {
            var categorias = await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nombre", seleccionada);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            await CargarCategorias(producto.CategoriaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Marca,Precio,Stock,CategoriaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Productos.AnyAsync(p => p.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                TempData["Mensaje"] = "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCategorias(producto.CategoriaId);
            return View(producto);
        }

    }


}