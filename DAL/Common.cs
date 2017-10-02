
using DAL.DBHelper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Common
    {
        public static DataSet GetList(int pageSize, int pageIndex, string tableName, string id, string innerjoin, string where, string fkid, out int totalCount)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@id", id),
                new SqlParameter("@innerjoin", innerjoin),
                new SqlParameter("@where", where),
                new SqlParameter("@fkid", fkid),
                new SqlParameter("@totalCount", SqlDbType.Int)
                
            };
            param[7].Direction = ParameterDirection.Output;
            DataSet ds = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "usp_pager", param);
            totalCount = Convert.ToInt32(param[7].Value);

            //SqlDataReader objReader = SQLHelper.GetReaderByProcedure("usp_pager", param);
            
            return ds;
        }
    }
}
