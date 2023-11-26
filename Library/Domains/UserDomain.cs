using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Library.Domains;

public class UserDomain : IdentityUser
{
    [PersonalData, Required, MaxLength(128)]
    public string Name { get; set; }

    [PersonalData, Required, MaxLength(128)]
    public string Surname { get; set; }

    [PersonalData, MaxLength(128)]
    public string? FatherName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime DateOfRegistration { get; set; } = DateTime.Now;
}

