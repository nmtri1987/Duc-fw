using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DTP.Object
{
    public static class Datehelper
    {
        public static DateTime BuildDateTimeFromYAFormat(string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}Z$");
            if (!r.IsMatch(dateString))
            {
                throw new FormatException(
                    string.Format("{0} is not the correct format. Should be yyyyMMddThhmmZ", dateString));
            }

            DateTime dt = DateTime.ParseExact(dateString, "yyyyMMddThhmmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dt;
        }

        public static DateTime FirstDayOfMonth_AddMethod(this DateTime value)
        {
            return value.Date.AddDays(1 - value.Day);
        }

        public static DateTime FirstDayOfMonth_NewMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime LastDayOfMonth_AddMethod(this DateTime value)
        {
            return value.FirstDayOfMonth_AddMethod().AddMonths(1).AddDays(-1);
        }

        public static DateTime LastDayOfMonth_AddMethodWithDaysInMonth(this DateTime value)
        {
            return value.Date.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - value.Day);
        }

        public static bool InDate(DateTime frmDate, DateTime toDate, DateTime value)
        {
            if (value.CompareTo(frmDate) >= 0 && value.CompareTo(toDate) <= 0)
            {
                return true;
            }
            return false;
        }
        public static DateTime ConvertDate(string anyString, bool DateFirst)
        {
            DateTime dummyDate = DateTime.Now;
            try
            {
                if (DateFirst)
                {
                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
                    dummyDate = Convert.ToDateTime(anyString, ci);
                }
                else
                {
                    dummyDate = DateTime.Parse(anyString);
                }
            }
            catch (Exception objEx)
            {
            }
            return dummyDate;
        }
        public static DateTime LastDayOfMonth_SpecialCase(this DateTime value)
        {
            return value.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth_NewMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
        }

        public static DateTime LastDayOfMonth_NewMethodWithReuseOfExtMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }
        public static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }
    }
}
