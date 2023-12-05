using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class Tutor
{
    public string TrId { get; set; } = null!;

    public string? Fullnames { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<AvailableSession> AvailableSessions { get; set; } = new List<AvailableSession>();

    public virtual ICollection<ReservedSession> ReservedSessions { get; set; } = new List<ReservedSession>();

    public virtual ICollection<Tutoring> Tutorings { get; set; } = new List<Tutoring>();
}
