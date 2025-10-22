
using HWW16.Entities;
using HWW16.Enums;
using HWW16.Infra;
using HWW16.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Services
{
    public class SurveyService
    {
        private readonly SurveyRepository _surveyRepository;
        public SurveyService(SurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }
        public void AddSurvey()
        {
            //if (LocalStorage.LoginUser == null)
            //{
            //    throw new Exception("User is not logged in.");
            //}
            //if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            //{
            //    throw new Exception("Only Admin users can create surveys.");
            //}
            //if (string.IsNullOrWhiteSpace(titleSurvey))
            //{
            //    throw new Exception("Survey title cannot be empty.");
            //}
            //foreach (var questionText in questionTexts)
            //{
            //    if (string.IsNullOrWhiteSpace(questionText))
            //    {
            //        throw new Exception("Question text cannot be empty.");
            //    }

            //}
            //foreach (var optionText in optionTexts)
            //{
            //    if (string.IsNullOrWhiteSpace(optionText))
            //    {
            //        throw new Exception("option text cannot be empty.");
            //    }
            //}



        }
    }
}
