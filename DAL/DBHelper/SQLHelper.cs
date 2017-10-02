
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.DBHelper
{
    public class SQLHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// Execute insert, delete, update and select operation
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        /// <summary>
        /// Get a datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Prepared process
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        /// <param name="cmdType"></param>
        /// <param name="sqlParams"></param>
        private static void PreparedCommand(SqlConnection conn, SqlCommand cmd, CommandType cmdType,
            SqlParameter[] sqlParams)
        {
            cmd.CommandType = cmdType;
            if (sqlParams != null)
            {
                cmd.Parameters.AddRange(sqlParams);
            }
            conn.Open();
            
        }

        /// <summary>
        /// Get a datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql, CommandType cmdType, SqlParameter[] sqlParams)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    PreparedCommand(conn, cmd, cmdType, sqlParams);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// return a single query result
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }

        /// <summary>
        /// return a data collection
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #region Execute Sql statement that has paramters

        public static int Update(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        public static object GetSingleResult(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                object result = cmd.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static SqlDataReader GetReader(string sql, SqlParameter[] paramters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddRange(paramters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }

        }
        #endregion


        #region Call the stored procedure 

        public static int UpdateByProcedure(string procedureName, SqlParameter[] paramters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                cmd.Parameters.AddRange(paramters);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Execute multiple results' query
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetReaderByProcedure(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                cmd.Parameters.AddRange(parameters);
                SqlDataReader objReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return objReader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        #endregion

        #region Transaction


        public static bool ExecSQLByTran(List<string> sqlList)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            try
            {
                foreach (string itemSql in sqlList)
                {

                    cmd.CommandText = itemSql;
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit(); // submit the transaction
                return true;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback(); // roll back transaction
                throw ex;
            }
            finally
            {
                cmd.Transaction = null;
                conn.Close();
            }
        }

        #endregion


        /// <summary>
        /// 执行Sql 命令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>DataSet 对象</returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    return ds;
                }
            }
        }

        /// <summary>
        /// 设置一个等待执行的SqlCommand对象
        /// </summary>
        /// <param name="cmd">SqlCommand 对象，不允许空对象</param>
        /// <param name="conn">SqlConnection 对象，不允许空对象</param>
        /// <param name="commandText">Sql 语句</param>
        /// <param name="cmdParms">SqlParameters  对象,允许为空对象</param>
        private static void PrepareCommand(SqlCommand cmd, CommandType commandType, SqlConnection conn, string commandText, SqlParameter[] cmdParms)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //设置SqlCommand对象
            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

    }
}
