using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class Admin
{
    public string AdId { get; set; } = null!;

    public string? Fullnames { get; set; }

    public string? Password { get; set; }
}
