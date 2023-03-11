using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorManagement.Models;

public class Errand
{

    public Guid Id { get; set; }

    public string ErrorMessage { get; set; } = null!;

    public int Status { get; set; } 

    public DateTime LogTime { get; set; }


    // Connected to... customer...

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }




    // Connected to... comment... 

    // public string Response { get; set; } = null!;

    // public string ResponseTime { get; set; } = null!;



}
