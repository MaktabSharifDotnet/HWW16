
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
            if (!surveyDb.Votes.Any())
            {
                throw new Exception("Nobody voted.");
            }
            _surveyRepository.Delete(surveyDb);
        }
        public List<Survey> GetSurveys()
        {
            return _surveyRepository.GetSurveys();
        }
        public SurveyResultsDto GetSurveyResults(int surveyId)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("Only Admin users can view survey results.");
            }

            Survey? survey = _surveyRepository.GetSurveyWithResultsById(surveyId);

           
            if (survey == null)
            {
                throw new Exception($"Survey with ID {surveyId} not found.");
            }

            
            var surveyResultDto = new SurveyResultsDto
            {
                Title = survey.Title,
                QuestionResults = new List<QuestionResultDto>(),
                ParticipantNames = new List<string>()
            };
            var allVotesForSurvey = survey.Votes;
            if (allVotesForSurvey != null && allVotesForSurvey.Any())
            {

                surveyResultDto.ParticipantNames = allVotesForSurvey
                                                .GroupBy(v => v.UserId) 
                                                .Select(g => g.First().User.Username) 
                                                .ToList(); 
                surveyResultDto.TotalNumberOfParticipants = surveyResultDto.ParticipantNames.Count;
            }
            else
            {
                surveyResultDto.TotalNumberOfParticipants = 0; 
            }
            foreach (var question in survey.Questions) 
            {
                var questionResultDto = new QuestionResultDto
                {
                    Text = question.Text, 
                    OptionResults = new List<OptionResultDto>() 
                };
                
                var filteredVotes = allVotesForSurvey?.Where(v => v.QuestionId == question.Id).ToList(); 

                List<Vote> votesForQuestion; 

                if (filteredVotes == null) 
                {
                    votesForQuestion = new List<Vote>(); 
                }
                else 
                {
                    votesForQuestion = filteredVotes;
                }

                int totalVotesForQuestion = votesForQuestion.Count; 
                foreach (var option in question.Options) 
                {
                 
                    var votesForOption = votesForQuestion.Where(v => v.SelectedOptionId == option.Id).ToList();
                    int voteCountForOption = votesForOption.Count; 
                    double votePercentage;
                    if (totalVotesForQuestion == 0) 
                    {
                        votePercentage = 0; 
                    }
                    else
                    {
                        
                        votePercentage = ((double)voteCountForOption / totalVotesForQuestion) * 100;
                    }

                    questionResultDto.OptionResults.Add(new OptionResultDto
                    {
                        Text = option.Text,
                        VoteCount = voteCountForOption,
                        VotePercentage = Math.Round(votePercentage, 2)
                    });
                }
                
                surveyResultDto.QuestionResults.Add(questionResultDto);
            }
            return surveyResultDto;
        }
    }

}

