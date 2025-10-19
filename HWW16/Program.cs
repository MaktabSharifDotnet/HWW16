using HWW16.DataAccess;
using HWW16.Entities;
using HWW16.Enums;
using HWW16.Infra;
using HWW16.Repositories;
using HWW16.Services;
AppDbContext appDbContext = new AppDbContext();
UserRepository userRepository = new UserRepository(appDbContext);
UserService userService = new UserService(userRepository);
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