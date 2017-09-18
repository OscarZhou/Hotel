using DAL;
using Models;
using System.Collections.Generic;

namespace BLL
{
    public class RecruitmentManager
    {
        private RecruitmentService objRecruitmentService = new RecruitmentService();

        public List<Recruitment> GetRecruitmentInfoList()
        {
            return objRecruitmentService.GetRecruitmentInfoList();
        }

        public int AddRecruitmentInfo(Recruitment objRecruitment)
        {
            return objRecruitmentService.AddRecruitmentInfo(objRecruitment);
        }

        public int ModifyRecruitmentInfo(Recruitment objRecruitment)
        {
            return objRecruitmentService.ModifyRecruitmentInfo(objRecruitment);
        }

        public int DeleteRecruitmentInfo(Recruitment objRecruitment)
        {
            return objRecruitmentService.DeleteRecruitmentInfo(objRecruitment);
        }

        public Recruitment GetRecruitmentById(string recruitmentId)
        {
            return objRecruitmentService.GetRecruitmentById(recruitmentId);
        }
    }
}
