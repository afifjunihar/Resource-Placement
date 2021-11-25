using API.Models.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class InterviewsController : BaseController<KeyVM, InterviewRepository, string>
    {
        private readonly InterviewRepository interview;
        public InterviewsController(InterviewRepository repository) : base(repository)
        {
            this.interview = repository;
        }

        public JsonResult Accept(KeyVM entity)
        {
            var result = interview.Accept(entity);
            return Json(result);
        }

        public JsonResult Reject(KeyVM entity)
        {
            var result = interview.Reject(entity);
            return Json(result);
        }

        public JsonResult Application(KeyVM entity)
        {
            var result = interview.Application(entity);
            return Json(result);
        }
    }
}
