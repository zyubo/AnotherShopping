using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnotherShopping.Models;

namespace AnotherShopping.Controllers
{
    public class CartController : Controller
    {
        private AnotherShoppingContext db = new AnotherShoppingContext();
        private UsersContext udb = new UsersContext();

        // 购物车首页
        // GET: /Cart/

        public ActionResult Index()
        {
            //用于显示给用户的购物车数据链表
            List<CartGuiModel> cartGuiModelList = new List<CartGuiModel>();
            //直接从数据库取到的购物车数据链表
            List<CartModels> cartModelList = db.CartModels.ToList();
            //将数据库原始数据逐个转化成能给用户看的数据，包括谁是购买者（Profile），商品详细信息（Product），购买数量（Count）
            foreach (CartModels cm in cartModelList)
            {
                if (User.Identity.Name == cm.UserId && cartGuiModelList.Find(e => e.Product.ProductId == cm.ProductId) == null)
                {
                    CartGuiModel cgm = new CartGuiModel();
                    //购买数量
                    cgm.Count = cartModelList.FindAll(e => e.ProductId == cm.ProductId).Count;
                    //购物车对象
                    cgm.Cart = cm;
                    //商品对象
                    cgm.Product = db.ProductModels.Find(cm.ProductId);
                    //用户对象
                    cgm.Profile = udb.UserProfiles.ToList().Find(u => u.UserName == cm.UserId);
                    //加入用于显示给用户的购物车数据链表
                    cartGuiModelList.Add(cgm);
                }
            }

            return View(cartGuiModelList);
        }

        // 购物车详细信息网页
        // GET: /Cart/Details/5

        public ActionResult Details(string id = null)
        {
            CartModels cartmodels = db.CartModels.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        // 新建购物车网页
        // GET: /Cart/Create

        public ActionResult Create()
        {
            return View();
        }

        // 新建购物车按钮
        // POST: /Cart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartModels cartmodels)
        {
            if (ModelState.IsValid)
            {
                cartmodels.CartId = Guid.NewGuid().ToString();
                db.CartModels.Add(cartmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartmodels);
        }

        // 修改购物车网页
        // GET: /Cart/Edit/5

        public ActionResult Edit(string id = null)
        {
            CartModels cartmodels = db.CartModels.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        // 修改购物车按钮
        // POST: /Cart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartModels cartmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartmodels);
        }

        // 删除购物车网页
        // GET: /Cart/Delete/5

        public ActionResult Delete(string id = null)
        {
            CartModels cartmodels = db.CartModels.Find(id);
            if (cartmodels == null)
            {
                return HttpNotFound();
            }
            return View(cartmodels);
        }

        // 删除购物车确认按钮
        // POST: /Cart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CartModels cartmodels = db.CartModels.Find(id);
            db.CartModels.Remove(cartmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}