using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CleanArchitecture.Application.Common.Behaviours;

public static class PersianDateTime
{
    public static string ToShamsi(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00")
               + " " + pc.GetHour(value).ToString("00") + " : " + pc.GetMinute(value).ToString("00") + " : "
               + pc.GetSecond(value).ToString("00");
    }
    public static string ToDateShamsi(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }
    public static string ToShamsiForMessage(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00")
               + "{" + pc.GetHour(value).ToString("00") + " : " + pc.GetMinute(value).ToString("00") + "}";
    }
    public static string ToTimeShamsi(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00")
               + " " + pc.GetHour(value).ToString("00") + " : " + pc.GetMinute(value).ToString("00") + " : "
               + pc.GetSecond(value).ToString("00");
    }
}
