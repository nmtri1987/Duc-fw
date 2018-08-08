using System;
using System.Collections.Generic;
using System.Linq;




public class BDatetime
{
    private const string DateFormat = "yyyy-MM-dd";

    private readonly List<string> _holidays;
    private readonly OpenHours _openHours;

    public BDatetime(IEnumerable<DateTime> holidays, OpenHours openHours)
    {
        _holidays = dateListToStringList(holidays);
        _openHours = openHours;
    }

    public double getElapsedMinutes(DateTime startDate, DateTime endDate)
    {
        if (_openHours.StartHour == 0 || _openHours.EndHour == 0)
            throw new InvalidOperationException("Open hours cannot be started with zero hours or ended with zero hours");

        int hour = startDate.Hour;
        int minute = startDate.Minute;
        if (hour == 0 && minute == 0)
        {
            startDate = DateTime.Parse(string.Format("{0} {1}:{2}", startDate.ToString(DateFormat), _openHours.StartHour, _openHours.StartMinute));
        }
        hour = endDate.Hour;
        minute = endDate.Minute;
        if (hour == 0 && minute == 0)
        {
            endDate = DateTime.Parse(string.Format("{0} {1}:{2}", endDate.ToString(DateFormat), _openHours.EndHour, _openHours.EndMinute));
        }

        startDate = nextOpenDay(startDate);
        endDate = prevOpenDay(endDate);

      
        if (startDate > endDate)
            return 0;

        if (startDate.ToString(DateFormat).Equals(endDate.ToString(DateFormat)))
        {
            if (!isWorkingDay(startDate))
                return 0;

            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday ||
                _holidays.Contains(startDate.ToString(DateFormat)))
                return 0;

            if (isDateBeforeOpenHours(startDate))
            {
                startDate = getStartOfDay(startDate);
            }
            if (isDateAfterOpenHours(endDate))
            {
                endDate = getEndOfDay(endDate);
            }
            var endminutes = (endDate.Hour * 60) + endDate.Minute;
            var startminutes = (startDate.Hour * 60) + startDate.Minute;

            return endminutes - startminutes;

        }

        var endOfDay = getEndOfDay(startDate);
        var startOfDay = getStartOfDay(endDate);
        var usedMinutesinEndDate = endDate.Subtract(startOfDay).TotalMinutes;
        var usedMinutesinStartDate = endOfDay.Subtract(startDate).TotalMinutes;
        var tempStartDate = startDate.AddDays(1);
        var workingHoursInMinutes = (_openHours.EndHour - _openHours.StartHour) * 60;
        var totalUsedMinutes = usedMinutesinEndDate + usedMinutesinStartDate;

        for (DateTime day = tempStartDate.Date; day < endDate.Date; day = day.AddDays(1.0))
        {
            if (isWorkingDay(day))
            {
                totalUsedMinutes += workingHoursInMinutes;
            }
        }

