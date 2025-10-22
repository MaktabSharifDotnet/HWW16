
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
            if (LocalStorage.LoginUser==null)
            {
                throw new Exception("");
            }
            if (LocalStorage.LoginUser.Role!=RoleEnum.Admin)
            {
                throw new Exception("");
            }
            foreach (var infoQuestionForCreateDto in infoSurveyForCreateDto.Questions)
            {
                if (string.IsNullOrEmpty(infoQuestionForCreateDto.Text))
                {
                    throw new Exception("");
                }
                foreach (var optionTexts in infoQuestionForCreateDto.OptionTexts)
                {
                    if (string.IsNullOrEmpty(optionTexts))
                    {
                        throw new Exception("");
                    }
                }
            }

            Survey survey = new Survey() 
            {
              CreatorUserId = LocalStorage.LoginUser.Id,
              Title = infoSurveyForCreateDto.Title,
            };

            foreach (var infoQuestionForCreateDto in infoSurveyForCreateDto.Questions)
            {
                Question question = new Question() 
                {
                   Text = infoQuestionForCreateDto.Text,
                };
                foreach (var optionText in infoQuestionForCreateDto.OptionTexts)
                {
                    Option option = new Option()
                    {
                       Text = optionText,
                    };
                    question.Options.Add(option);
                }
                survey.Questions.Add(question);
            }
            _surveyRepository.AddSurvey(survey);
        }
    }
}
