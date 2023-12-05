using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class Course
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public int? Credit { get; set; }

    public virtual ICollection<AvailableSession> AvailableSessions { get; set; } = new List<AvailableSession>();

    public virtual ICollection<ReservedSession> ReservedSessions { get; set; } = new List<ReservedSession>();

    public virtual ICollection<Tutoring> Tutorings { get; set; } = new List<Tutoring>();
}
