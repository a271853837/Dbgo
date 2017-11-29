using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dbgo.Core;
using Dbgo.Core.Domain.Logging;
using Dbgo.Core.Data;

namespace Dbgo.Services.Logging
{
    public partial class DefaultLogger : ILogger
    {

        private readonly IRepository<Log> _logRepository;


        public DefaultLogger(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public void ClearLog()
        {
            var log = _logRepository.Table.ToList();
            foreach (var logItem in log)
                _logRepository.Delete(logItem);
        }

        public void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Delete(log);
        }

        public void DeleteLogs(IList<Log> logs)
        {
            if (logs == null)
                throw new ArgumentNullException("logs");

            _logRepository.Delete(logs);
        }

        public IPagedList<Log> GetAllLogs(DateTime? fromUtc = default(DateTime?), DateTime? toUtc = default(DateTime?), string message = "", LogLevel? logLevel = default(LogLevel?), int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _logRepository.Table;
            if (fromUtc.HasValue)
                query = query.Where(l => fromUtc.Value <= l.CreatedOnUtc);
            if (toUtc.HasValue)
                query = query.Where(l => toUtc.Value >= l.CreatedOnUtc);
            if (logLevel.HasValue)
            {
                var logLevelId = (int)logLevel.Value;
                query = query.Where(l => logLevelId == l.LogLevelId);
            }
            if (!String.IsNullOrEmpty(message))
                query = query.Where(l => l.ShortMessage.Contains(message) || l.FullMessage.Contains(message));
            query = query.OrderByDescending(l => l.CreatedOnUtc);

            var log = new PagedList<Log>(query, pageIndex, pageSize);
            return log;
        }

        public Log GetLogById(int logId)
        {
            if (logId == 0)
                return null;

            return _logRepository.GetById(logId);
        }

        public IList<Log> GetLogByIds(int[] logIds)
        {
            if (logIds == null || logIds.Length == 0)
                return new List<Log>();

            var query = from l in _logRepository.Table
                        where logIds.Contains(l.Id)
                        select l;
            var logItems = query.ToList();
            //sort by passed identifiers
            var sortedLogItems = new List<Log>();
            foreach (int id in logIds)
            {
                var log = logItems.Find(x => x.Id == id);
                if (log != null)
                    sortedLogItems.Add(log);
            }
            return sortedLogItems;
        }

        public Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "")
        {
            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                //IpAddress = _webHelper.GetCurrentIpAddress(),
                //Customer = customer,
                //PageUrl = _webHelper.GetThisPageUrl(true),
                //ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };
            _logRepository.Insert(log);
            return log;
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }
    }
}
