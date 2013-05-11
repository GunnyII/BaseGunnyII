using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDataProvider.Data;
using SqlDataProvider.BaseClass;
using System.Data;
using System.Data.SqlClient;
using DAL;
using log4net;
using System.Reflection;
using log4net.Util;
using Bussiness.Managers;
using Bussiness.CenterService;
using System.Collections;

namespace Bussiness
{
    public class MemberShipBussiness : IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected Sql_DbObject db;
        public MemberShipBussiness()
        {
            db = new Sql_DbObject("AppConfig", "membershipDb");
        }
        public bool CheckUsername(string applicationname, string username, string password)
        {
            bool result = false;
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ApplicationName", applicationname), new SqlParameter("@UserName", username), new SqlParameter("@password", password), new SqlParameter("@UserId", SqlDbType.Int) };
            para[3].Direction = ParameterDirection.Output;
            result = db.RunProcedure("Mem_Users_Accede", para);
            int userid = 0;
            int.TryParse(para[3].Value.ToString(), out userid);
            return (userid > 0);
        }

        public bool CreateUsername(string applicationname, string username, string password, string email, string passwordformat, string passwordsalt, bool usersex)
        {
            bool result = false;
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ApplicationName", applicationname), new SqlParameter("@UserName", username), new SqlParameter("@password", password), new SqlParameter("@email", email), new SqlParameter("@PasswordFormat", passwordformat), new SqlParameter("@PasswordSalt", passwordsalt), new SqlParameter("@UserSex", usersex), new SqlParameter("@UserId", SqlDbType.Int) };
            para[7].Direction = ParameterDirection.Output;
            result = db.RunProcedure("Mem_Users_CreateUser", para);
            if (result)
            {
                result = ((int)para[7].Value) > 0;
            }
            return result;
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool ExistsUsername(string username)
        {
            bool result = false;
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserName", username), new SqlParameter("@UserCOUNT", SqlDbType.Int) };
            para[1].Direction = ParameterDirection.Output;
            result = db.RunProcedure("Mem_UserInfo_SearchName", para);
            return (((int)para[1].Value) > 0);
        }

    }
}
