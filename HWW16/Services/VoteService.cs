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
     
    }
}