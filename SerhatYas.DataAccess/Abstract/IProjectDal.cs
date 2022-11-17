using SerhatYas.Core.DataAccess;
using SerhatYas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.DataAccess.Abstract
{
    public interface IProjectDal : IEntityRepository<Project>
    {
    }
}
