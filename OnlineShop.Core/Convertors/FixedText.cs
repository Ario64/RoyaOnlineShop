﻿namespace OnlineShop.Core.Convertors;

public class FixedText
{
    public static string FixedEmail(string email)
    {
        return email.Trim().ToLower();
    }
}