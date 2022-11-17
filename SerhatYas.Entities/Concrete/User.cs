using SerhatYas.Core.Entities;
using SerhatYas.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Entities.Concrete
{
    public class User : EntityBase,  IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string Job { get; set; }
        public DateTime LastLoginDate { get; set; }

    }
}
