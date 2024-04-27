using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace efcoreApp.Controllers
{
 
    public class KursController : Controller
    {
         private readonly DataContext _context;
         
        public KursController(DataContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.Include(x => x.Ogretmen).ToListAsync());
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if(ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs(){Baslik=model.Baslik, OgretmenId=model.OgretmenId});
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId","AdSoyad");
            return View(model);
            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var kurs = await _context
                                .Kurslar
                                .Include(x => x.KursKayitlari)
                                .ThenInclude(x => x.Ogrenci)
                                .Select(k => new KursViewModel
                                {
                                    KursId = k.KursId,
                                    Baslik = k.Baslik,
                                    OgretmenId = k.OgretmenId,
                                    KursKayitlari = k.KursKayitlari
                                })
                                .FirstOrDefaultAsync(t=>t.KursId == id);
            if(kurs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId","AdSoyad");
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, KursViewModel model)
        {
            if(id != model.KursId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs(){ KursId = model.KursId, Baslik=model.Baslik, OgretmenId=model.OgretmenId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if(!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                  return RedirectToAction("Index");
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId","AdSoyad");
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            if(kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            
            var kurs = await _context.Kurslar.FindAsync(id);
            if(kurs == null)
            {
                return NotFound();
            }

            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
    }
}