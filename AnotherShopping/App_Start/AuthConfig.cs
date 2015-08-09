using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using AnotherShopping.Models;

namespace AnotherShopping
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //从facebook开发者帐号申请的appId和appSecret, 用于激活facebook登录
            //上面和下面注释的代码可用于已Microsoft或Twitter或Google帐户登录
            OAuthWebSecurity.RegisterFacebookClient(
                appId: "868414896526529",
                appSecret: "a7a26df172d09bcba2bd5cccc1d5a0b3");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
