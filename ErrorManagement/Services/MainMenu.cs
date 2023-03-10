using ErrorManagement.Models;
using ErrorManagement.Models.Entities;

namespace ErrorManagement.Services;

internal class MainMenu
{

    public List<Errand> Errands = new List<Errand>();

    public void Menu()
    {

        //Populate list from database

        Console.Clear();
        WelcomeMenu();

    }



    private void WelcomeMenu()
    {
        Console.WriteLine("Welcome to Error handling system, console version!");
        Console.WriteLine("Are you a customer, press 1");
        Console.WriteLine("Are you an employee, press 2");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                CustomerChoice();
                break;

            case "2":
                EmployeeChoice();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. Please try again.");
                Console.ReadKey();
                break;
        }
    }


    private void CustomerChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome customer!");
        Console.WriteLine("Press 1 to let us know of a new error.");
        Console.WriteLine("Press 2 to view the state of a specific error.");
        Console.WriteLine("Press 3 to view all errors under management");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                LogError();
                break;

            case "2":
                ViewErrorDetails();
                break;

            case "3":
                ViewAllErrors();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. You will be directed back to main menu.");
                Console.ReadKey();
                break;
        }

    }


    private void EmployeeChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome employee!");
        Console.WriteLine("Press 1 to view all errands.");
        Console.WriteLine("Press 2 to view details on one errand.");
        Console.WriteLine("Press 3 to finalize or comment an errand.");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                ViewAllErrors();
                break;

            case "2":
                ViewErrorDetails();
                break;

            case "3":
                ChangeError();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. You will be directed back to main menu.");
                Console.ReadKey();
                break;
        }

    }


    private void LogError() //Save to database instead
    {
        Errand errand = new Errand();

        Console.WriteLine("Here lies create function...");
        Console.WriteLine("You have chosen to let us know of an error. Follow the following steps:");
        Console.WriteLine("Enter your name: ");
        errand.Name = Console.ReadLine() ?? "";
        Console.WriteLine("Enter your email: ");
        errand.Email = Console.ReadLine() ?? "";
        Console.WriteLine("Enter your phonenumber (optional): ");
        errand.PhoneNumber = Console.ReadLine() ?? "";

        //SHOULD SAVE TO COMMENT, AND NOT TO CUSTOMER... 
        Console.WriteLine("What is the error that has occured ");
        errand.ErrorMessage = Console.ReadLine() ?? "";


        errand.Status = 1;
        // Create a logged time of the errand
        // Set status of errand to "Ej påbörjad"
        Errands.Add(errand);
        Console.WriteLine("Your errand have been logged, we will be in touch.");
        Console.ReadKey();
    }

    private void ViewAllErrors() //Load from database instead
    {
        Console.WriteLine("Here lies view all function... (Not very detailed)");
        Console.ReadKey();

        if (Errands.Count == 0)
        {
            Console.WriteLine("There aint a single errand here!!!");
            Console.WriteLine("");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Here are all errands: ");
            Console.WriteLine("");
            int nr = 0;
            foreach (Errand errand in Errands)
            {
                nr++;
                Console.WriteLine($"Comment number {nr}: {errand.Email} {errand.PhoneNumber}");
                Console.WriteLine($"Name: {errand.Name}");
                Console.WriteLine($"Contact information: {errand.Email} {errand.PhoneNumber}");

                if (errand.Status == 1)
                {
                    Console.WriteLine("This error doesnt have a handler yet!");
                }
                else if (errand.Status == 2)
                {
                    Console.WriteLine("This error is ongoing!");
                }
                else
                {
                    Console.WriteLine("This error is finished.");
                }

                Console.WriteLine("");

            }

            Console.ReadKey();
        }

    }

    private void ViewErrorDetails()
    {
        Console.WriteLine("Here lies search function... (View one)");
        Console.ReadKey();
    }

    private void ChangeError()
    {
        Console.WriteLine("Here lies change status & comment function...");
        Console.ReadKey();

        Console.WriteLine("1. Do you want to change status of a specific errand?");
        Console.WriteLine("2. Do you want add a comment on a specific errand?");
        Console.ReadLine();




    }



  


}
