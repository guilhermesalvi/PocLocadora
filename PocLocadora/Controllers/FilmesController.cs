using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PocLocadora.Data;
using PocLocadora.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PocLocadora.Controllers
{
    [Route("Filmes")]
    public class FilmesController : Controller
    {
        private readonly PocLocadoraDbContext _context;

        public FilmesController(PocLocadoraDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Route("")]
        public IActionResult Index()
        {
            this.RenderGenerosViewData();
            this.RenderFilmesViewData();

            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Create(Filme model)
        {
            this._context.Filme.Add(model);
            await this._context.SaveChangesAsync();

            this.RenderGenerosViewData();
            this.RenderFilmesViewData();

            return View("Index", model);
        }

        private void RenderGenerosViewData()
        {
            var generos = new SelectList(this._context.Genero.ToList(), "Id", "Nome");
            generos.FirstOrDefault().Selected = true;
            ViewData["Generos"] = generos;
        }

        private void RenderFilmesViewData()
        {
            ViewBag.Filmes = this._context.Filme.ToList();
        }
    }
}
