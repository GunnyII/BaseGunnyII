using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Bussiness;
using System.Web.Security;
using System.Web.SessionState;


namespace Tank.Flash.auth
{
    /// <summary>
    /// Summary description for register
    /// </summary>
    public class register : IHttpHandler, IRequiresSessionState
    {
        protected string SiteTitle
        {
            get
            {
                return ((ConfigurationManager.AppSettings["SiteTitle"] == null) ? "DanDanTang" : ConfigurationManager.AppSettings["SiteTitle"]);
            }
        }
        private string code;
        private string email;
        private string message;
        private string password;
        private string repassword;
        private bool sex = true;
        private string username;

        protected bool CheckPara(HttpContext context, ref string message)
        {
           
            if (string.IsNullOrEmpty(username))
            {
                message = "Bạn chưa nhập Tài khoản。";
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                message = "Bạn chưa nhập Mật khuẩu。";
                return false;
            }
            if (string.IsNullOrEmpty(email))
            {
                message = "Bạn chưa nhập Email。";
                return false;
            }
            if (password!=repassword)
            {
                message = "Mật khuẩu không trùng nhau。";
                return false;
            }
            if (((context.Session["CheckCode"] == null) || ("" == code)) || (code.ToLower() != context.Session["CheckCode"].ToString().ToLower()))
            {
                message = "Sai mã bảo mật!";
                return false;
            }
            using (MemberShipBussiness db = new MemberShipBussiness())
            {
                if (db.ExistsUsername(username))
                {
                    message = "Tài khoản đã được sử dụng！";
                    return false;
                }
            }
            return true;
        }

        protected bool CreateUsername(HttpContext context, ref string message)
        {
           password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            using (MemberShipBussiness db = new MemberShipBussiness())
            {
                return db.CreateUsername(SiteTitle, username, password, email, "1", "MD5", sex);
            }
        }

        public void ProcessRequest(HttpContext context)
        {
             
            if (context.Request.Form.Count > 0)
            {
                username = context.Request["username"];
                password = context.Request["password"];
                repassword = context.Request["repassword"];
                email = context.Request["email"];
                sex = context.Request["sex"] == "1";
                code = context.Request["code"];
                message = "Register False!";
                if (CheckPara(context, ref message) && CreateUsername(context, ref message))
                {
                    message = "ok";
                }
                context.Response.Write(message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}