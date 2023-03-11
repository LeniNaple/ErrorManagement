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


    public async Task CustomerChoice()
    {
        Console.Clear();
        Console.WriteLine("Welcome customer!");
        Console.WriteLine("1: To let us know of a new error.");
        Console.WriteLine("2: To view the state of a specific error.");
        Console.WriteLine("3: To view all errors under management");
        Console.WriteLine("4: To change an error.");
        var choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                Console.Clear();
                await LogErrorAsync();
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                await ViewErrorDetailsAsync();
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();
                await ViewAllErrorsAsync();
                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                await ChangeErrorAsync();
                Console.ReadKey();
                break;

            default:
                Console.Clear();
                Console.WriteLine("You have entered an invalid choice. You will be directed back to main menu.");
                Console.ReadKey();
                break;
        }

    }


    public async Task EmployeeChoice()
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
                Console.Clear();
                await ViewAllErrorsAsync();
                break;

            case "2":
                Console.Clear();
                await ViewErrorDetailsAsync();
                break;

            case "3":
                Console.Clear();
                await ChangeErrorStatusAsync();
                break;

            case "4":
                Console.Clear();
                await DeleteErrorAsync();
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
        errand.PhoneNumber = Console.ReadLine();

        Console.WriteLine("What is the error that has occured ");
        errand.ErrorMessage = Console.ReadLine() ?? "";

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

                if (errand.Status == 0)
                {
                    Console.WriteLine("ERRAND STATUS: - Changed by customer. Not assigned yet.");
                }
                else if (errand.Status == 1)
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

    private async Task ViewErrorDetailsAsync() //(NEEDS VISUAL UPDATE) It is functional, but veeeery slow in loading an errand..
    {
        Console.WriteLine("Write errand number to view specific errand: ");
        var errandNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNumber))
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

    private async Task ChangeErrorStatusAsync()
    {
        Console.WriteLine("Write the errand number you want to change status on: ");
        var errandNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(errandNumber))
        {
            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Errand: {_errand.Id} has been found.");
                    Console.WriteLine("1: If you want to change status to ongoing.");
                    Console.WriteLine("2: If you want to change status to completed.");
                    Console.WriteLine("3: If you want to change back status to unassigned.");
                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
                            _errand.Status = 2;
                            Console.WriteLine("Set to ONGOING");
                            Console.ReadKey();
                            break;
                        case "2":
                            _errand.Status = 3;
                            Console.WriteLine("Set to COMPLETED");
                            Console.ReadKey();
                            break;
                        case "3":
                            _errand.Status = 1;
                            Console.WriteLine("Set to UNASSIGNED");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Not a valid choice");
                            Console.ReadKey();
                            break;

                    }
                    await CustomerService.UpdateAsync(_errand);
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
        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }
    }


    private async Task ChangeErrorAsync()
    {
        Console.WriteLine("Write the errand number to change an errand: ");
        var errandNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(errandNumber))
        {

            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    Console.WriteLine($"Errand: {_errand.Id} has been found.");
                    Console.WriteLine("Fill in the information that you want to change.");
                    Console.WriteLine("ErrorMessage:");
                    _errand.ErrorMessage = Console.ReadLine() ?? "";
                    Console.WriteLine("Name:");
                    _errand.Name = Console.ReadLine() ?? "";
                    Console.WriteLine("Email:");
                    _errand.Email = Console.ReadLine() ?? "";
                    Console.WriteLine("PhoneNumber:");
                    _errand.PhoneNumber = Console.ReadLine();

                    _errand.Status = 0;

                    await CustomerService.UpdateAsync(_errand);
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


        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }


    }


    private async Task DeleteErrorAsync()
    {
        Console.WriteLine("Write the errand number of the errand you want to delete: ");
        var errandNumber = Console.ReadLine();

        if (!string.IsNullOrEmpty(errandNumber))
        {

            try
            {
                Guid errandNr = Guid.Parse(errandNumber);
                var _errand = await CustomerService.GetAsync(errandNr);
                if (_errand != null)
                {
                    
                    Console.WriteLine($"Do you want to delete the following comment?");
                    Console.WriteLine($"Customer number {_errand.Id}");
                    Console.WriteLine($"Customer {_errand.Name}");
                    Console.WriteLine($"Error {_errand.ErrorMessage}");
                    Console.WriteLine($"");
                    Console.WriteLine($"y for yes n for no");
                    string choice = Console.ReadLine() ?? "";

                    if (choice == "y")
                    {
                        await CustomerService.DeleteAsync(errandNr);
                        Console.WriteLine("Errand has been deleted!");
                        Console.ReadKey();

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Nothing has been deleted.");
                        Console.ReadKey();
                    } else
                    {
                        Console.WriteLine("Not a valid choice!");
                        Console.ReadKey();
                    }


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


        }
        else
        {
            Console.WriteLine("Not a valid entry.");
            Console.ReadKey();
        }

        Console.ReadKey();
    }





}
