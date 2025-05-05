using System.Collections;
using BenchmarkDotNet.Attributes;
// ReSharper disable UnusedVariable

namespace Dabensymon.MultiDictionary.Tests.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount:20)]
    public class BenchmarkTests
    {
        private readonly Dictionary<string, string> _standardDict = new();
        private readonly TwoWayDictionary<string, string> _twoWayDict = [];

        [Benchmark]
        public void CreateStandardDictionary()
        {
            var dict = new Dictionary<string, string>();
        }

        [Benchmark]
        public void CreateTwoWayDictionary()
        {
            var dict = new TwoWayDictionary<string, string>();
        }

        [Benchmark]
        public void CreateHashtable()
        {
            var dict = new Hashtable();
        }
    
        #region AddItem

        [Benchmark]
        public void AddItemToStandardDictionary()
        {
            _standardDict.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    
        [Benchmark]
        public void AddItemToTwoWayDictionary()
        {
            _twoWayDict.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    
        [GlobalCleanup(Targets =
        [
            nameof(AddItemToStandardDictionary),
            nameof(AddItemToTwoWayDictionary),
        ])]
        public void GlobalCleanup()
        {
            _standardDict.Clear();
            _twoWayDict.Clear();
        }

        #endregion

        #region GetItem

        [GlobalSetup]
        public void GlobalSetupGetItem()
        {
            var i = 0;
            while (i <= 100)
            {
                _standardDict.Add($"itemKey{i}", $"itemKey{i+1}");
                _twoWayDict.Add($"itemKey{i}", $"itemKey{i+1}");
                i += 2;
            }
        }
    
    
        [Benchmark]
        public void GetItemByKeyFromStandardDictionary()
        {
            var value = _standardDict["itemKey10"];
        }
    
        [Benchmark]
        public void GetItemByFirstKeyFromTwoWayDictionary()
        {
            var value = _twoWayDict.GetValueByFirstKey("itemKey10");
        }
    
        [Benchmark]
        public void GetItemBySecondKeyFromTwoWayDictionary()
        {
            var value = _twoWayDict.GetValueBySecondKey("itemKey11");
        }
    
        [Benchmark]
        public void TryGetItemByKeyFromStandardDictionary()
        {
            _standardDict.TryGetValue("itemKey10", out string? value);
        }
    
        [Benchmark]
        public void TryGetItemByFirstKeyFromTwoWayDictionary()
        {
            _twoWayDict.TryGetValueByFirstKey("itemKey10", out string? value);
        }
    
        [Benchmark]
        public void TryGetItemBySecondKeyFromTwoWayDictionary()
        {
            _twoWayDict.TryGetValueBySecondKey("itemKey11", out string? value);
        }
    
        [Benchmark]
        public void GetFirstItemByValueFromStandardDictionary()
        {
            var value = _standardDict.FirstOrDefault(x => x.Value == "itemKey1").Key;
        }
    
        [Benchmark]
        public void GetFirstItemByValueFromTwoWayDictionary()
        {
            var value = _twoWayDict.GetValueBySecondKey("itemKey1");
        }
    
        [Benchmark]
        public void GetLastItemByValueFromStandardDictionary()
        {
            var value = _standardDict.FirstOrDefault(x => x.Value == "itemKey101").Key;
        }
    
        [Benchmark]
        public void GetLastItemByValueFromTwoWayDictionary()
        {
            var value = _twoWayDict.GetValueBySecondKey("itemKey101");
        }

    
        #endregion
    }
}