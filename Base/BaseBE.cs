using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EVE.Data;

namespace EVE.Bussiness
{

    public class BaseBE<T> : IBaseBE<T>
            where T : class
    {
        public readonly IGenericRepository<T> _repository;

        public readonly IUnitOfWork<EVEEntities> _uoW;

        public BaseBE(IUnitOfWork<EVEEntities> uoW)
        {
            _uoW = uoW;
            _repository = _uoW.Repository<T>();
        }

        #region Get

        public async Task<IEnumerable<T>> GetAllAsync() => _repository.GetAll();
        public IEnumerable<T> GetAll() => _repository.GetAll();

        public async Task<T> GetByIdAsync(object id) => _repository.GetById(id);

        public T GetById(object id) => _repository.GetById(id);

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter,
                                                   string includeProperties = "")
        {
            return _repository.Get(filter, null, includeProperties);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter,
                                                 string includeProperties = "")
        {
            return _repository.Get(filter, null, includeProperties);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter,
                              string includeProperties = "")
        {
            return _repository.FindOne(filter, includeProperties);
        }

        #endregion Get

        #region Actions

        public bool Delete(T obj)
        {
            _repository.Delete(obj);
            return _uoW.Save();
        }

        public bool Insert(T obj)
        {
            _repository.Insert(obj);
           return _uoW.Save();
        }

        public virtual bool Update(T obj)
        {
            _repository.Update(obj);
           return _uoW.Save();
        }

        public void SubmitChange()
        {
            _uoW.Save();
        }

        #endregion Actions

        #region Validated

        #endregion Validated
    }
}
