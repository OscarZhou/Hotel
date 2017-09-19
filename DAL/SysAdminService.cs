
using DAL.DBHelper;
using Models;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class SysAdminService
    {
        public SysAdmin Login(SysAdmin objAdmin)
        {
            string sql =
                "SELECT LoginId, LoginPwd, LoginName FROM [dbo].[SysAdmins] WHERE LoginId = @LoginId and LoginPwd = @LoginPwd";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LoginId", objAdmin.LoginId), 
                new SqlParameter("@LoginPwd", objAdmin.LoginPwd)
            };

            SysAdmin objRetAdmin = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            if (objReader.Read())
            {
                objRetAdmin = new SysAdmin()
                {
                    LoginId = Convert.ToInt32(objReader["LoginId"]),
                    LoginPwd = objReader["LoginPwd"].ToString(),
                    LoginName = objReader["LoginName"].ToString()
                };
            }
            objReader.Close();
            return objRetAdmin;
         }
    }
}
