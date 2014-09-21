using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mikpo.Classes;

namespace mikpoTests
{
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        ///  Муравьев Игорь
        ///   21.09.2014
        /// </summary>

        [TestClass]
        public class mikpoTests
        {
            [TestMethod]
            public void TriangleInfoTest_getC_3_4_90()
            {
                double a = 3;
                double b = 4;
                double alpha = 90;

                double res = TriangleInfo.getC(a, b, alpha);

                Assert.AreEqual(5, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getC_2_2_60()
            {
                double a = 2;
                double b = 2;
                double alpha = 60;

                double res = TriangleInfo.getC(a, b, alpha);

                Assert.AreEqual(2, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getC_1_1_180()
            {
                double a = 1;
                double b = 1;
                double alpha = 180;

                double res = TriangleInfo.getC(a, b, alpha);

                Assert.AreEqual(2, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getC_0_1_45()
            {
                double a = 0;
                double b = 1;
                double alpha = 45;

                double res = TriangleInfo.getC(a, b, alpha);

                Assert.AreEqual(1, res, 1E-6);
            }


            [TestMethod]
            public void TriangleInfoTest_getC_1_neg1_60()
            {
                double a = 1;
                double b = -1;
                double alpha = 60;

                double res = TriangleInfo.getC(a, b, alpha);

                Assert.AreEqual(Math.Sqrt(3), res, 1E-6);
            }


            [TestMethod]
            public void TriangleInfoTest_getAngle_3_4_5()
            {
                double a = 3;
                double b = 4;
                double c = 5;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(90, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getAngle_3_3_3()
            {
                double a = 3;
                double b = 3;
                double c = 3;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(60, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getAngle_1_1_2()
            {
                double a = 1;
                double b = 1;
                double c = 2;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(180, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getAngle_neg3_4_5()
            {
                double a = -3;
                double b = 4;
                double c = 5;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(90, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getAngle_neg4_4_5()
            {
                double a = -4;
                double b = 4;
                double c = 5;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(102.63562509, res, 1E-6);
            }
            [TestMethod]
            public void TriangleInfoTest_getAngle_4_4_8()
            {
                double a = 4;
                double b = 4;
                double c = 8;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(180, res, 1E-6);
            }

            [TestMethod]
            public void TriangleInfoTest_getAngle_NAN_4_8()
            {
                double a = Double.NaN;
                double b = 4;
                double c = 8;

                double res = TriangleInfo.getAngle(a, b, c);

                Assert.AreEqual(Double.NaN, res);
            }

            [TestMethod]
            public void TriangleInfoTest_parsestring_3_4_90()
            {
                string s = "3;4;90";

                double[] res = TriangleInfo.parseString(s);

                CollectionAssert.AreEqual(new double[] { 3, 4, 90 }, res);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void TriangleInfoTest_parsestring_neg3_4_90()
            {
                string s = "-3;4;90";

                double[] res = TriangleInfo.parseString(s);


            }

            [TestMethod]
            [ExpectedException(typeof(IndexOutOfRangeException))]
            public void TriangleInfoTest_parsestring_3_3_4_90()
            {
                string s = "3;3;4;90";

                double[] res = TriangleInfo.parseString(s);

            }


            [TestMethod]
            [ExpectedException(typeof(FormatException))]
            public void TriangleInfoTest_parsestring_dasdas()
            {
                string s = "dasdas";

                double[] res = TriangleInfo.parseString(s);
            }

            [TestMethod]
            [ExpectedException(typeof(FormatException))]
            public void TriangleInfoTest_parsestring_dcdcdc()
            {
                string s = ";;";

                double[] res = TriangleInfo.parseString(s);


            }


            [TestMethod]
            [ExpectedException(typeof(FormatException))]
            public void TriangleInfoTest_parsestring_emp()
            {
                string s = "";

                double[] res = TriangleInfo.parseString(s);


            }



            [TestMethod]
            [ExpectedException(typeof(System.IO.FileNotFoundException))]
            public void TriangleInfoTest_Processing_fnotfound()
            {
                string s = "dasdas.tst";
                TriangleInfo.Processing(s, "213.txt");
            }



        }
    }
}
