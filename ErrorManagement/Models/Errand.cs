using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorManagement.Models;

public class Errand
{

    public string Id { get; set; } = null!;

    public string ErrorMessage { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string LogTime { get; set; } = null!;



    // Connected to... comment... 
    // Connected to... customer...



}
