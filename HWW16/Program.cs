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
                            InfoSurveyForCreateDto  infoSurveyForCreateDto = new InfoSurveyForCreateDto();
                            Console.WriteLine("Title:");
                            string surveyTitle = Console.ReadLine()!;
                            infoSurveyForCreateDto.Title = surveyTitle;
                            Console.WriteLine("please enter number of question");
                            int questionCount = int.Parse(Console.ReadLine()!);
                            for (int i = 0; i < questionCount; i++)
                            {
                                InfoQuestionForCreateDto infoQuestionForCreateDto = new InfoQuestionForCreateDto();
                                Console.WriteLine($"please enter questionText{i+1}");
                                string questionText = Console.ReadLine()!;
                                infoQuestionForCreateDto.Text = questionText;
                                for (int j = 0; j < 4; j++)
                                {
                                    Console.WriteLine($"please enter optionText{j+1}");
                                    string optionText = Console.ReadLine()!;
                                    infoQuestionForCreateDto.OptionTexts.Add(optionText);
                                }
                                infoSurveyForCreateDto.Questions.Add(infoQuestionForCreateDto);
                            }
                            surveyService.AddSurvey(infoSurveyForCreateDto);
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
