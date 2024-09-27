using CelebritiesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CelebritiesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CelebritiesController : ControllerBase
    {
        private static readonly List<Celebrity> celebrities = new List<Celebrity>
        {
            new Celebrity { Id = 1, Name = "Tarkan", Profession = "Pop Müzik Sanatçısı" },
            new Celebrity { Id = 2, Name = "Sıla", Profession = "Pop Müzik Sanatçısı" },
            new Celebrity { Id = 3, Name = "Kenan İmirzalioğlu", Profession = "Oyuncu" },
            new Celebrity { Id = 4, Name = "Bergüzar Korel", Profession = "Oyuncu" }
        };

        // GET: api/celebrities
        [HttpGet]
        public ActionResult<IEnumerable<Celebrity>> Get()
        {

            return Ok(celebrities);
        }
        // GET: api/celebrities/5
        [HttpGet("{id}")]
        public ActionResult<Celebrity> Get(int id)
        {
            // id ye göre arama yaparken o id ye ait ilk kaydı çektik
            var celebrity = celebrities.FirstOrDefault(c => c.Id == id);

            // kayıt null geliyorsa not found döndük
            if (celebrity == null)
            {
                return NotFound();
            }
            //
            return Ok(celebrity);
        }

        // yeni bir kayıt oluşturmak için post kullanırız.
        [HttpPost]
        public ActionResult<Celebrity> Post([FromBody] Celebrity celebrity)
        {
            celebrity.Id = celebrities.Max(c => c.Id)+1;
            celebrities.Add(celebrity);

            return CreatedAtAction(nameof(Get), new {id= celebrity.Id},celebrities);

        }

        // olan kaydı tamamen değiştirmek için Put işlemi yapılır.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Celebrity updatedCelebrity)
        { 
             var selectedCelebrity = celebrities.FirstOrDefault(ce => ce.Id == id);

            // yazılmış id'li bir kayıt var mı diye bakıyoruz
            if(selectedCelebrity == null)
                return NotFound();

              selectedCelebrity.Name = updatedCelebrity.Name;
             selectedCelebrity.Profession = updatedCelebrity.Profession;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cid= celebrities.FirstOrDefault(c => c.Id==id);
            if(cid == null)
                return NotFound();
            // hard delete yapmak istemezsiniz models'e isdeleted eklemeyip onu true'ya çekebilirsiniz.
            celebrities.Remove(cid);
            return NoContent();
        }

    }
}