        return totalUsedMinutes;
    }
    public DateTime add(DateTime date, int minutes)
    {
        if (_openHours != null)
        {
            if (_openHours.StartHour == 0 || _openHours.EndHour == 0)
                throw new InvalidOperationException("Open hours cannot be started with zero hours or ended with zero hours");

            date = nextOpenDay(date);
            var endOfDay = getEndOfDay(date);
            var minutesLeft = (int)endOfDay.Subtract(date).TotalMinutes;

            if (minutesLeft < minutes)
            {
                date = nextOpenDay(endOfDay.AddMinutes(1));
                date = nextOpenDay(date);
                minutes -= minutesLeft;
            }
            var workingHoursInMinutes = (_openHours.EndHour - _openHours.StartHour) * 60;
            while (minutes > workingHoursInMinutes)
            {
                date = getStartOfDay(date.AddDays(1));
                date = nextOpenDay(date);
                minutes -= workingHoursInMinutes;
            }
        }

        return date.AddMinutes(minutes);

    }


    private List<string> dateListToStringList(IEnumerable<DateTime> dates)
    {
        return dates.Select(piDate => piDate.ToString(DateFormat)).ToList();
    }


    private DateTime prevOpenDay(DateTime endDate)
    {
        if (_holidays.Contains(endDate.ToString(DateFormat)))
        {
            return prevOpenDayAfterHoliday(endDate);
        }
        if (endDate.DayOfWeek == DayOfWeek.Saturday)
        {
            return prevOpenDayAfterHoliday(endDate);
        }
        if (endDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return prevOpenDayAfterHoliday(endDate);
        }
        if (isDateBeforeOpenHours(endDate))
        {
            return getStartOfDay(endDate);
        }
        if (isDateAfterOpenHours(endDate))
        {
            return getEndOfDay(endDate);
        }
        return endDate;
    }

    public bool isWorkingDay(DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday &&
               !_holidays.Contains(date.ToString(DateFormat));
    }

    public static bool isWeekDay(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
               //&&!_holidays.Contains(date.ToString(DateFormat));
    }


    private DateTime nextOpenDay(DateTime startDate)
    {
        if (_holidays.Contains(startDate.ToString(DateFormat)))
        {
            return nextOpenDayAfterHoliday(startDate);
        }
        if (startDate.DayOfWeek == DayOfWeek.Saturday)
        {
            return nextOpenDayAfterHoliday(startDate);
        }
        if (startDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return nextOpenDayAfterHoliday(startDate);
        }
        if (isDateBeforeOpenHours(startDate))
        {
            return getStartOfDay(startDate);
        }
        if (isDateAfterOpenHours(startDate))
        {

            var nextDate = startDate.AddDays(1);

            if (_holidays.Contains(nextDate.ToString(DateFormat)))
            {
                return nextOpenDayAfterHoliday(nextDate);
            }
            return getStartOfDay(nextDate);
        }
        return startDate;
    }

    private DateTime nextOpenDayAfterHoliday(DateTime holiday)
    {
        var nextDay = holiday.AddDays(1);
        if (nextDay.DayOfWeek == DayOfWeek.Saturday)
            nextDay = nextDay.AddDays(2);
        if (nextDay.DayOfWeek == DayOfWeek.Sunday)
            nextDay = nextDay.AddDays(1);
        while (_holidays.Contains(nextDay.ToString(DateFormat)))
        {
            nextDay = nextDay.AddDays(1);
        }
        return getStartOfDay(nextDay);
    }

    private DateTime prevOpenDayAfterHoliday(DateTime holiday)
    {
        var prevDay = holiday.AddDays(-1);
        if (prevDay.DayOfWeek == DayOfWeek.Saturday)
            prevDay = prevDay.AddDays(-1);
        if (prevDay.DayOfWeek == DayOfWeek.Sunday)
            prevDay = prevDay.AddDays(-2);
        while (_holidays.Contains(prevDay.ToString(DateFormat)))
        {
            prevDay = prevDay.AddDays(-1);
        }
        return getEndOfDay(prevDay);
    }

    private DateTime getStartOfDay(DateTime nextDate)
    {
        return DateTime.Parse(string.Format("{0} {1}:{2}", nextDate.ToString(DateFormat), _openHours.StartHour, _openHours.StartMinute));
    }

    private DateTime getEndOfDay(DateTime startDate)
    {
        return DateTime.Parse(string.Format("{0} {1}:{2}", startDate.ToString(DateFormat), _openHours.EndHour, _openHours.EndMinute));
    }

    private bool isDateBeforeOpenHours(DateTime startDate)
    {
        return startDate.Hour < _openHours.StartHour || (startDate.Hour == _openHours.StartHour && startDate.Minute < _openHours.StartMinute);
    }
    private bool isDateAfterOpenHours(DateTime startDate)
    {
        return startDate.Hour > _openHours.EndHour || (startDate.Hour == _openHours.EndHour && startDate.Minute > _openHours.EndMinute);
    }

}

public class BWorkingDate
{
    public static BDatetime SystemDate
    {
        get
        {
            return new BDatetime(new List<DateTime>(), new OpenHours("08:30;16:00"));
        }
    }
    public static string TimeReturn(double Hour)
    {
        TimeSpan b = TimeSpan.FromHours(Hour);
        if (Hour > 0)
        {
            if (Hour >= 8)
            {
                int days = (int)Hour / 8;
                int remainTime = (int)Hour - (days * 8);
                if (days == 0)
                {
                    return string.Format("{0} Hours {1} Minutes", remainTime, b.Minutes);
                }
                if (days == 1)
                {
                    return string.Format("{0} Day {1} Hours {2} Minutes", days, remainTime, b.Minutes);
                }else
                {
                    return string.Format("{0} Days {1} Hours {2} Minutes", days, remainTime, b.Minutes);
                }
            }
            return string.Format("{0} Hours {1} Minutes", b.Hours, b.Minutes);
        }
        else
        {
            return string.Format("{0} Minutes", b.Minutes);
        }
    }
}
//namespace CoreTest
//{
//    [TestClass]
//    public class CalculationTests
//    {
//        private static Calculation getEmptyCalculator()
//        {
//            return new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));
//        }

