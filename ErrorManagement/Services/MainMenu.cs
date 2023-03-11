using ErrorManagement.Contexts;
using ErrorManagement.Models;
using ErrorManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ErrorManagement.Services;

internal class MainMenu
{
    private static DataContext _context = new DataContext();
    public List<Errand> Errands = new List<Errand>();

    public async Task Menu()
    {

        //Populate list from database

        Console.Clear();
        await WelcomeMenu();

    }



    private async Task WelcomeMenu()
    {
        Console.WriteLine("Welcome to Error handling system, console version!");
        Console.WriteLine("Are you a customer, press 1");
        Console.WriteLine("Are you an employee, press 2");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                await CustomerChoice();
                break;

            case "2":
                await EmployeeChoice();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. Please try again.");
                Console.ReadKey();
                break;
        }
    }


    private async Task CustomerChoice()
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
                await LogErrorAsync();
                Console.ReadKey();
                break;

            case "2":
                await ViewErrorDetails();
                Console.ReadKey();
                break;

            case "3":
                await ViewAllErrorsAsync();
                Console.ReadKey();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. You will be directed back to main menu.");
                Console.ReadKey();
                break;
        }

    }


    private async Task EmployeeChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome employee!");
        Console.WriteLine("1: to view all errands.");
        Console.WriteLine("2: to view details on one errand.");
        Console.WriteLine("3: to change status on an errand.");
        Console.WriteLine("4: to delete an errand from database.");

        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                await ViewAllErrorsAsync();
                break;

            case "2":
                await ViewErrorDetails();
                break;

            case "3":
                ChangeError();
                break;

            case "4":
                await DeleteError();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. You will be directed back to main menu.");
                Console.ReadKey();
                break;
        }

    }


    private async Task LogErrorAsync() //Seems functional, saves new errand to database... 
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
        errand.LogTime = DateTime.Now;

        await CustomerService.SaveAsync(errand);
        Console.WriteLine("Your errand have been logged, we will be in touch.");
        Console.ReadKey();
    }

    private async Task ViewAllErrorsAsync() //Seems functional, loads all errands from database, and displays them...
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

    private async Task ViewErrorDetails() //(NEEDS VISUAL UPDATE) It is functional, but veeeery slow in loading an errand..
    {
        Console.WriteLine("Write errand number to view specific errand: ");
        var errandNumber = Console.ReadLine();

        if (!string .IsNullOrEmpty(errandNumber))
        {

            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Customer number {_errand.Id}");
                    Console.WriteLine($"Customer {_errand.Name}");
                    Console.WriteLine($"Error {_errand.ErrorMessage}");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("No customer with this errand number");
                    Console.ReadKey();
                }
            } 
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();
            }


        } else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }
 

        
    }

    private void ChangeError()
    {
        Console.WriteLine("Here lies change status & comment function...");
        Console.ReadKey();

        Console.WriteLine("1. Do you want to change status of a specific errand?");
        Console.WriteLine("2. Do you want add a comment on a specific errand?");
        Console.ReadLine();

    }

    private async Task DeleteError()
    {
        Console.WriteLine("Write the errand number of the errand you want to delete: ");
        var errandNr = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNr))
        {

            try
            {
                Guid errandNumber = Guid.Parse(errandNr);
                await CustomerService.DeleteAsync(errandNumber);
                Console.WriteLine("Errand has been deleted!");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Not a valid entry.");
                Console.ReadKey();
            }

        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }
        Console.ReadKey();
    }





}
