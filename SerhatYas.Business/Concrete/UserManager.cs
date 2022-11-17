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
    class UserManager : IUserService
    {
        private IUserDal _UserDal;
        public UserManager(IUserDal UserDal)
        {
            _UserDal = UserDal;
        }
        public IDataResult<User> Add(User TEntity)
        {
            try
            {
                TEntity.LastLoginDate = DateTime.Now;
                TEntity.DisplayName = TEntity.Name + " " + TEntity.Surname;
                TEntity.Date = DateTime.Now;
                TEntity.IsDeleted = false;
                TEntity.IsPassive = false;
                _UserDal.Add(TEntity);
                return new SuccessDataResult<User>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>();
            }
        }
        public IResult Delete(User TEntity)
        {
            try
            {
                _UserDal.Delete(TEntity);
                return new SuccessDataResult<User>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>();
            }
        }
        public IDataResult<User> Get(Expression<Func<User, bool>> filter)
        {
            try
            {

                var result = _UserDal.Get(filter);
                return new SuccessDataResult<User>(result);
            }
            catch (Exception)
            {

                return new ErrorDataResult<User>();
            }
        }
        public IDataResult<List<User>> GetList(Expression<Func<User, bool>> filter = null)
        {
            try
            {

                var result = filter == null ? _UserDal.GetList() : _UserDal.GetList(filter);

                return new SuccessDataResult<List<User>>(result.ToList());
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<User>>(ex.Message);
            }

        }

        public IResult Update(User TEntity)
        {
            try
            {
                TEntity.DisplayName = TEntity.Name + " " + TEntity.Surname;
                TEntity.Date = DateTime.Now;
                _UserDal.Update(TEntity);
                return new SuccessDataResult<User>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>();
            }
        }
    }
}
