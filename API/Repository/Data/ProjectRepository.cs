using API.Context;
using API.Models;
using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ProjectRepository : GeneralRepository<ResourceContext, Project, int>
    {
        private readonly ResourceContext pContext;
        public ProjectRepository(ResourceContext pContext) : base(pContext) 
        {
            this.pContext = pContext;
        }
        public dynamic showApplicant(KeyVM keyVM)
        {

            var listInterview = pContext.Interviews.ToList();
            var listUser = pContext.Users.ToList();
            var listSkillHandlers = pContext.SkillHandlers.ToList();
            var listSKill = pContext.Skills.ToList();

            var ScoreFE = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Front-End"
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              FrontEnd = b.Score
                          };

            var ScoreBE = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Back-End"
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              BackEnd = b.Score
                          };

            var ScoreFS = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Full-Stack"
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              FullStack = b.Score
                          };
            var ScoreFC = from a in listUser
                          join b in listSkillHandlers on a.User_Id equals b.User_Id
                          join c in listSKill on b.Skill_Id equals c.Skill_Id
                          where c.Skill_Name == "Fundamental C#"
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " " + a.LastName,
                              Fundamental = b.Score
                          };


            var getData = from a in ScoreBE
                          join b in ScoreFC on a.Fullname equals b.Fullname
                          join c in ScoreFE on a.Fullname equals c.Fullname
                          join d in ScoreFS on a.Fullname equals d.Fullname
                          select new
                          {
                              a.User_Id,
                              Fullname = a.Fullname,
                              FundamentalC = b.Fundamental,
                              BackEnd = a.BackEnd,
                              FrontEnd = c.FrontEnd,
                              FullStack = d.FullStack,
                              score = (b.Fundamental + c.FrontEnd + d.FullStack + a.BackEnd) / 4
                          };


            var getAplicant = from a in listUser
                          join d in listInterview on a.User_Id equals d.User_Id
                          join e in getData on a.User_Id equals e.User_Id
                          where d.Project_Id == keyVM.KeyInt && d.Interview_Result == InterviewResult.Accepted
                          orderby a.FirstName ascending
                          select new
                          {
                              a.User_Id,
                              Fullname = a.FirstName + " "+ a.LastName,
                              a.Email,
                              a.Phone,
                              e.score
                          };

            int hitungData1 = getAplicant.Count();
            int hitungData2 = getData.Count();
            if (hitungData1 == 0 || hitungData2 == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getAplicant;
            }
        }
        public dynamic projectInterview(KeyVM keyVM) 
        {
            var listInterview = pContext.Interviews.ToList();
            var getData = from a in listInterview
                          where a.Project_Id == keyVM.KeyInt
                          select new
                          {
                              a.Project_Id,
                              a.Interview_Id,
                              a.Interview_Date,
                              a.Interview_Result,
                              a.User_Id,
                              a.Description,
                              a.ReadBy
                          };
            int hitungData = getData.Count();
            if (hitungData == 0 )
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }         
        }
        public dynamic openProject() 
        {
            var listProject = pContext.Projects.ToList();
            var getData = from a in listProject
                          where a.Status == Status.Open
                          select new 
                          {
                              a.Project_Id,
                              a.Project_Name,
                              a.Capacity,
                              a.Current_Capacity,
                              a.Required_Skill,
                              a.Status,
                              a.Creator_Id
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public dynamic closedProject()
        {
            var listProject = pContext.Projects.ToList();
            var getData = from a in listProject
                          where a.Status == Status.Closed
                          select new
                          {
                              a.Project_Id,
                              a.Project_Name,
                              a.Capacity,
                              a.Current_Capacity,
                              a.Required_Skill,
                              a.Status,
                              a.Creator_Id
                          };

            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }
        }
        public Object Details(string Key)
        {
            int PK = Convert.ToInt32(Key);
            var detailProject = from proj in pContext.Projects
                                join usr in pContext.Users on proj.Creator_Id equals usr.User_Id
                                where proj.Project_Id == PK
                                select new
                                {
                                    proj,
                                    fullname = usr.FirstName + " " + usr.LastName,
                                };

            return detailProject.FirstOrDefault();
        }
        public IEnumerable<Object> Handler(string Key)
        {
            int PK = Convert.ToInt32(Key);

            string userID = (from iv in pContext.Interviews where iv.Project_Id == PK select iv.User_Id).FirstOrDefault();
            int grade = (from sk in pContext.SkillHandlers
                         where sk.User_Id == userID
                         group sk by sk.User_Id into skillGrup
                         select ((int)skillGrup.Average(o => o.Score))).FirstOrDefault();

            var projectHandler = from iv in pContext.Interviews
                                 join us in pContext.Users on iv.User_Id equals us.User_Id
                                 where iv.Project_Id == PK && iv.Interview_Result == InterviewResult.Waiting
                                 select new
                                 {
                                     iv.Interview_Id,
                                     name = us.FirstName + " " + us.LastName,
                                     grade = (grade > 82 ? "A" : "B"),
                                     iv.Interview_Date,
                                     iv.Interview_Result
                                 };

            return projectHandler.ToList();
        }
    }
}
