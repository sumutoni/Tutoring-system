using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class Student
{
    public int StId { get; set; }

    public string? Fullnames { get; set; }

    public virtual ICollection<ReservedSession> ReservedSessions { get; set; } = new List<ReservedSession>();
}
