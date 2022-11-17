using SerhatYas.Core.DataAccess.EntityFramework;
using SerhatYas.DataAccess.Abstract;
using SerhatYas.Entities.Concrete;

namespace SerhatYas.DataAccess.Concrete.EntityFramework
{
    public class EfUserProjectMapDal : EfEntityRepository<UserProjectMap, SerhatYasDB>, IUserProjectMapDal
    {
    }
}
