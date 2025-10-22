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
                    int option = int.Parse(Console.ReadLine()!);
                    switch (option) 
                    {
                        case 1:

                            CreateInfoSurveyDto createInfoSurveyDto = CreateInfoSurvey();
                            surveyService.AddSurvey(createInfoSurveyDto.Title , createInfoSurveyDto.OptionTexts , createInfoSurveyDto.OptionTexts);
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
CreateInfoSurveyDto CreateInfoSurvey() 
{
    Console.WriteLine("please enter Survey Title");
    string title = Console.ReadLine()!;
    Console.WriteLine("please enter number of question");
    int questionCount = int.Parse(Console.ReadLine()!);
    List<string> questionTexts = new List<string>();
    for (int i = 0; i < questionCount - 1; i++)
    {
        Console.WriteLine($"please enter question{i + 1}:");
        string questionText = Console.ReadLine()!;
        questionTexts.Add(questionText);
    }
    List<string> optionTesxts = new List<string>();
    for (int i = 0; i < 4; i++)
    {
        Console.WriteLine($"please enter option{i + 1}:");
        string optionText = Console.ReadLine()!;
        optionTesxts.Add(optionText);
    }
    CreateInfoSurveyDto createInfoSurveyDto = new CreateInfoSurveyDto() 
    {
        Title = title,
        QuestionTexts = questionTexts,
        OptionTexts = optionTesxts
    };
    return createInfoSurveyDto;
}