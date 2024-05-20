using System.Collections.Generic;

namespace src.MVVM.Model
{
    public interface IRepository<T>
    {
        public void CreateTable();
        void Add(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
    }
}
