using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hue2.Models;

namespace hue2.Controllers
{
    public interface IArestaiRepository //cia yra interfeisas kuris man sako kad klase turi tureti metoda GetAsmenys (butent toki)
    {
        IEnumerable<ArestaiModels> GetArestai();
        void ArestuotiValda(long valdoskodas);
    }

    public class ArestaiController : Controller
    {
        private IArestaiRepository _repository = new PlainSqlArestaiRepository();
        //
        // GET: /Arestai/
        private static Arestai _arestai = new Arestai();

        public ActionResult Index()
        {
            return View(_repository.GetArestai().ToList());
        }

    } 
}