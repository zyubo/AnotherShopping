using AnotherShopping.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherShopping.Controllers
{
    public class PayPalController : Controller
    {
        private AnotherShoppingContext db = new AnotherShoppingContext();
        private UsersContext udb = new UsersContext();

        //支付确认首页
        public ActionResult Index(string id)
        {
            //从字符串id中提取商品id和优惠码
            string[] idArr = id.Split('|');
            //得到优惠码
            string coupon = idArr.Length > 1 ? idArr[1] : "";
            //得到商品id
            id = idArr[0];
            CartGuiModel cgm = new CartGuiModel();

            //有优惠码的情况
            if (string.IsNullOrEmpty(coupon))
            {
                CartModels cm = db.CartModels.Find(id);
                if (cm != null)
                {
                    List<CartModels> cartModelList = db.CartModels.ToList();

                    //组成能给用户看的购物车数据
                    cgm.Count = cartModelList.FindAll(e => e.ProductId == cm.ProductId).Count;
                    cgm.Cart = cm;
                    cgm.Product = db.ProductModels.Find(cm.ProductId);
                    cgm.Profile = udb.UserProfiles.ToList().Find(u => u.UserName == cm.UserId);
                }
            }
            //没有优惠码的情况
            else
            {
                //组成能给用户看的购物车数据
                //加入用户数据
                UserProfile up = new UserProfile();
                up.UserId = new Random().Next(999);
                up.UserName = "Facebook Friend.";

                //加入数据库原始购物车数据
                CartModels cm = new CartModels();
                cm.CartId = Guid.NewGuid().ToString();
                cm.ProductId = id;
                cm.UserId = up.UserId.ToString();

                //加入商品数据
                ProductModels pm = db.ProductModels.Find(id);
                pm.ProductPrice = (float)(pm.ProductPrice * 0.9);
                pm.ProductName += " (with 10% discount)";

                cgm.Cart = cm;
                cgm.Product = pm;
                cgm.Profile = up;
                cgm.Count = 1;
            }
            return View(cgm);
        }

        //调用PayPal sdk指定是什么商品，总价是多少
        public ActionResult ValidateCommand(string product, string totalPrice)
        {
            //Sandbox是PayPal提供的沙盒，是用于测试，而不必用现实货币真正交易
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalModel(useSandbox);

            //商品名
            paypal.item_name = product;
            //总价
            paypal.amount = totalPrice;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            return View();
        }

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }

    }
}
