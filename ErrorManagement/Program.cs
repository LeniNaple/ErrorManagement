using ErrorManagement.Services;

MainMenu menu = new MainMenu();


while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome to Error handling system, console version!");
    Console.WriteLine("Are you a customer, press 1");
    Console.WriteLine("Are you an employee, press 2");
    var choice = Console.ReadLine();

    switch (choice)
    {

        case "1":
            await menu.CustomerChoice();
            break;

        case "2":
            await menu.EmployeeChoice();
            break;

        default:
            Console.Clear();
            Console.WriteLine("You have entered an invalid choice. Please try again.");
            Console.ReadKey();
            break;
    }
}
