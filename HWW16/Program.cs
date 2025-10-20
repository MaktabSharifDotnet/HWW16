using HWW16.DataAccess;
using HWW16.Dtos;
using HWW16.Entities;
using HWW16.Enums;
using HWW16.Infra;
using HWW16.Repositories;
using HWW16.Services;
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
                                int surveyId = int.Parse(Console.ReadLine()!);
                                surveyService.DeleteSurvey(surveyId);
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
                            Console.Clear(); 
                            Console.WriteLine("--- View Survey Results ---");
                            ShowSurveys(); 
                            Console.Write("Enter the ID of the survey you want to view results for: "); 
                            try
                            {
                                int surveyId = int.Parse(Console.ReadLine()!); 

                               
                                SurveyResultsDto surveyResultsDto = surveyService.GetSurveyResults(surveyId);
                                Console.Clear(); 
                                Console.WriteLine($"--- Results for Survey: '{surveyResultsDto.Title}' ---");
                                Console.WriteLine($"Total Participants: {surveyResultsDto.TotalNumberOfParticipants}");

                                if (surveyResultsDto.TotalNumberOfParticipants > 0)
                                {
                                    Console.WriteLine("Participants:");
                                    foreach (var name in surveyResultsDto.ParticipantNames)
                                    {
                                        Console.WriteLine($"- {name}"); 
                                    }

                                    Console.WriteLine("\n--- Question Results ---");
                                    foreach (var questionResult in surveyResultsDto.QuestionResults)
                                    {
                                        Console.WriteLine($"\nQuestion: {questionResult.Text}"); 
                                        foreach (var optionResult in questionResult.OptionResults)
                                        {
                                            
                                            Console.WriteLine($"- {optionResult.Text}: {optionResult.VoteCount} votes ({optionResult.VotePercentage}%)");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No votes have been cast for this survey yet."); 
                                }
                            

                            }
                            catch (FormatException) 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Please enter a number for the Survey ID.");
                                Console.ResetColor();
                            }
                            catch (Exception ex) 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Error: {ex.Message}");
                                Console.ResetColor();
                            }

                            
                            Console.WriteLine("\nPress any key to return to the admin menu...");
                            Console.ReadKey();
                            Console.Clear();
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