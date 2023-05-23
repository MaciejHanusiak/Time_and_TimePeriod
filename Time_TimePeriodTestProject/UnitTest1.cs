using Time_and_TimePeriod;
using Time_and_TimePeriod_Lib;

namespace Time_TimePeriodTestProject
{
    [TestClass]
    public class TimeTest
    {
        [DataTestMethod]
        [DataRow("00:00:00:0000", true)]
        [DataRow("21:37:00:323", true)]
        [DataRow("23:59:59:999", true)]
        [DataRow("24:69:69:6969", false)]
        [DataRow("12:60:00:2137", false)]
        [DataRow("12:34:60:2222", false)]
        [DataRow("12:34:56:7890", false)]
        [DataRow("1r:3s:5p:1234:321", false)]
        public void Constructor_StringInput_Valid(string input, bool shouldSucceed)
        {
            bool succeeded = true;
            try
            {
                var time = new Time(input);
            }
            catch (ArgumentException)
            {
                succeeded = false;
            }

            Assert.AreEqual(shouldSucceed, succeeded);
        }


        [TestMethod]
        [DataRow((byte)0, (byte)0, (byte)0,(short)0, (byte)0, (byte)0, (byte)0,(short)0)]
        [DataRow((byte)23, (byte)59, (byte)59, (short)999, (byte)23, (byte)59, (byte)59, (short)999)]
        [DataRow((byte)12, (byte)34, (byte)56, (short)789, (byte)12, (byte)34, (byte)56, (short)789)]
        [DataRow((byte)12, (byte)34, (byte)56, (short)789, (byte)12, (byte)34, (byte)56, (short)789)]
        public void TimeConstructor_4Arguments(byte h, byte m, byte s, short ms, byte expectedH, byte expectedM, byte expectedS,short expectedMS)
        {
            Assert.AreEqual(new Time(h, m, s,ms), new Time(expectedH, expectedM, expectedS,expectedMS));
        }

        [TestMethod]
        public void TimePeriod_AddTimePeriod_ReturnsCorrectTimePeriod()
        {
            // Arrange
            TimePeriod duration1 = new TimePeriod(1, 30, 0, 22);
            TimePeriod duration2 = new TimePeriod(0, 45, 30, 33);
            TimePeriod expectedSum = new TimePeriod(2, 15, 30, 55);

            // Act
            TimePeriod sum = duration1 + duration2;

            // Assert
            Assert.AreEqual(expectedSum, sum);
        }

        [DataTestMethod]
        [DataRow("12:34:56:78", "01:23:45:67", "13:58:41:145")]
        [DataRow("23:59:59:999", "00:00:00:001", "24:00:00:000")]
        [DataRow("00:00:00:001", "00:00:00:002", "00:00:00:003")]
        public void Plus_TimePeriodAndTimePeriod_Addition(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod)
        {
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);
            var actual = timePeriod1.Plus(timePeriod2);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56:789", "01:23:45:678", "11:11:11:111", true)]
        [DataRow("120:00:00:000", "60:00:00:010", "59:59:59:990", true)]
        [DataRow("21:37:00:43", "42:12:24:69", "44:44:44:44", false)]
        [DataRow("00:00:00:00", "00:00:00:001", "00:00:00:000", false)]
        public void Minus_TimePeriodAndTimePeriod_Subtraction(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod, bool expectedResult)
        {

            bool actualResult = false;
            bool exception = false;
            TimePeriod actual = new TimePeriod();
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);
            try { actual = timePeriod1.Minus(timePeriod2); }
            catch (Exception) { exception = true; }
            if (exception == false)
            {
                if (expected == actual)
                    actualResult = true;
            }


            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, 0, 2, "02:00:00:000")]
        [DataRow(0, 30, 0,0, 3, "01:30:00:000")]
        [DataRow(0, 0, 45,0, 4, "00:03:00:000")]
        [DataRow(2, 30, 45,999, 0, "00:00:00:000")]
        public void OperatorMultiply_ShouldReturnCorrectResult(long h, long m, long s,long ms, long multiplier, string expected)
        {

            var timePeriod = new TimePeriod(h, m, s);
            var multipliedTimePeriod = timePeriod * multiplier;

            Assert.AreEqual(expected, multipliedTimePeriod.ToString());



        }

        [DataTestMethod]
        [DataRow("12:34:56:789", "1:23:45:678", "13:58:42:467")]
        [DataRow("23:59:59:999", "00:00:00:001", "00:00:00:000")]
        [DataRow("23:59:59:999", "00:00:00:002", "00:00:00:001")]
        public void Plus_TimeAndTimePeriod_Addition(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time.Plus(timePeriod);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56:789", "01:23:45:678", "11:11:11:111")]
        [DataRow("01:02:03:004", "00:33:44:555", "00:28:18:449")]
        [DataRow("20:00:00:000", "09:09:09:999", "10:50:50:001")]
        public void Minus_TimeAndTimePeriod_Subtraction(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time.Minus(timePeriod);

            Assert.AreEqual(expected, actual);
        }
    }
}