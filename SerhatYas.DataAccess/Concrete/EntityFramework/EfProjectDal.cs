using SerhatYas.Core.DataAccess.EntityFramework;
using SerhatYas.DataAccess.Abstract;
using SerhatYas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.DataAccess.Concrete.EntityFramework
{
    public class EfProjectDal : EfEntityRepository<Project, SerhatYasDB>, IProjectDal
    {
    }
}