//        #region add

//        [TestMethod]
//        public void add_NoRule_AddMinutes()
//        {
//            var calculator = getEmptyCalculator();
//            var startDate = DateTime.Parse("2013-01-01 10:00");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-01 11:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeIsSaturday_addFromMonday()
//        {
//            var calculator = getEmptyCalculator();

//            var saturdayStartDate = DateTime.Parse("2013-01-05 10:00");

//            var result = calculator.add(saturdayStartDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-07 09:00"), result);//Monday
//        }

//        [TestMethod]
//        public void add_StartTimeIsSunday_addFromMonday()
//        {
//            var calculator = getEmptyCalculator();

//            var sundayStartDate = DateTime.Parse("2013-01-06 10:00");

//            var result = calculator.add(sundayStartDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-07 09:00"), result); //Monday
//        }

//        [TestMethod]
//        public void add_StartTimeIsHoliday_addFromNextWorkingDay()
//        {
//            var calculator = new Calculation(new List<DateTime> { DateTime.Parse("2013-01-15") }, new OpenHours("08:00;16:00"));
//            var startDate = DateTime.Parse("2013-01-15 10:00");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-16 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimBeforeOfficeHours_AddFromOfficeHour()
//        {
//            var calculator = getEmptyCalculator();
//            var startDate = DateTime.Parse("2013-01-01 06:00");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-01 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeAfterOfficeHours_AddFromNextWorkingDayOfficeHour()
//        {
//            var calculator = getEmptyCalculator();

//            var startDate = DateTime.Parse("2013-01-01 16:15");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-02 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeWithinOfficeHour_AddMinutes()
//        {
//            var calculator = getEmptyCalculator();

//            var startDate = DateTime.Parse("2013-01-01 08:15");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-01 09:15"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeWithinOfficeHourCloseToEnd_AddMinutes()
//        {
//            var calculator = getEmptyCalculator();

//            var startDate = DateTime.Parse("2013-08-28 09:05:18");

//            var result = calculator.add(startDate, 405); //6.75 hrs

//            Assert.AreEqual(DateTime.Parse("2013-08-28 15:50:18"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeWithinOfficeHourButEndOnNextDay_AddMinutes()
//        {
//            var calculator = getEmptyCalculator();

//            var startDate = DateTime.Parse("2013-01-01 15:00");

//            var result = calculator.add(startDate, 120);

//            Assert.AreEqual(DateTime.Parse("2013-01-02 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeWithinOfficeHourButEndOnThreeDaysLater_AddMinutes()
//        {
//            var calculator = getEmptyCalculator();

//            var startDate = DateTime.Parse("2013-01-01 15:00");

//            var result = calculator.add(startDate, 1080); //18 hours

//            Assert.AreEqual(DateTime.Parse("2013-01-04 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeAfterOfficeHourButNextDayIsHoliday_AddFromNextWorkingDay()
//        {
//            var calculator = new Calculation(new List<DateTime> { DateTime.Parse("2013-01-15") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2013-01-14 16:30");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-16 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeAfterOfficeHourButNextDayIsHolidayFollowedWithSaturday_AddFromMonday()
//        {
//            var calculator = new Calculation(new List<DateTime> { DateTime.Parse("2013-01-18") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2013-01-17 16:30");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2013-01-21 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeIsHolidayAndNextDayAlsoHoliday_addFromNextWorkingDay()
//        {
//            var calculator = new Calculation(new List<DateTime> { DateTime.Parse("2014-03-05"), DateTime.Parse("2014-03-06") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2014-03-05 10:00");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2014-03-07 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeIsFridayAndHolidayThenMondayAlsoHoliday_addFromTuesday()
//        {
//            var calculator = new Calculation(new List<DateTime> { DateTime.Parse("2014-03-07"), DateTime.Parse("2014-03-10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2014-03-07 10:00");

//            var result = calculator.add(startDate, 60);

//            Assert.AreEqual(DateTime.Parse("2014-03-11 09:00"), result);
//        }

//        [TestMethod]
//        public void add_StartTimeIsFridayWithinOfficeHours_MaxStartTimeExceedOfficeHours()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;17:00"));

//            var startDate = DateTime.Parse("2014-10-10 14:00");

//            var result = calculator.add(startDate, 720);

