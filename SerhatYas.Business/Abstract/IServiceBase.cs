using SerhatYas.Core.Entities;
using SerhatYas.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SerhatYas.Business.Abstract
{
    public interface IServiceBase<T> where T:IEntity
    {
        public IDataResult<T> Get(Expression<Func<T, bool>> filter);
        public IDataResult<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        public IDataResult<T> Add(T TEntity);
        public IResult Delete(T TEntity);
        public IResult Update(T TEntity);
    }
}
