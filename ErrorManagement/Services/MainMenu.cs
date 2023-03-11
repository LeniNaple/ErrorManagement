using ErrorManagement.Models;
using ErrorManagement.Models.Entities;
using Microsoft.Identity.Client;

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
                LogErrorAsync();
                break;

            case "2":
                ViewErrorDetails();
                break;

            case "3":
                ViewAllErrorsAsync();
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
                ViewAllErrorsAsync();
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


    private async void LogErrorAsync() //Works, but need to add timestamp properly...
    {
        Errand errand = new Errand();

        Console.WriteLine("You have chosen to let us know of an error:");
        Console.WriteLine("Enter your name: ");
        errand.Name = Console.ReadLine() ?? "";

        Console.WriteLine("Enter your email: ");
        errand.Email = Console.ReadLine() ?? "";

        Console.WriteLine("Enter your phonenumber (optional): ");
        errand.PhoneNumber = Console.ReadLine() ?? "";

        Console.WriteLine("What is the error that has occured ");
        errand.ErrorMessage = Console.ReadLine() ?? "";

        // Set status of errand to "Ej påbörjad"
        errand.Status = 1;

        await CustomerService.SaveAsync(errand);
        Console.WriteLine("Your errand have been logged, we will be in touch.");
        Console.ReadKey();
    }

    private async void ViewAllErrorsAsync() //Load from database instead
    {
        var errands = await CustomerService.GetAllAsync();

        if (errands.Any())
        {
            Console.WriteLine("Here are all errands: ");
            Console.WriteLine("");
            int nr = 0;
            foreach (Errand errand in errands)
            {
                nr++;
                Console.WriteLine($"Comment number: {nr}. ");
                Console.WriteLine($"Name: {errand.Name}");
                Console.WriteLine($"Handling number: {errand.Id}");
                Console.WriteLine($"Contact information: {errand.Email} PN: {errand.PhoneNumber}");


                if (errand.Status == 1)
                {
                    Console.WriteLine("ERRAND STATUS: - Not yet assigned to handler.");
                }
                else if (errand.Status == 2)
                {
                    Console.WriteLine("ERRAND STATUS: - Ongoing!");
                }
                else
                {
                    Console.WriteLine("ERRAND STATUS: - Finished!");
                }

                Console.WriteLine("");

            }

            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("There aint a single errand here!!!");
            Console.WriteLine("");
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
