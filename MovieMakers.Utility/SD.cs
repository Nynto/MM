using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMakers.Utility
{
    public static class SD
    {
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";

        public const string Role_Customer = "Customer";
        public const string Role_FO_Employee = "Front Office Employee";
        public const string Role_Admin = "Administrator";
        public const string Role_BO_Employee = "Back Office Employee";
        
        public const string ssShoppingCart = "Shopping Cart Session";


        // public static double GetPrice(double quantityAdult, double quantityReduction, double price, double reductionprice)
        // {
        //     if (quantityAdult < 1)
        //     {
        //         
        //     }
        // }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
