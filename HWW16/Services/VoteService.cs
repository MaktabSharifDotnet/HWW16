using HWW16.DataAccess;
using HWW16.Dtos; 
using HWW16.Entities;
using HWW16.Infra;
using HWW16.Repositories; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Services
{
    public class VoteService
    {
        private readonly VoteRepository _voteRepository;
        private readonly SurveyRepository _surveyRepository; 
        public VoteService(VoteRepository voteRepository, SurveyRepository surveyRepository)
        {
            _voteRepository = voteRepository;
            _surveyRepository = surveyRepository;
        }
        public void CastVote(CastVoteDto castVoteDto)
        {
           
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in. Please log in to vote.");
            }
            var currentUser = LocalStorage.LoginUser; 
            
            bool alreadyVoted = _voteRepository.HasUserVotedInSurvey(currentUser.Id, castVoteDto.SurveyId);
            if (alreadyVoted)
            {
               
                throw new Exception("You have already voted in this survey.");
            }
         
            Survey? survey = _surveyRepository.GetSurveyWithResultsById(castVoteDto.SurveyId); 
            if (survey == null)
            {
                throw new Exception($"Survey with ID {castVoteDto.SurveyId} not found.");
            }

            if (castVoteDto.Answers.Count != survey.Questions.Count)
            {
                throw new Exception("You must answer all questions in the survey.");
            }         
            var newVotes = new List<Vote>();        
            foreach (var question in survey.Questions)
            {              
                AnswerDto? userAnswer = castVoteDto.Answers.FirstOrDefault(a => a.QuestionId == question.Id);            
                if (userAnswer == null)
                {
                    
                    throw new Exception($"Answer for question '{question.Text}' is missing.");
                }
           
                int selectedOptionId = userAnswer.SelectedOptionId;              
                bool isValidOption = question.Options.Any(o => o.Id == selectedOptionId);
                if (!isValidOption)
                {
                    
                    throw new Exception($"Invalid option selected for question '{question.Text}'.");
                }
            
                var vote = new Vote
                {
                    UserId = currentUser.Id, 
                    SurveyId = castVoteDto.SurveyId, 
                    QuestionId = question.Id, 
                    SelectedOptionId = selectedOptionId 
                };
                newVotes.Add(vote);
            }
            _voteRepository.AddVotes(newVotes); 
        }

        public List<Vote> GetVotes() 
        {
          return  _voteRepository.GetVotes();
        }
    }
}