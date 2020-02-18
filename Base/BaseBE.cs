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

        public async Task<T> GetByIdAsync(object id) => _repository.GetById(id);

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter,
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

        public void Delete(T obj)
        {
            _repository.Delete(obj);
            _uoW.Save();
        }

        public void Insert(T obj)
        {
            _repository.Insert(obj);
            _uoW.Save();
        }

        public void Update(T obj)
        {
            _repository.Update(obj);
            _uoW.Save();
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
