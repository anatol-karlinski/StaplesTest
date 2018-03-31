using NLog;
using Staples.DAL.Interfaces;
using System.Reflection;

namespace Staples.DAL.Helpers
{
    public class LogHelper : ILogHelper
    {
        public void LogEntity<T>(T entity)
            where T : class
        {
            var entityName = typeof(T).Name;
            Logger Log = LogManager.GetLogger(entityName);
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, entityName, "New entity added");
            theEvent.Properties["EntityName"] = entityName;
            theEvent = AddPropertiesToEventInfo(theEvent, entity);
            Log.Log(theEvent);
        }

        private LogEventInfo AddPropertiesToEventInfo<T>(LogEventInfo eventInfo, T entity)
        {
            var entityProperties = entity
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in entityProperties)
            {
                var propertyValue = property.GetValue(entity) as string;
                if (propertyValue != null && propertyValue.ToUpper() != "ID")
                    eventInfo.Properties[property.Name] = property.GetValue(entity);
            }

            return eventInfo;
        }
    }
}