
using HWW16.DTOs;
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
        public void AddSurvey(InfoSurveyForCreateDto infoSurveyForCreateDto)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can create surveys.");
            }
            if (string.IsNullOrWhiteSpace(infoSurveyForCreateDto.Title))
            {
                throw new Exception("Survey title cannot be empty.");
            }
            foreach (var infoQuestionForCreateDto in infoSurveyForCreateDto.Questions)
            {
                if (string.IsNullOrWhiteSpace(infoQuestionForCreateDto.Text))
                {
                    throw new Exception("Question text cannot be empty.");
                }
                foreach (var optionText in infoQuestionForCreateDto.OptionTexts)
                {
                    if (string.IsNullOrWhiteSpace(optionText))
                    {
                        throw new Exception("option text cannot be empty.");
                    }
                }
            }

            Survey survey = new Survey()
            {
                Title = infoSurveyForCreateDto.Title,
                Questions = new List<Question>()
            };

            foreach (var infoQuestionForCreateDto in infoSurveyForCreateDto.Questions)
            {
                Question question = new Question();
                question.Text = infoQuestionForCreateDto.Text;
                foreach (var optionText in infoQuestionForCreateDto.OptionTexts)
                {
                    Option option = new Option();
                    option.Text = optionText;
                    question.Options.Add(option);
                }
                survey.Questions.Add(question);
            }
            _surveyRepository.AddSurvey(survey);

        }
    }
}
