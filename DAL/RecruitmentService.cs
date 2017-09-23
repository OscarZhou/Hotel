
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class RecruitmentService
    {
        /// <summary>
        /// Query recruitment information
        /// </summary>
        /// <returns></returns>
        public List<Recruitment> GetRecruitmentInfoList()
        {
            string sql =
                "SELECT PostId, PostName, PostPlace, RequireCount, PostType, EduBackground, PostDesc, PostRequire, Experience, PublishTime, Manager, PhoneNumber, Email FROM [dbo].[Recruitment]";

            List<Recruitment> list = new List<Recruitment>();
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new Recruitment()
                {
                    PostId = Convert.ToInt32(objReader["PostId"]),
                    PostName = objReader["PostName"].ToString(),
                    PostPlace = objReader["PostPlace"].ToString(),
                    RequireCount = Convert.ToInt32(objReader["RequireCount"]),
                    PostType = objReader["PostType"].ToString(),
                    EduBackground = objReader["EduBackground"].ToString(),
                    PostDesc = objReader["PostDesc"].ToString(),
                    PostRequire = objReader["PostRequire"].ToString(),
                    Experience = objReader["Experience"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    Manager = objReader["Manager"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// Add recruitment information
        /// </summary>
        /// <param name="objRecruitment"></param>
        /// <returns></returns>
        public int AddRecruitmentInfo(Recruitment objRecruitment)
        {
            string sql =
                "INSERT INTO [dbo].[Recruitment] (PostName, PostPlace, RequireCount, PostType, EduBackground, PostDesc, PostRequire, Experience, PublishTime, Manager, PhoneNumber, Email) VALUES (@PostName, @PostPlace, @RequireCount, @PostType, @EduBackground, @PostDesc, @PostRequire, @Experience, @PublishTime, @Manager, @PhoneNumber, @Email)";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PostName", objRecruitment.PostName), 
                new SqlParameter("@PostPlace", objRecruitment.PostPlace), 
                new SqlParameter("@RequireCount", objRecruitment.RequireCount), 
                new SqlParameter("@PostType", objRecruitment.PostType), 
                new SqlParameter("@EduBackground", objRecruitment.EduBackground), 
                new SqlParameter("@PostDesc", objRecruitment.PostDesc), 
                new SqlParameter("@PostRequire", objRecruitment.PostRequire), 
                new SqlParameter("@Experience", objRecruitment.Experience), 
                new SqlParameter("@PublishTime", objRecruitment.PublishTime), 
                new SqlParameter("@Manager", objRecruitment.Manager), 
                new SqlParameter("@PhoneNumber", objRecruitment.PhoneNumber), 
                new SqlParameter("@Email", objRecruitment.Email)
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Modify recruitment information
        /// </summary>
        /// <param name="objRecruitment"></param>
        /// <returns></returns>
        public int ModifyRecruitmentInfo(Recruitment objRecruitment)
        {
            string sql =
                "UPDATE [dbo].[Recruitment] SET PostName = @PostName, PostPlace = @PostPlace, RequireCount = @RequireCount, PostType = @PostType, EduBackground = @EduBackground, PostDesc = @PostDesc, PostRequire = @PostRequire, Experience = @Experience, PublishTime = @PublishTime, Manager = @Manager, PhoneNumber = @PhoneNumber, Email = @Email WHERE PostId = @PostId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PostName", objRecruitment.PostName), 
                new SqlParameter("@PostPlace", objRecruitment.PostPlace), 
                new SqlParameter("@RequireCount", objRecruitment.RequireCount), 
                new SqlParameter("@PostType", objRecruitment.PostType), 
                new SqlParameter("@EduBackground", objRecruitment.EduBackground), 
                new SqlParameter("@PostDesc", objRecruitment.PostDesc), 
                new SqlParameter("@PostRequire", objRecruitment.PostRequire), 
                new SqlParameter("@Experience", objRecruitment.Experience), 
                new SqlParameter("@PublishTime", objRecruitment.PublishTime), 
                new SqlParameter("@Manager", objRecruitment.Manager), 
                new SqlParameter("@PhoneNumber", objRecruitment.PhoneNumber), 
                new SqlParameter("@Email", objRecruitment.Email),
                new SqlParameter("@PostId", objRecruitment.PostId),
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Delete recruitment information
        /// </summary>
        /// <param name="objRecruitment"></param>
        /// <returns></returns>
        public int DeleteRecruitmentInfo(Recruitment objRecruitment)
        {
            string sql = "DELETE FROM [dbo].[Recruitment] WHERE PostId = @PostId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PostId", objRecruitment.PostId), 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Display recruitment detail
        /// </summary>
        /// <param name="recruitmentId"></param>
        /// <returns></returns>
        public Recruitment GetRecruitmentById(string recruitmentId)
        {
            string sql =
                "SELECT PostId, PostName, PostPlace, RequireCount, PostType, EduBackground, PostDesc, PostRequire, Experience, PublishTime, Manager, PhoneNumber, Email FROM [dbo].[Recruitment] WHERE PostId = @PostId";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PostId", recruitmentId) 
            };

            Recruitment objRecruitment = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            if (objReader.Read())
            {
                objRecruitment = new Recruitment()
                {
                    PostName = objReader["PostName"].ToString(),
                    PostPlace = objReader["PostPlace"].ToString(),
                    RequireCount = Convert.ToInt32(objReader["RequireCount"]),
                    PostType = objReader["PostType"].ToString(),
                    EduBackground = objReader["EduBackground"].ToString(),
                    PostDesc = objReader["PostDesc"].ToString(),
                    PostRequire = objReader["PostRequire"].ToString(),
                    Experience = objReader["Experience"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    Manager = objReader["Manager"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString()
                };
            }

            objReader.Close();
            return objRecruitment;
        }
    }
}
