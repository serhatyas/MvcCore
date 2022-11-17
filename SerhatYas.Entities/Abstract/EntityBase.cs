using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Entities.Abstract
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPassive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
