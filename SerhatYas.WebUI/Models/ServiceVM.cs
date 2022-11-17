using SerhatYas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerhatYas.WebUI.Models
{
    public class ServiceVM
    {
     
        public User User { get; set; }
        public List<User> UserList { get; set; }
        public Project Project { get; set; }
        public List<Project> ProjectList { get; set; }
        public UserProjectMap UserProjectMap { get; set; }
        public List<UserProjectMap> UserProjectMapList { get; set; }

    }
}
