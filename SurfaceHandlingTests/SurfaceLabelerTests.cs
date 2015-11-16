using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurfaceHandling;

namespace SurfaceHandlingTests
{
    [TestClass]
    public class SurfaceLabelerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            SurfaceLabeler labeler = new SurfaceLabeler();

            int[,] testArr = {{1, 0, 0}, {0, 0, 0}, {1, 1, 1}, {1,1,1}};

            labeler.Labeling(testArr);

            Assert.AreEqual(true, true);
        }
    }
}
