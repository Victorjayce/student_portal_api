using System;
using System.ComponentModel.DataAnnotations;
namespace Students_portal.Models
{
    public enum Gender { Male = 1, Female = 2 }

    public enum StudentStatus { Active = 1, Suspended = 2, Graduated = 3, Withdrawn = 4 }

    public enum Semester { First = 1, Second = 2 }
}