using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Utils.Impl;

namespace UrlShortenerTests.Utils
{
    [TestFixture]
    class NumericBaseConverterTests
    {

        [Test]
        public void IntToBase_ItShouldConvertToTheMinValue_WhenItIs1()
        {
            int intToConvert = 1;

            var numericBaseConverter = new NumericBaseConverter();
            string result = numericBaseConverter.IntToBase(intToConvert);

            Assert.AreEqual("000000", result);
        }

        [Test]
        public void IntToBase_ItShouldConvertToTheMaxValue_WhenItIs0()
        {
            int intToConvert = 0;

            var numericBaseConverter = new NumericBaseConverter();
            string result = numericBaseConverter.IntToBase(intToConvert);

            Assert.AreEqual("4gfFC3", result);
        }

        [Test]
        public void IntToBase_ItShouldHandleTheIntOverflow_WhenIntMaxExceeded()
        {
            int intToConvert;
            unchecked
            {
                intToConvert = Int32.MaxValue;
                intToConvert++;
            }

            var numericBaseConverter = new NumericBaseConverter();
            string result = numericBaseConverter.IntToBase(intToConvert);

            Assert.AreEqual(Int32.MinValue, intToConvert);
            Assert.AreEqual("2LKcb1", result);
        }

        [Test]
        public void BaseToInt_ItShouldConvertBackToTheMinValue_WhenItIs000000()
        {
            string stringToConvert = "000000";

            var numericBaseConverter = new NumericBaseConverter();
            int result = numericBaseConverter.BaseToInt(stringToConvert);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void BaseToInt_ItShouldConvertBackToTheMaxValue_WhenItIs4gfFC3()
        {
            string stringToConvert = "4gfFC3";

            var numericBaseConverter = new NumericBaseConverter();
            int result = numericBaseConverter.BaseToInt(stringToConvert);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void BaseToInt_ItShouldHandleTheMinInt_WhenItsReached()
        {
            string stringToConvert = "2LKcb1";

            var numericBaseConverter = new NumericBaseConverter();
            int result = numericBaseConverter.BaseToInt(stringToConvert);

            Assert.AreEqual(Int32.MinValue, result);
        }

        [Test]
        public void BaseToInt_ItShouldHandleTheMaxInt_WhenItsReached()
        {
            string stringToConvert = "2LKcb0";

            var numericBaseConverter = new NumericBaseConverter();
            int result = numericBaseConverter.BaseToInt(stringToConvert);

            Assert.AreEqual(Int32.MaxValue, result);
        }
    }
}
