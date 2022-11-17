using SerhatYas.Core.Entities;
using SerhatYas.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Entities.Concrete
{
    public class Project : EntityBase,  IEntity
    {
      
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public int Statu { get; set; }

    }
}
