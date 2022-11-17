using SerhatYas.Core.Entities;
using SerhatYas.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Entities.Concrete
{
   public class UserProjectMap:EntityBase,IEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
