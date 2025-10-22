using HWW16.DataAccess;
using HWW16.DTOs;
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
                    int option1 = int.Parse(Console.ReadLine()!);
                    switch (option1) 
                    {
                        case 1:
                            Console.WriteLine("Tite:");
                            string title = Console.ReadLine()!;
                            Console.WriteLine("please enter number Of question ");
                            int count = int.Parse(Console.ReadLine()!);
                            Survey survey = new Survey();
                            survey.Title = title;
                            survey.CreatorUser = LocalStorage.LoginUser;
                            for (int i = 0; i < count; i++)
                            {
                                Console.WriteLine($"pleae enter questionText {i+1}:"); 
                                string questionText = Console.ReadLine()!;
                                InfoQuestionForCreateDto infoQuestionForCreateDto = new InfoQuestionForCreateDto();
                                infoQuestionForCreateDto.Text = questionText;
                                Option option = new Option();
                                Question question = new Question();
                             
                                for (int j = 0; j < 4; j++)
                                {
                                    Console.WriteLine($"pleae enter optionText {j + 1}:");
                                    string optionText = Console.ReadLine()!;
                                    option.Text = optionText;
                                    option.QuestionId = question.Id;
                                }
                                question.Options.Add( option );
                                survey.Questions.Add( question );
                            }
                            surveyRepository.AddSurvey(survey);
                            break;
                        case 0:
                            LocalStorage.Logout();
                            break;
                    }
                }
                catch (FormatException) 
                {
                    Console.WriteLine("invalid option please enter number");
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }

                break;

            case RoleEnum.NormalUser:
                
                break;
        }
    }
}

void ShowMenuAdmin() 
{
    Console.WriteLine("please enter option");
    Console.WriteLine("1.Add survey");
    Console.WriteLine("0.LogOut");
}
