
using DAL.DBHelper;
using Models;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// Admin login
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public SysAdmin Login(SysAdmin objAdmin)
        {
            string sql =
                "SELECT LoginName FROM [dbo].[SysAdmins] WHERE LoginId = @LoginId and LoginPwd = @LoginPwd";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LoginId", objAdmin.LoginId), 
                new SqlParameter("@LoginPwd", objAdmin.LoginPwd)
            };

            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql, param);
                if (objReader.Read())
                {
                    objAdmin = new SysAdmin()
                    {
                        LoginName = objReader["LoginName"].ToString()
                    };
                }
                else
                {
                    objAdmin = null;
                }
                objReader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return objAdmin;
         }
    }
}
