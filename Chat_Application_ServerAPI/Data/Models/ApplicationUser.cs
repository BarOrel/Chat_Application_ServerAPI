using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Chat_Application_ServerAPI.Data.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string FullName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsAdmin { get; set; }
    [JsonIgnore]
    public List<ChatRoom> ChatRooms { get; set; }

    public string GetFullName()
    {
        return $"{FullName}";
    }
}

