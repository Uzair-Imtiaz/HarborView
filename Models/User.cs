using System;
using System.Collections.Generic;
using System.Globalization;

namespace HarborView_Inn.Models;

public partial class User
{

    public string? Name { get; set; }
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNo { get; set; }

    public string Location { get; set; }

}
