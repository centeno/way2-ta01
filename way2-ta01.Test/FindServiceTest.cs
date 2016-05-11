using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using way2_ta01.model;
using way2_ta01.service;

namespace way2_ta01.Test
{
    [TestClass]
    public class FindServiceTest
    {
        FindService fs;

        [TestInitialize()]
        public void Initialize()
        {
            fs = new FindService();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Should_Throw_Exception_When_Url_IsNull()
        {
            new FindService("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Should_Throw_Exception_When_Word_IsNull()
        {
            FindResult fr = fs.Search("");
        }


        [TestMethod]
        public void Compare_Should_Return_Null_When_Index_Is_40000000000000()
        {
            FindServiceFake fsf = new FindServiceFake();
            long i = 40000000000000;
            Assert.IsNull(fsf.CompareFake(i));
        }

        [TestMethod]
        public void Search_Should_Return_Success_When_Word_Is_ABEL()
        {
            FindResult fr = fs.Search("ABEL");
            Assert.IsTrue(fr.Success);
        }

        [TestMethod]
        public void Search_Should_Return_Insuccess_When_Word_Is_CENTENO()
        {
            FindResult fr = fs.Search("CENTENO");
            Assert.IsFalse(fr.Success);
        }

        [TestMethod]
        public void Search_Should_Return_Index0_When_Word_Is_AARAO()
        {
            FindResult fr = fs.Search("AARÃO");
            Assert.AreEqual(0, (fr.Index));
        }
    }
}
