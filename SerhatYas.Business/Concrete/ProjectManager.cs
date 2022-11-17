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
    class ProjectManager : IProjectService
    {
        private IProjectDal _ProjectDal;
        public ProjectManager(IProjectDal ProjectDal)
        {
            _ProjectDal = ProjectDal;
        }
        public IDataResult<Project> Add(Project TEntity)
        {
            try
            {
                TEntity.Date = DateTime.Now;
                TEntity.IsDeleted = false;
                TEntity.IsPassive = false;
                _ProjectDal.Add(TEntity);
                return new SuccessDataResult<Project>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Project>();
            }
        }
        public IResult Delete(Project TEntity)
        {
            try
            {
                _ProjectDal.Delete(TEntity);
                return new SuccessDataResult<Project>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Project>();
            }
        }
        public IDataResult<Project> Get(Expression<Func<Project, bool>> filter)
        {
            try
            {

                var result = _ProjectDal.Get(filter);
                return new SuccessDataResult<Project>(result);
            }
            catch (Exception)
            {

                return new ErrorDataResult<Project>();
            }
        }
        public IDataResult<List<Project>> GetList(Expression<Func<Project, bool>> filter = null)
        {
            try
            {

                var result = filter == null ? _ProjectDal.GetList() : _ProjectDal.GetList(filter);

                return new SuccessDataResult<List<Project>>(result.ToList());
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Project>>(ex.Message);
            }

        }

        public IResult Update(Project TEntity)
        {
            try
            {
                TEntity.Date = DateTime.Now;
                _ProjectDal.Update(TEntity);
                return new SuccessDataResult<Project>(TEntity);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Project>();
            }
        }
    }
}
