using Staples.DAL.Models;
using System.Collections.Generic;

namespace Staples.GUI.ViewModels
{
    public class PersonDetailsViewModel
    {
        public PersonDetails PersonDetails { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}