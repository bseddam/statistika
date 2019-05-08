using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public static class ConvertTypes
{

    /// <summary>
    /// stringe cevirir . source :  ConvertTypes class
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToParseStr(this object value)
    {
        if (value == null)
            return "";

        return Convert.ToString(value);
    }
    public static bool IsNumeric_Int(this string s)
    {
        int num1;
        return int.TryParse(s, out num1);
    }

    public static bool IsNumeric_double(this string s)
    {
        double num1;
        return double.TryParse(s, out num1);
    }
    public static bool IsNumeric(this string s)
    {
        if (s == null) return false;
        if (s.Length < 1) return false;
        for (int i = 0; i < s.Length; i++)
        {
            if ("0123456789".IndexOf(s.Substring(i, 1)) < 0)
            {
                return false;
            }
        }
        return true;
    }
    public static int ToParseInt(this object value)
    {
        if (value == null || ToParseStr(value).Length < 1)
        {
            return 0;
        }
        return int.Parse(Convert.ToString(value));
    }


    public static int ToParseInt(this string value)
    {
        if (value == null)
        {
            return 0;
        }
        if (value.Length < 1)
        {
            return 0;
        }
        int val = 0;
        int.TryParse(value, out val);

        return val;
    }
}