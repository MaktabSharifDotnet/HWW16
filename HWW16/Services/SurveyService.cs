
using HWW16.Dtos;
using HWW16.Entities;
using HWW16.Enums;
using HWW16.Infra;
using HWW16.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public void AddSurvey(CreateSurveyDto createSurvey)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can create surveys.");
            }
            if (string.IsNullOrWhiteSpace(createSurvey.Title))
            {
                throw new Exception("Survey title cannot be empty.");
            }
            if (!createSurvey.Questions.Any())
            {
                throw new Exception("Survey must have at least one question.");
            }
            foreach (var questionDto in createSurvey.Questions)
            {
                if (string.IsNullOrWhiteSpace(questionDto.Text))
                {
                    throw new Exception("Question text cannot be empty.");
                }

                if (questionDto.Options.Count != 4)
                {
                    throw new Exception($"Question '{questionDto.Text}' must have exactly 4 options.");
                }
                if (questionDto.Options.Any(optionText => string.IsNullOrWhiteSpace(optionText)))
                {
                    throw new Exception($"Options text for question '{questionDto.Text}' cannot be empty or just whitespace.");
                }

            }
            var survey = new Survey
            {
                Title = createSurvey.Title,
                CreatorUserId = LocalStorage.LoginUser.Id,
            };
            foreach (var questionDto in createSurvey.Questions)
            {
                var question = new Question
                {
                    Text = questionDto.Text,
                    Survey = survey,
                };
                foreach (var optionText in questionDto.Options)
                {
                    var option = new Option
                    {
                        Text = optionText,
                        Question = question
                    };
                    question.Options.Add(option);
                }
                survey.Questions.Add(question);
            }
            _surveyRepository.Add(survey);
        }
        public void DeleteSurvey(int surveyId)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can create surveys.");
            }
            Survey? surveyDb = _surveyRepository.GetSurveyById(surveyId);
            if (surveyDb == null)
            {
                throw new Exception("There is no Survey with this ID.");
            }
           
            if (surveyDb.Votes.Any()) 
            {
              
                _surveyRepository.Delete(surveyDb);
                
            }
            else
            {
               
                throw new Exception("Cannot delete survey because no one has voted yet."); 
            }
            
        }
        public List<Survey> GetSurveys()
        {
            return _surveyRepository.GetSurveys();
        }
       

        public Survey? GetSurveyForVoting(int surveyId)
        {
            
            var survey = _surveyRepository.GetSurveyWithQuestionsWithOptions(surveyId);

     
            if (survey == null)
            {
               
                throw new Exception($"Survey with ID {surveyId} not found.");
            }

            
            return survey;
        }
    }

}

