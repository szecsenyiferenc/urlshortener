using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Providers.Impl;
using UrlShortener.Utils;

namespace UrlShortenerTests.Providers
{
    [TestFixture]
    class IdProviderTests
    {
        Mock<INumericBaseConverter> numericBaseConverterMock;
        int convertedInt = 5;
        string convertedString = "000005";

        [SetUp]
        public void SetUp()
        {
            numericBaseConverterMock = new Mock<INumericBaseConverter>(MockBehavior.Strict);
            numericBaseConverterMock.Setup(n => n.BaseToInt(It.IsAny<string>())).Returns(() => convertedInt);
            numericBaseConverterMock.Setup(n => n.IntToBase(It.IsAny<int>())).Returns(() => convertedString);
        }

        [Test]
        public void Init_ItShouldNotConvertTheValue_WhenStartIdIsNull()
        {
            string startId = null;
            var idProvider = new IdProvider(numericBaseConverterMock.Object);

            idProvider.Init(startId);

            numericBaseConverterMock.Verify(n => n.BaseToInt(startId), Times.Never());
        }

        [Test]
        public void Init_ItShouldConvertTheValueAndUseTheValue_WhenStartIdNotNull()
        {
            string startId = "000002";
            var idProvider = new IdProvider(numericBaseConverterMock.Object);

            idProvider.Init(startId);

            numericBaseConverterMock.Verify(n => n.BaseToInt(startId), Times.Once());
        }

    }
}
