using API.Context;
using API.Library.Email;
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
		private readonly IEmailSender _emailSender;
		public InterviewRepository(ResourceContext iContext, IEmailSender emailSender) : base(iContext)
		{
			this.iContext = iContext;
			this._emailSender = emailSender;
		}

		public int AssignInterview(InterviewVM interviewVM)
		{
			var interviewData = new Interview
			{
				Interview_Date = interviewVM.Interview_Date,
				Interview_Result = interviewVM.Result,
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

				// Send Email
				var Payload = new Message
				(
					//updateUser.Email,
					"testwebpkl@gmail.com",
					"Jadwal Interview",
					new EmailVM
					{
						Sender_Alias = "HR Metrodata",
						Interview_Action = "Interview",
						Tanggal = interviewData.Interview_Date,
						Nama = updateUser.FirstName + " " + updateUser.LastName,
						Project_Name = updateProject.Project_Name,
						Jobs = updateProject.Required_Skill
					}
				);
				_emailSender.SendEmailAsync(Payload);
				// End of Send Email

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

				Project project = iContext.Projects.Find(entity.Project_Id);

				// Send Email
				var Payload = new Message
				(
					//updateUser.Email,
					"testwebpkl@gmail.com",
					"Hasil Interview",
					new EmailVM
					{
						Sender_Alias = "Client Project",
						Project_Name = project.Project_Name,
						Jobs = project.Required_Skill,
						Interview_Action = "Diterima",
						Nama = updateUser.FirstName + " " + updateUser.LastName
					}
				);
				_emailSender.SendEmailAsync(Payload);
				// End of Send Email

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
				entity.Description = key.KeyStr;
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

				// Send Email
				var Payload = new Message
				(
					//updateUser.Email,
					"testwebpkl@gmail.com",
					"Hasil Interview",
					new EmailVM
					{
						Sender_Alias = "Client Project",
						Interview_Action = "Ditolak",
						Project_Name = updateProject.Project_Name,
						Jobs = updateProject.Required_Skill,
						Nama = updateUser.FirstName + " " + updateUser.LastName,
						Note = key.KeyStr
					}
				);
				_emailSender.SendEmailAsync(Payload);
				// End of Send Email

				return 0;
			}
			return 1;
		}

		public IEnumerable<Object> History(string UserID)
		{
			var historyList = from intv in iContext.Interviews
									where intv.User_Id == UserID
									join proj in iContext.Projects on intv.Project_Id equals proj.Project_Id
									orderby intv.Interview_Id descending
									select new
									{
										id = intv.Interview_Id,
										name = proj.Project_Name,
										req_skill = proj.Required_Skill,
										jadwal = intv.Interview_Date,
										status = intv.Interview_Result,
										desc = intv.Description
									};

			return historyList.Take(3);
		}

		public Object Current(string UserID)
		{
			var currentProject = from intv in iContext.Interviews
										join proj in iContext.Projects on intv.Project_Id equals proj.Project_Id
										join usr in iContext.Users on intv.User_Id equals usr.User_Id
										orderby intv.Interview_Id descending										
										where intv.User_Id == UserID
										select new { 
											Name = proj.Project_Name,
											Accept_Date = intv.Interview_Date,
											Spec = proj.Required_Skill,
											Stats = intv.Interview_Result
										};
			return currentProject.FirstOrDefault();

		}

		// Private Method
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
