using System;
using Service.Messaging.Filter;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;

namespace Service.Logic
{
    public class MessageFilter : IMessageFilter
    {
        private readonly IApplicationEnvironment _app;
        public MessageFilter(IApplicationEnvironment app)
        {
            _app = app;
        }
        public bool ShouldTryProcessingMessage(dynamic message)
        {
            var servicename = _app.ApplicationName;
            
            if (!CheckMessageIfForThisService(message, servicename)) return false;
            if (!CheckForVersionCompatiblity(message, servicename)) return false;
            if (!CheckForSatisfiableNeed(message)) return false;
            if (!CheckForExistingSolutions(message)) return false;
            return true;
        }

        private bool CheckMessageIfForThisService(dynamic message, string servicename)
        {
            if (servicename != message.ModifiedBy) return true;
            Log.Information("Event=\"Message Dropped\" Message=\"This message was droped as it was last modified by this service\" ");
            return false;
        }

        private bool CheckForVersionCompatiblity(dynamic message, string servicename)
        {
            var currentMajorVersion = _environment.GetServiceVersion();
            try
            {
                foreach (var versionRequirement in message.CompatibleServiceVersions)
                {
                    if (versionRequirement.Service == servicename)
                    {
                        if (versionRequirement.Version > currentMajorVersion)
                        {
                            Log.Information("Event=\"Message Dropped\" Message=\"This message was droped as the service version is not compatible with the message requirements\" RequiredVersion=\"{0}\" CurrentVersion=\"{1}\" ", currentMajorVersion, versionRequirement.Version);
                            return false;
                        }                      
                    }
                }
            }
            catch (Exception)
            {

            }
            return true;
        }

        private bool CheckForSatisfiableNeed(dynamic message)
        {
            if (CheckIfCreateOpperation(message)) return true;

            var satisfiableNeedFound = false;
            if (message.Needs != null && message.Needs.Count > 0)
            {
                foreach (var need in message.Needs)
                {
                    try
                    {
                        var sampleId = need.SampleUuid;
                        satisfiableNeedFound = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!satisfiableNeedFound)
            {
                Log.Information("Event=\"Message Dropped\" Message=\"This message was droped as it contains no Needs satisfiable by this serivice\"");
                return false;
            }
               
            return true;
        }

        private static bool CheckIfCreateOpperation(dynamic message)
        {
            if (message.Method == "POST")
                return true;
            return false;
        }

        private bool CheckForExistingSolutions(dynamic message)
        {
            var solutionExists = false;
            if (message.Solutions != null && message.Solutions.Count > 0)
            {
                foreach (var solution in message.Solutions)
                {
                    try
                    {
                        var sampleId = solution.SampleUuid;
                        solutionExists = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (solutionExists)
            {
                Log.Information("Event=\"Message Dropped\" Message=\"This message was droped as it already contains a Solution satisfiable by this serivice\"");
                return false;
            }
                
            return true;
        }
    }
}
