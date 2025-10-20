using HWW16.DataAccess;
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
                            
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid option please enter number");
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
}