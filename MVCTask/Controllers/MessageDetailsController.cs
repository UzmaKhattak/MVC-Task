using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTask.Models;
using System.IO;

namespace MVCTask.Controllers
{
    public class MessageDetailsController : Controller
    {
        private TaskContext db = new TaskContext();

        // GET: MessageDetails
        public ActionResult Index()
        {
            return View(db.msgDetails.ToList());
        }

        // GET: MessageDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageDetails messageDetails = db.msgDetails.Find(id);
            if (messageDetails == null)
            {
                return HttpNotFound();
            }
            return View(messageDetails);
        }

        // GET: MessageDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessageDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgID,FullName,File,Message")] HttpPostedFileBase upload, MessageDetails messageDetails)
        {
            if (ModelState.IsValid)
            {
                string date = DateTime.Now.ToShortDateString();
                List<MessageDetails> list = db.msgDetails.Where<MessageDetails>(i => i.FullName == messageDetails.FullName && i.Message == messageDetails.Message
               && i.Date == date).ToList();
                if (list.Count < 2)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {

                        if (upload.FileName.EndsWith(".csv") || upload.FileName.EndsWith(".xls") || upload.FileName.EndsWith(".xlsx"))
                        {
                            var fileName = Path.GetFileName(upload.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                            upload.SaveAs(path);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                    messageDetails.Time = DateTime.Now.ToShortTimeString();
                    messageDetails.Date = DateTime.Now.ToShortDateString();
                    messageDetails.File = upload.FileName;
                    db.msgDetails.Add(messageDetails);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "Record already inserted twice.";
                }

            }

            return View(messageDetails);
        }

        // GET: MessageDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageDetails messageDetails = db.msgDetails.Find(id);
            if (messageDetails == null)
            {
                return HttpNotFound();
            }
            return View(messageDetails);
        }

        // POST: MessageDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgID,FullName,File,Message")] HttpPostedFileBase upload, MessageDetails messageDetails)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv") || upload.FileName.EndsWith(".xls") || upload.FileName.EndsWith(".xlsx"))
                    {
                        var fileName = Path.GetFileName(upload.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                        upload.SaveAs(path);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                messageDetails.Time = DateTime.Now.ToShortTimeString();
                messageDetails.Date = DateTime.Now.ToShortDateString();
                messageDetails.File = upload.FileName;
                db.Entry(messageDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messageDetails);
        }

        // GET: MessageDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageDetails messageDetails = db.msgDetails.Find(id);
            if (messageDetails == null)
            {
                return HttpNotFound();
            }
            return View(messageDetails);
        }

        // POST: MessageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageDetails messageDetails = db.msgDetails.Find(id);
            db.msgDetails.Remove(messageDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
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
