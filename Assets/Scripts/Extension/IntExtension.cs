using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtension
{
    public static string ToWord(this int number)
    {
        switch (number)
        {
            case 0:
                return "ZERO";
            case 1:
                return "ONE";
            case 2:
                return "TWO";
            case 3:
                return "THREE";
            case 4:
                return "FOUR";
            case 5:
                return "FIVE";
            case 6:
                return "SIX";
            case 7:
                return "SEVEN";
            case 8:
                return "EIGHT";
            case 9:
                return "NINE";
            default:
                return "NULL";
        }
    }
}
