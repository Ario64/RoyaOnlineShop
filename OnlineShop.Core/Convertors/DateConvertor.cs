﻿using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineShop.Core.Convertors;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
    }
}