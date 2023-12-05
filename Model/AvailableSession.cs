﻿using System;
using System.Collections.Generic;

namespace TutoringSys_core.Model;

public partial class AvailableSession
{
    public int Id { get; set; }

    public string? TutorId { get; set; }

    public string? CourseCode { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Course? CourseCodeNavigation { get; set; }

    public virtual Tutor? Tutor { get; set; }
}
