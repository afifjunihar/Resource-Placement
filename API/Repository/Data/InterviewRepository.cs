﻿using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class InterviewRepository : GeneralRepository<ResourceContext, Interview, int>
    {
        private readonly ResourceContext iContext;
        public InterviewRepository(ResourceContext iContext) : base(iContext) 
        {
            this.iContext = iContext;        
        }

		public int AssignInterview(InterviewVM interviewVM)
		{
			var interviewData = new Interview
			{
				Interview_Date = interviewVM.Interview_Date,
				Interview_Result = InterviewResult.Waiting,
				Description = interviewVM.Description,
				ReadBy = interviewVM.ReadBy,
				User_Id = interviewVM.User_Id,
				Project_Id = interviewVM.Project_Id
			};
			iContext.Interviews.Add(interviewData);
			iContext.SaveChanges();

			User updateUser = iContext.Users.Find(interviewData.User_Id);
			updateUser.User_Status = CandidateStatus.OnProcess;
			iContext.Entry(updateUser).State = EntityState.Modified;
			iContext.SaveChanges();

			Project updateProject = iContext.Projects.Find(interviewData.Project_Id);
			bool isFull = updateProject.Current_Capacity == updateProject.Capacity;
			if (!isFull)
			{
				updateProject.Current_Capacity += 1;
				iContext.Entry(updateProject).State = EntityState.Modified;
				iContext.SaveChanges();
				CheckCapacity(updateProject.Project_Id);

				return 0;
			}
			return 1;
		}

		public int AcceptedInterview(KeyVM key)
		{
			var entity = iContext.Interviews.Find(key.KeyInt);
			if (entity != null)
			{
				entity.Interview_Result = InterviewResult.Accepted;
				iContext.Entry(entity).State = EntityState.Modified;
				iContext.SaveChanges();

				User updateUser = iContext.Users.Find(entity.User_Id);
				updateUser.User_Status = CandidateStatus.Hired;
				iContext.Entry(updateUser).State = EntityState.Modified;
				iContext.SaveChanges();

				return 0;
			}
			return 1;
		}

		public int RejectInterview(KeyVM key)
		{
			var entity = iContext.Interviews.Find(key.KeyInt);
			if (entity != null)
			{
				entity.Interview_Result = InterviewResult.Rejected;
				iContext.Entry(entity).State = EntityState.Modified;
				iContext.SaveChanges();

				User updateUser = iContext.Users.Find(entity.User_Id);
				updateUser.User_Status = CandidateStatus.Free;
				iContext.Entry(updateUser).State = EntityState.Modified;
				iContext.SaveChanges();

				Project updateProject = iContext.Projects.Find(entity.Project_Id);
				updateProject.Current_Capacity -= 1;
				iContext.Entry(updateProject).State = EntityState.Modified;
				iContext.SaveChanges();
				CheckCapacity(updateProject.Project_Id);

				return 0;
			}
			return 1;
		}

		private void CheckCapacity(int projectId)
		{
			Project closeProject = iContext.Projects.Find(projectId);
			if (closeProject.Capacity == closeProject.Current_Capacity)
			{
				closeProject.Status = Status.Closed;
				iContext.Entry(closeProject).State = EntityState.Modified;
				iContext.SaveChanges();
			}
			else
			{
				closeProject.Status = Status.Open;
				iContext.Entry(closeProject).State = EntityState.Modified;
				iContext.SaveChanges();
			}
		}
	}
}
