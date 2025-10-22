
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
        public void AddSurvey(string titleSurvey, List<string> questionTexts, List<string> optionTexts)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can create surveys.");
            }
            if (string.IsNullOrWhiteSpace(titleSurvey))
            {
                throw new Exception("Survey title cannot be empty.");
            }
            foreach (var questionText in questionTexts)
            {
                if (string.IsNullOrWhiteSpace(questionText))
                {
                    throw new Exception("Question text cannot be empty.");
                }

            }
            foreach (var optionText in optionTexts)
            {
                if (string.IsNullOrWhiteSpace(optionText))
                {
                    throw new Exception("option text cannot be empty.");
                }
            }

            Survey survey = new Survey()
            {
                Title = titleSurvey,
                CreatorUserId = LocalStorage.LoginUser.Id
            };

            List<Question> questions = new List<Question>();
            List<Option> options = new List<Option>();
            for (int i = 0; i < questionTexts.Count - 1; i++)
            {
                questions[i].Text = questionTexts[i];
                questions[i].SurveyId = survey.Id;
                for (int j = 0; j < 4; j++)
                {
                    options[j].Text = optionTexts[j];
                    options[j].QuestionId = questions[i].Id;   
                }
            }

            survey.Questions = questions;
            _surveyRepository.AddSurvey(survey);

        }
    }
}
