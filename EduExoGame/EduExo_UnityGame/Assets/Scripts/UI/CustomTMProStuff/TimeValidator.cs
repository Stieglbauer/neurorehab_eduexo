using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "TimeValidator", menuName = "Time Input Validator")]
public class TimeValidator : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        string[] subtexts = text.Split(':');
        if(ch == ':')
        {
            if(subtexts.Length != 1)
            {
                // no two ':' allowed
                return '\0';
            } else
            {
                text = text.Insert(pos++, "" + ch);
                return ch;
            }
        } else if(char.IsNumber(ch))
        {
            if(subtexts.Length == 1 || subtexts[1].Length < 2 || pos <= subtexts[0].Length)
            {
                text = text.Insert(pos++, ""+ch);
                return ch;
            } else
            {
                return '\0';
            }
        } else
        {
            return '\0';
        }
    }
}
