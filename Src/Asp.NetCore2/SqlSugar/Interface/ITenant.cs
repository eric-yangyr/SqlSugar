﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar
{
    public interface ITenant
    {
        void BeginTran();
        void CommitTran();
        void RollbackTran();
        void ChangeDatabase(dynamic configId);
        void ChangeDatabase(Func<ConnectionConfig, bool> changeExpression);
        SqlSugarTransaction UseTran();
        DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack = null);
        Task<DbResult<bool>> UseTranAsync(Func<Task> action, Action<Exception> errorCallBack = null);
        DbResult<T> UseTran<T>(Func<T> action, Action<Exception> errorCallBack = null);

        Task<DbResult<T>> UseTranAsync<T>(Func<Task<T>> action, Action<Exception> errorCallBack = null);
        void AddConnection(ConnectionConfig connection);
        SqlSugarProvider GetConnection(dynamic configId);
        void RemoveConnection(dynamic configId);
        SqlSugarScopeProvider GetConnectionScope(dynamic configId);
        SqlSugarProvider GetConnectionWithAttr<T>();
        SqlSugarScopeProvider GetConnectionScopeWithAttr<T>();
        ISugarQueryable<T> QueryableWithAttr<T>();
        IInsertable<T> InsertableWithAttr<T>(T insertObj) where T : class, new();
        IInsertable<T> InsertableWithAttr<T>(List<T> insertObjs) where T : class, new();
        IUpdateable<T> UpdateableWithAttr<T>(T updateObj) where T : class, new();
        IUpdateable<T> UpdateableWithAttr<T>() where T : class, new();
        IUpdateable<T> UpdateableWithAttr<T>(List<T> updateObjs) where T : class, new();
        IDeleteable<T> DeleteableWithAttr<T>(T deleteObjs) where T : class, new();
        IDeleteable<T> DeleteableWithAttr<T>(List<T> deleteObjs) where T : class, new();
        bool IsAnyConnection(dynamic configId);

        void Close();
        void Open();
    }
}
