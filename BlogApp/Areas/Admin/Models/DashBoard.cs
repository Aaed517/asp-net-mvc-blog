using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace BlogApp.Areas.Models
{
    public class Dashboard
    {
        public int MembersCount {get; set;}
        public int PostCount {get;set;}
        public int CommentCount {get;set;}
        public int RoleCount {get;set;}
        public  IEnumerable<object>? PostCountsByMonth {get;set;}
    }
}