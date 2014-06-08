﻿using System.Collections.Generic;
using log4net.Core;

namespace log4net.Raygun
{
	public class UserCustomDataBuilder : IUserCustomDataBuilder
    {
		internal const string NotSupplied = "Not supplied.";

        public Dictionary<string, string> Build(LoggingEvent loggingEvent)
        {
			var assemblyResolver = new AssemblyResolver();
			var applicationAssembly = assemblyResolver.GetApplicationAssembly();
			var applicationAssemblyFullName = applicationAssembly != null ? applicationAssembly.FullName : NotSupplied;

            var userCustomData = new Dictionary<string, string>
            {
				{ UserCustomDataKey.AssemblyFullName, applicationAssemblyFullName },
                { UserCustomDataKey.Domain, loggingEvent.Domain ?? NotSupplied},
                { UserCustomDataKey.Identity, loggingEvent.Identity ?? NotSupplied},
                { UserCustomDataKey.ClassName, loggingEvent.LocationInformation.ClassName ?? NotSupplied},
                { UserCustomDataKey.ThreadName, loggingEvent.ThreadName ?? NotSupplied},
                { UserCustomDataKey.RenderedMessage, loggingEvent.RenderedMessage ?? NotSupplied},
                { UserCustomDataKey.TimeStamp, loggingEvent.TimeStamp.ToString("O")},
                { UserCustomDataKey.UserName, loggingEvent.UserName ?? NotSupplied}
            };

            AddCustomProperties(loggingEvent, userCustomData);

            return userCustomData;
        }

	    private static void AddCustomProperties(LoggingEvent loggingEvent, Dictionary<string, string> userCustomData)
	    {
	        if (loggingEvent.Properties != null)
	        {
	            foreach (var propertyKey in loggingEvent.Properties.GetKeys())
	            {
	                var propertyValue = loggingEvent.Properties[propertyKey].ToString();
	                userCustomData.Add(string.Format("{0}.{1}", UserCustomDataKey.PropertiesPrefix, propertyKey), propertyValue);
	            }
	        }
	    }

		internal protected static class UserCustomDataKey
        {
            public const string AssemblyFullName = "Assembly FullName";
            public const string Domain = "Domain";
            public const string Identity = "Identity";
            public const string ClassName = "Class Name";
            public const string RenderedMessage = "Rendered Message";
            public const string ThreadName = "Thread Name";
            public const string TimeStamp = "Time Stamp";
            public const string UserName = "User Name";

            public const string PropertiesPrefix = "Properties";
        }
    }
}