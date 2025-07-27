using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface IUsuarioRepositorio<T> where T : class
{
    IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
    T GetById(int id);
    void Add(T entity);
    void Delete(T entity);
    void Modified(T entity);
    void Save();
}
