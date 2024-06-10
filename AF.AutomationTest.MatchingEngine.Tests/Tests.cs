using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AF.AutomationTest.MatchingEngine.Tests
{
    [TestClass]
    public class Tests
    {
        private static MatchingApi _matchingApi;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _matchingApi = new MatchingApi();
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            _matchingApi.ClearData();
        }

        // example test
        [TestMethod]
        public void TestSymbolFindMatch()
        {
            var date = DateTime.UtcNow;

            var recordUpper = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);
            var recordLower = _matchingApi.CreateRecord("test", 100, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(recordUpper, recordLower);

            Assert.IsTrue(isMatched);

        }

        [TestMethod]
        public void TestSymbolLowerFirstFindMatch()
        {
            var date = DateTime.UtcNow;

            var recordLower = _matchingApi.CreateRecord("test", 100, 10, date, Side.Sell);
            var recordUpper = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);

            var isMatched = _matchingApi.CheckIfRecordsMatched(recordUpper, recordLower);

            Assert.IsTrue(isMatched);

        }

        [TestMethod]
        public void TestSymbolTwoUpperFindMatch()
        {
            var date = DateTime.UtcNow;

            var recordLower = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Sell);
            var recordUpper = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);

            var isMatched = _matchingApi.CheckIfRecordsMatched(recordUpper, recordLower);

            Assert.IsTrue(isMatched);

        }

        [TestMethod]
        public void TestPriceTwoNumbersAfterTheDotFindMatch()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("test", 100, 10.001, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("test", 100, 10.002, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }

        [TestMethod]
        public void TestQuantityFiveLeranceFindMatch()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("test", 95, 10, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("test", 100, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);
            Assert.IsTrue(isMatched);
  
        }

        [TestMethod]
        public void TestQuantityLessFindMatch()
        {
            var date = DateTime.UtcNow;

            var record2 = _matchingApi.CreateRecord("test", 100, 10, date, Side.Buy);
            var record1 = _matchingApi.CreateRecord("test", 85, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);
            Assert.IsFalse(isMatched);

        }
    }
}