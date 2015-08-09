using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnotherShopping.Models;
using ASPSnippets.FaceBookAPI;

namespace AnotherShopping.Controllers
{
    public class ProductController : Controller
    {
        private AnotherShoppingContext db = new AnotherShoppingContext();
        private UsersContext udb = new UsersContext();

        // 商品首页，显示所用商品列表
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.ProductModels.ToList());
        }

        // 显示商品详细信息
        // GET: /Product/Details/5

        public ActionResult Details(string id = null)
        {
            ProductModels productmodels = db.ProductModels.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        // 创建商品网页
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.Coupon = new Random().Next(9999);
            return View();
        }

        // 创建一个商品确认按钮点击后进入此函数
        // 创建商品表格提交后变成参数productmodels传入此函数
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModels productmodels)
        {
            if (ModelState.IsValid)
            {
                productmodels.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                //将商品加入数据库
                db.ProductModels.Add(productmodels);

                //保存数据库
                db.SaveChanges();

                //转到商品首页
                return RedirectToAction("Index");
            }

            return View(productmodels);
        }

        //将商品加入购物车，id为唯一标识商品的id
        public ActionResult AddToCart(string id = null)
        {
            //在数据库中找到该商品
            ProductModels productmodels = db.ProductModels.Find(id);
            //创建一个购物车
            CartModels cartmodels = new CartModels();
            //为购物车指定是那个商品、那个用户买的、购物车本身的id
            cartmodels.CartId = Guid.NewGuid().ToString();
            cartmodels.ProductId = id;
            cartmodels.UserId = User.Identity.Name;
            //存入数据库
            db.CartModels.Add(cartmodels);
            db.SaveChanges();
            //弹窗加入购物车成功
            return Content("<script>alert('The item is added to your cart!');window.history.back();</script>");
        }

        // 修改商品网页
        // GET: /Product/Edit/5

        public ActionResult Edit(string id = null)
        {
            ProductModels productmodels = db.ProductModels.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        // 修改商品确认按钮
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModels productmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productmodels);
        }

        // 删除商品网页
        // GET: /Product/Delete/5

        public ActionResult Delete(string id = null)
        {
            ProductModels productmodels = db.ProductModels.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        // 删除商品按钮
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ProductModels productmodels = db.ProductModels.Find(id);
            db.ProductModels.Remove(productmodels);
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