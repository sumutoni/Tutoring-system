using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class ReservedSession
{
    public int Id { get; set; }

    public string? TutorId { get; set; }

    public string? CourseCode { get; set; }

    public int? StudentId { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Course? CourseCodeNavigation { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Tutor? Tutor { get; set; }
}
