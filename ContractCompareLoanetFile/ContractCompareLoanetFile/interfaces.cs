using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ContractCompareLoanetFile
{   
    public interface IObjectCreator
    {
        string GetDataForFile();
    }

    public static class GR
    {        
        public static string ReturnFiller(int length)
        {
            return new string(' ', length);
        }

        public static string ReturnFiller(int length, char c)
        {
            return new string(c, length);
        }

        //Example GR.GetPName(() => SpoItem.CUSIP  will return the string CUSIP
        public static string GetPName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public static string SanitizeNumber(string input, int RequiredLengthBeforeDecimal, bool isRequiredLengthBeforeDecimalLeadingZeros, int RequiredLengthAfterDecimal, bool RemoveLetters)
        {
            string finalNum = "";

            //Removes Letters
            if (RemoveLetters) { input = Regex.Replace(input, @"[^0-9\.]", ""); }
            
            //Split number whether it has a decimal or not
            string[] SplitNumber = input.Split(new char[] { '.' });
            string NumToLeftOfDecimal = SplitNumber[0];
            string NumToRightOfDecimal = SplitNumber.Length == 2 ? SplitNumber[1] : "";

            //Adds leading Zeros
            NumToLeftOfDecimal = NumToLeftOfDecimal.Length <= RequiredLengthBeforeDecimal
                ?
                NumToLeftOfDecimal.PadLeft(RequiredLengthBeforeDecimal, '0')
                :
                NumToLeftOfDecimal.Substring(0, RequiredLengthBeforeDecimal);   //It's too large, substring it

            finalNum = NumToLeftOfDecimal;

            //After Decimal
            if (RequiredLengthAfterDecimal > 0)
            {
                NumToRightOfDecimal = NumToRightOfDecimal.Length <= RequiredLengthAfterDecimal 
                    ?
                    NumToRightOfDecimal.PadRight(RequiredLengthAfterDecimal, '0') 
                    : 
                    NumToRightOfDecimal.Substring(0, RequiredLengthAfterDecimal);

                finalNum += NumToRightOfDecimal;
            }

            return finalNum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val">Table value</param>
        /// <param name="reqlen">Total required length for this property</param>
        /// <param name="isLeftJustified">True means leading spaces, zeros, or what ever you put for charToFill</param>
        /// <param name="charToFill">Char used to pad the val</param>
        /// <param name="propertyName">Name of Table property.  Used for logging</param>
        /// <param name="TradeId">TradeId of position.  Used for logging</param>
        /// <returns></returns>
        public static string getval1(string val, int reqlen, bool isLeftJustified, char charToFill, string propertyName, string TradeId)
        {
            if (val != null)
            {
                if (val.Length <= reqlen)
                {
                    if (isLeftJustified) { return val.PadLeft(reqlen, charToFill); }
                    else { return val.PadRight(reqlen, charToFill); }
                }
                else
                {
                    string sid = string.IsNullOrEmpty(TradeId) ? "Unknown TradeId" : TradeId;
                    WPFUtils.Utils.LogError("TradeId:" + sid + " " + propertyName + " was greater than required length.  String was substringed to correct length");
                    return val.Substring(0, reqlen);
                }
            }

            else
            {
                string sid = string.IsNullOrEmpty(TradeId) ? "Unknown TradeId" : TradeId;
                WPFUtils.Utils.LogError("TradeId:" + sid + " " + propertyName + " was null");
                return GR.ReturnFiller(reqlen, charToFill);
            }
        }

        

        //Not used yet but may be useful for reuse
        private static string HandleNull(string propertyName, string TradeId, int requiredLength, char charToFill)
        {
            string sid = string.IsNullOrEmpty(TradeId) ? "Unknown TradeId" : TradeId;
            WPFUtils.Utils.LogError("TradeId:" + sid + " " + propertyName + " was null");
            return GR.ReturnFiller(requiredLength, charToFill);
        }

        
    }

}
