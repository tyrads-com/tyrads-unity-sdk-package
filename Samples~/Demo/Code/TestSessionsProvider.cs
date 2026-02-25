using System.Collections.Generic;
using UnityEngine;

namespace TyrAds.Demo
{
    public class TestSessionsProvider
    {
        private const string TyradsTestSessionsPath = "TyrAdsTestSessions";
        
        private static readonly Dictionary<string, TestSessionConfig> _testSessions = new();
        private static bool _isLoaded; 

        public bool IsTestSessionAvailable => TestSessions is { Count: > 0 };
        
        public IReadOnlyDictionary<string, TestSessionConfig> TestSessions
        {
            get
            {
                if (_isLoaded)
                {
                    return _testSessions;
                }

                TyrAdsTestSessions testSessionConfig = Resources.Load<TyrAdsTestSessions>(TyradsTestSessionsPath);

                if (testSessionConfig != null)
                {
                    foreach (TestSessionConfig config in testSessionConfig.TestSessionConfigs)
                    {
                        _testSessions.TryAdd(config.Id, config);
                    }
                }
                    
                _isLoaded = true;

                return _testSessions;
            }
        }
    }
}