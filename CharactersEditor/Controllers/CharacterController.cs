using CharactersEditor.DAL;
using CharactersEditor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CharactersEditor.Controllers

{
    public class CharacterController : Controller
    {
        private CharacterContext db = new CharacterContext();
        private List<Character> charactersList = new List<Character>();

        // GET: Character
        public ActionResult Index()
        {
            return View(db.Characters.ToList());
        }

        // GET: Character/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Character/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Character/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pseudonym, Kind, Position, Universe")] Character character)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Characters.Add(character);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте ещё раз. Если проблема не ушла - обратитесь к системному администратору");
            }
            return View(character);
        }

        // GET: Character/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Character/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var characterToUpdate = db.Characters.Find(id);
            if (TryUpdateModel(characterToUpdate, "",
               new string[] { "Pseudonym", "Kind", "Position", "Universe" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте ещё раз. Если проблема не ушла - обратитесь к системному администратору");
                }
            }
            return View(characterToUpdate);
        }

        // GET: Character/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Невозможно удалить запись. Попробуйте ещё раз. Если проблема не ушла - обратитесь к системному администратору";
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Character character = db.Characters.Find(id);
                db.Characters.Remove(character);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Character>));
            var stream = new System.IO.MemoryStream();
            foreach (var item in db.Characters)
                charactersList.Add(item);
            xml.Serialize(stream, charactersList);
            stream.Flush();
            var data = stream.GetBuffer();
            stream.Close();
            return File(data, "application/xhtml+xml", "Characters [" + DateTime.Now + "].xml");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
