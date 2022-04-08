using ClosedXML.Excel;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class GenresController : Controller
    {
        private readonly MaksgogoContext _context;

        public GenresController(MaksgogoContext maksgogo)
        {
            _context = maksgogo;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                Genre newgen;
                                var g = _context.Genres.Where(x => x.GenreName == worksheet.Name).ToList();
                                if (g.Count() > 0)
                                    newgen = g[0];
                                else
                                {
                                    newgen = new Genre()
                                    {
                                        GenreName = worksheet.Name
                                    };
                                    _context.Genres.Add(newgen);
                                    _context.SaveChanges();
                                }

                                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Studio newstu;
                                        var s = _context.Studios.Where(x => x.StudioName == row.Cell(2).Value.ToString()).ToList();
                                        if(s.Count() > 0)
                                            newstu = s[0];
                                        else
                                        {
                                            newstu = new Studio()
                                            {
                                                StudioName = row.Cell(2).Value.ToString()
                                            };
                                            _context.Studios.Add(newstu);
                                            _context.SaveChanges();
                                        }
                                        Film film = new Film();
                                        film.Name = row.Cell(1).Value.ToString();
                                        film.IdGenre = (from ge in _context.Genres
                                                        where ge.GenreName == newgen.GenreName
                                                        select ge.IdGenre).First();
                                        film.IdStudio = (from st in _context.Studios
                                                         where st.IdStudio == newstu.IdStudio
                                                         select st.IdStudio).First();
                                        film.Image = row.Cell(3).Value.ToString();
                                        film.Price = int.Parse(row.Cell(5).Value.ToString());
                                        film.ReleaseDate = DateTime.ParseExact(row.Cell(6).Value.ToString(), "MM/dd/yyyy", null);
                                        film.IsFav = Boolean.Parse(row.Cell(7).Value.ToString());
                                        film.Href = row.Cell(8).Value.ToString();
                                        film.AmountBougth = 0;
                                        film.Country = "США";
                                        _context.Films.Add(film);
                                        _context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Excel", "Films");
        }


        public IActionResult Export()
        {
            using(XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var genres = _context.Genres.ToList();
                foreach(var g in genres)
                {
                    var worksheet = workbook.Worksheets.Add(g.GenreName);

                    worksheet.Cell("A1").Value = "Назва";
                    worksheet.Cell("B1").Value = "Айді";
                    worksheet.Cell("C1").Value = "Ціна";
                    worksheet.Cell("D1").Value = "Скільки купили";
                    worksheet.Cell("E1").Value = "Улюблений?";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var films = (from f in _context.Films
                                where f.IdGenre == g.IdGenre
                                select f).ToList();
                    for(int i = 0; i < films.Count(); i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = films[i].Name;
                        worksheet.Cell(i + 2, 2).Value = films[i].IdFilm;
                        worksheet.Cell(i + 2, 3).Value = films[i].Price;
                        worksheet.Cell(i + 2, 4).Value = films[i].AmountBougth;
                        worksheet.Cell(i + 2, 5).Value = films[i].IsFav;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Maksgogo_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

    }
}
