using SerhatYas.Business.Abstract;
using SerhatYas.Core.Utilities;
using SerhatYas.DataAccess.Abstract;
using SerhatYas.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SerhatYas.Business.Concrete
{
    class UserProjectMapManager : IUserProjectMapService
    {
        private IUserProjectMapDal _UserProjectMapDal;
        public UserProjectMapManager(IUserProjectMapDal UserProjectMapDal)
        {
            _UserProjectMapDal = UserProjectMapDal;
        }
        public IDataResult<UserProjectMap> Add(UserProjectMap TEntity)
        {
            try
            {
                TEntity.Date = DateTime.Now;
                TEntity.IsDeleted = false;
                TEntity.IsPassive = false;
                _UserProjectMapDal.Add(TEntity);
                return new SuccessDataResult<UserProjectMap>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<UserProjectMap>();
            }
        }
        public IResult Delete(UserProjectMap TEntity)
        {
            try
            {
                _UserProjectMapDal.Delete(TEntity);
                return new SuccessDataResult<UserProjectMap>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<UserProjectMap>();
            }
        }
        public IDataResult<UserProjectMap> Get(Expression<Func<UserProjectMap, bool>> filter)
        {
            try
            {

                var result = _UserProjectMapDal.Get(filter);
                return new SuccessDataResult<UserProjectMap>(result);
            }
            catch (Exception)
            {

                return new ErrorDataResult<UserProjectMap>();
            }
        }
        public IDataResult<List<UserProjectMap>> GetList(Expression<Func<UserProjectMap, bool>> filter = null)
        {
            try
            {

                var result = filter == null ? _UserProjectMapDal.GetList() : _UserProjectMapDal.GetList(filter);

                return new SuccessDataResult<List<UserProjectMap>>(result.ToList());
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<UserProjectMap>>(ex.Message);
            }

        }

        public IResult Update(UserProjectMap TEntity)
        {
            try
            {
                TEntity.Date = DateTime.Now;
                _UserProjectMapDal.Update(TEntity);
                return new SuccessDataResult<UserProjectMap>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<UserProjectMap>();
            }
        }
    }
}
