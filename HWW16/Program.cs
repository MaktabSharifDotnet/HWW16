using HWW16.DataAccess;
using HWW16.Dtos;
using HWW16.Entities;
using HWW16.Enums;
using HWW16.Infra;
using HWW16.Repositories;
using HWW16.Services;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
AppDbContext appDbContext = new AppDbContext();
UserRepository userRepository = new UserRepository(appDbContext);
UserService userService = new UserService(userRepository);
SurveyRepository surveyRepository = new SurveyRepository(appDbContext);
SurveyService surveyService = new SurveyService(surveyRepository);
VoteRepository voteRepository = new VoteRepository(appDbContext);
VoteService voteService = new VoteService(voteRepository, surveyRepository);
while (true)
{
    if (LocalStorage.LoginUser == null)
    {
        Console.WriteLine("please enter username");
        string username = Console.ReadLine()!;
        Console.WriteLine("please enter password");
        string password = Console.ReadLine()!;
        try
        {
            userService.Login(username, password);
            Console.WriteLine("login is done");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    else
    {
        User user = LocalStorage.LoginUser;
        switch (user.Role)
        {
            case RoleEnum.Admin:

                ShowMenuAdmin();
                try
                {
                    int option = int.Parse(Console.ReadLine()!);
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("--- Add New Survey ---");
                            Console.Write("Enter Survey Title: ");
                            string surveyTitle = Console.ReadLine()!;
                            var createSurveyDto = new CreateSurveyDto
                            {
                                Title = surveyTitle,
                                Questions = new List<QuestionDto>()
                            };
                            while (true)
                            {
                                Console.WriteLine("\n--- Add New Question ---");
                                Console.Write("Enter Question Text (or type 'done' to finish adding questions): ");
                                string questionText = Console.ReadLine()!;

                                if (questionText.ToLower() == "done")
                                {
                                    break;
                                }

                                var questionDto = new QuestionDto
                                {
                                    Text = questionText,
                                    Options = new List<string>()
                                };

                                Console.WriteLine("Enter exactly 4 options for this question:");
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.Write($"Option {i + 1}: ");
                                    string optionText = Console.ReadLine()!;
                                    questionDto.Options.Add(optionText);
                                }

                                createSurveyDto.Questions.Add(questionDto);
                            }
                            try
                            {
                                surveyService.AddSurvey(createSurveyDto);
                                Console.WriteLine("\nSurvey added successfully!");
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine($"Error adding survey: {ex.Message}");

                            }
                            Console.WriteLine("Press any key to return to the admin menu...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            ShowSurveys();
                            Console.WriteLine("please enter surveyId");
                            try
                            {
                                int surveyIdForShow = int.Parse(Console.ReadLine()!);
                                surveyService.DeleteSurvey(surveyIdForShow);
                                Console.WriteLine("delete is done");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("invalid surveyId");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        case 3:
                            ShowSurveys();
                            Console.WriteLine("please enter surveyID");
                            int surveyId = int.Parse(Console.ReadLine()!);
                            ResultSurveyDto resultSurveyDto=surveyService.GetResultSurvey(surveyId);
                            Console.WriteLine($"Total number of participants :{resultSurveyDto.TotalNumberOfParticipants}");
                            Console.WriteLine("Names of participants:");
                            foreach (var username in resultSurveyDto.ParticipantsUsernames)
                            {
                                Console.WriteLine($"username : {username}");
                                Console.WriteLine("--------------------------------");
                            }
                            foreach (var questionDto in resultSurveyDto.ResultQuestionsDto)
                            {
                                Console.WriteLine($"questionText:{questionDto.QuestionText}");
                                Console.WriteLine($"Total number of votes for this question :{questionDto.AllVotesForQuestionCount}");
                                Console.WriteLine("--------------------------------");
                                foreach (var optionDto in questionDto.ResultOptionsDto)
                                {
                                    Console.WriteLine($"NumberOfVotesForThisQuestionOption : {optionDto.NumberOfVotesForThisQuestionOption}");
                                    double percent = Math.Round(optionDto.Percent, 2);
                                    Console.WriteLine($"percent:{percent}%");
                                    Console.WriteLine("--------------------------------");
                                }

                            }
                            break;
                        case 4:
                            LocalStorage.Logout();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid option please enter number");
                }

                break;
            case RoleEnum.NormalUser:
                ShowMenuNormalUser();
                try
                {
                    int userOption = int.Parse(Console.ReadLine()!);
                    switch (userOption)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("--- Available Surveys ---");
                            ShowSurveys();
                            Console.WriteLine("\nPress any key to return to the menu...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            Console.WriteLine("--- Available Surveys ---");
                            ShowSurveys();
                            ParticipateInSurvey();
                            break;
                        case 0:
                            LocalStorage.Logout();
                            Console.Clear();
                            Console.WriteLine("You have been logged out.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }
                break;

        }
    }
}

void ShowMenuAdmin()
{
    Console.WriteLine("please enter option");
    Console.WriteLine("1.Add survey");
    Console.WriteLine("2.Delete survey");
    Console.WriteLine("3.View survey results");
    Console.WriteLine("4.logout");
}
void ShowSurveys()
{
    List<Survey> surveys = surveyService.GetSurveys();
    foreach (var survey in surveys)
    {
        Console.WriteLine($"id:{survey.Id} , title:{survey.Title}");
    }
}

void ShowMenuNormalUser()
{
    Console.WriteLine("\n--- User Menu ---");
    Console.WriteLine("Please enter an option:");
    Console.WriteLine("1. View available surveys");
    Console.WriteLine("2. Participate in a survey");
    Console.WriteLine("0. Logout");
}

void ParticipateInSurvey()
{
    Console.WriteLine("Type the desired survey ID.");
    try
    {
        int surveyId = int.Parse(Console.ReadLine()!);
        Survey? survey = surveyService.GetSurveyForVoting(surveyId);
        List<AnswerDto> myAnswers = new List<AnswerDto>();

        foreach (var question in survey.Questions)
        {
            Console.WriteLine($"\n--- Question: {question.Text} ---");

            for (int i = 0; i < question.Options.Count; i++)
            {
                string optionText = question.Options[i].Text;
                Console.WriteLine($"   {i + 1}. {optionText}");
            }

            int userChoice = 0;
            Option selectedOption = null;


            while (selectedOption == null)
            {

                Console.Write("Please enter your choice (1-4): ");
                try
                {
                    userChoice = int.Parse(Console.ReadLine()!);
                    if (userChoice >= 1 && userChoice <= question.Options.Count)
                    {
                        int selectedIndex = userChoice - 1;
                        selectedOption = question.Options[selectedIndex];
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                }
            }

            var answer = new AnswerDto
            {
                QuestionId = question.Id,
                SelectedOptionId = selectedOption.Id,
            };


            myAnswers.Add(answer);

        }


        var voteInfo = new CastVoteDto
        {
            SurveyId = surveyId,
            Answers = myAnswers
        };


        voteService.CastVote(voteInfo);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nYour vote was successfully registered!");
        Console.ResetColor();
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid survey ID format.");
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.Message);
        Console.ResetColor();
    }

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
    Console.Clear();
}


