using Common.DataBaseAccess;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Repository
{
    public class WorkLogRepository
    {
        public int LogsCount(Expression<Func<WorkLog, bool>> filter = null)
        {
            Context context = new Context();
            IQueryable<WorkLog> query = context.WorkLogs;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        public List<WorkLog> GetAll(Expression<Func<WorkLog, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            Context context = new Context();
            IQueryable<WorkLog> query = context.WorkLogs;
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<WorkLog> GetAllFromList(Expression<Func<WorkLog, bool>> filter = null, List<WorkLog> list = null, int page = 1, int pageSize = int.MaxValue )
        {
            Context context = new Context();
            IQueryable<WorkLog> query = list.AsQueryable();
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
