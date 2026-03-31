using System;
using TyrAds.Data;
using UnityEngine;

namespace TyrAds.Demo
{
    [CreateAssetMenu(fileName = "TyrAdsTestSessions", menuName = "TyrAds/Test Sessions", order = 1)]
    public class TyrAdsTestSessions : ScriptableObject
    {
        [SerializeField] private TestSessionConfig[] testSessionConfigs;

        public TestSessionConfig[] TestSessionConfigs => testSessionConfigs;
    }

    [Serializable]
    public class TestSessionConfig
    {
        [SerializeField] private string id;
        [SerializeField] private SessionConfig config;

        public string Id => id;
        public SessionConfig Config => config;
    }
}