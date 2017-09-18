
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class SuggestionService
    {
        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="objSuggestion"></param>
        /// <returns></returns>
        public int SubmitSuggestion(Suggestion objSuggestion)
        {
            string sql =
                "INSERT INTO [dbo].[Suggestion] (CustomerName, ConsumeDesc, PhoneNumber, Email, SuggestionDesc, StatusId, SuggestionTime) VALUES (@CustomerName, @ConsumeDesc, @PhoneNumber, @Email, @SuggestionDesc, @StatusId, @SuggestionTime)";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CustomerName", objSuggestion.CustomerName), 
                new SqlParameter("@ConsumeDesc", objSuggestion.ConsumeDesc), 
                new SqlParameter("@PhoneNumber", objSuggestion.PhoneNumber), 
                new SqlParameter("@Email", objSuggestion.Email), 
                new SqlParameter("@SuggestionDesc", objSuggestion.SuggestionDesc), 
                new SqlParameter("@StatusId", objSuggestion.StatusId), 
                new SqlParameter("@SuggestionTime", objSuggestion.SuggestionTime)
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Query the latest suggestion information
        /// </summary>
        /// <param name="objSuggestion"></param>
        /// <returns></returns>
        public List<Suggestion> GetSuggestionNotAudited(Suggestion objSuggestion)
        {
            string sql =
                "SELECT SuggestionId, CustomerName, ConsumeDesc, PhoneNumber, Email, SuggestionDesc, StatusId, SuggestionTime FROM [dbo].[Suggestion] ORDER BY SuggestionTime DESC";

            List<Suggestion> list = new List<Suggestion>();
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new Suggestion()
                {
                    SuggestionId = Convert.ToInt32(objReader["SuggestionId"]),
                    CustomerName = objReader["CustomerName"].ToString(),
                    ConsumeDesc = objReader["ConsumeDesc"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString(),
                    SuggestionDesc = objReader["SuggestionDesc"].ToString(),
                    StatusId = Convert.ToInt32(objReader["StatusId"]),
                    SuggestionTime = Convert.ToDateTime(objReader["SuggestionTime"])
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// Modify status
        /// </summary>
        /// <param name="objSuggestion"></param>
        /// <returns></returns>
        public int ModifySuggestion(Suggestion objSuggestion)
        {
            string sql = "UPDATE [dbo].[Suggestion] SET StatusId = @StatusId WHERE SuggestionId = @SuggestionId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@StatusId", objSuggestion.StatusId), 
                new SqlParameter("@SuggestionId", objSuggestion.SuggestionId)
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }
    }
}
 