//            Assert.AreEqual(DateTime.Parse("2014-10-13 17:00"), result);
//        }
//        [TestMethod]
//        public void add_StartTimeIsFridayWithinOfficeHours_MaxStartTimeExceedOfficeHours_WithMaxThreshHold()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;17:00"));

//            var startDate = DateTime.Parse("2014-10-10 14:00");

//            var result = calculator.add(startDate, 912);

//            Assert.AreEqual(DateTime.Parse("2014-10-14 11:12"), result);
//        }

//        #endregion add

//        #region getElapsedMinutes

//        [TestMethod]
//        public void getElapsedMinutes_SameDateBut2HoursDiffernt_120()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-07 08:00");
//            var endDate = DateTime.Parse("2015-04-07 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(120, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateBut8HoursDiffernt_480()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-07 08:00");
//            var endDate = DateTime.Parse("2015-04-07 16:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(480, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButStartTimeBeforeOfficeHoursAnd5HoursDiffernt_300()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-07 07:00");
//            var endDate = DateTime.Parse("2015-04-07 13:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(300, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButEndTimeAfterOfficeHoursAnd3HoursDiffernt_180()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-07 13:00");
//            var endDate = DateTime.Parse("2015-04-07 18:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(180, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButHoliday_0()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-06") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-06 13:00");
//            var endDate = DateTime.Parse("2015-04-06 18:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(0, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButWeekEnd_0()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-05 13:00");
//            var endDate = DateTime.Parse("2015-04-05 18:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(0, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButStartTimeGreaterThanEndTime_0()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-08 13:00");
//            var endDate = DateTime.Parse("2015-04-08 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(0, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_DatesDifferent_600()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-07 10:00");
//            var endDate = DateTime.Parse("2015-04-08 12:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(600, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_DatesDifferentButStartDateHoliday_countFromNextOpenday()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-06") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-06 12:00");
//            var endDate = DateTime.Parse("2015-04-08 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(600, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_DatesDifferentButEndDateHoliday_countExcludingEndDate()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-08 14:00");
//            var endDate = DateTime.Parse("2015-04-10 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(600, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_DatesDifferentButNoWorkingDays_0()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-06") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-03 16:10");
//            var endDate = DateTime.Parse("2015-04-06 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(0, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_DatesDifferentButEndDateIsAfterOfficeHours_exlcudeAfterOfficeHour()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-08 10:10"); //350
//            var endDate = DateTime.Parse("2015-04-09 17:30"); //480

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(830, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_5Days_2400()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-06 06:10");
//            var endDate = DateTime.Parse("2015-04-10 17:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(2400, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_5DaysIncludingOneHoliday_1920()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-07 06:10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-06 06:10");
//            var endDate = DateTime.Parse("2015-04-10 17:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(1920, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_5DaysIncludingOneHolidayOneSunday_1440()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-07 06:10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-05 06:10");
//            var endDate = DateTime.Parse("2015-04-09 17:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(1440, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_5DaysIncludingOneHolidayOneWeekend_960()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-07 06:10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-04 05:10");
//            var endDate = DateTime.Parse("2015-04-08 17:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(960, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_4DaysIncludingOneHolidayOneWeekend_1WorkingDay480()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-06 06:10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-04 05:10");
//            var endDate = DateTime.Parse("2015-04-07 17:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(480, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_4DaysIncludingOneHolidayOneWeekend_390()
//        {
//            var calculator = new Calculation(new List<DateTime>() { DateTime.Parse("2015-04-06 06:10") }, new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-04 05:10");
//            var endDate = DateTime.Parse("2015-04-07 14:30");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(390, result);
//        }
//        [TestMethod]
//        public void getElapsedMinutes_EndDateDoesNotHaveTime_considerOfficeCloseTime()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-02 17:00");//created after office hour
//            var endDate = DateTime.Parse("2015-04-03 00:00"); //next day is end date but hours not specified. consider the end of office hours

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(480, result);//one day
//        }
//        [TestMethod]
//        public void getElapsedMinutes_SameDateButStartDateDoesNotHaveTime_considerOfficeStartTime()
//        {
//            var calculator = new Calculation(new List<DateTime>(), new OpenHours("08:00;16:00"));

//            var startDate = DateTime.Parse("2015-04-02 00:00");
//            var endDate = DateTime.Parse("2015-04-02 10:00");

//            var result = calculator.getElapsedMinutes(startDate, endDate);

//            Assert.AreEqual(120, result);
//        }

//        #endregion getElapsedMinutes
//    }
//}
