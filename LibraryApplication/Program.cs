using System;
using System.Collections.Generic;
using System.Text;
using LibraryBusiness;
using System.Configuration;
using System.Xml;

namespace LibraryApplication
{
    public class Program{
        static void Main(string[] args){
            if (args == null || args.Length != 3) {
                PrintUsage();
                return;
            }
            String fee = "";
            try{

                string CountryCode = args[0];
                DateTime DateStart = Convert.ToDateTime(args[1]);
                DateTime DateEnd = Convert.ToDateTime(args[2]);
                int result = DateTime.Compare(DateStart, DateEnd);
                if (result>0)
                {
                    throw new Exception("The end date is less than start date.");
                }
                fee = new PenaltyFeeCalculator().Calculate(CountryCode, DateStart, DateEnd);
                /* Description,
                 * please implement PenaltyFeeCalculator class
                 * feel free to make any changes (if necessary) 
                 * to PenaltyFeeCalculator class method and constructor signatures,
                 * as well as here this very portion of the main method
                 * You should not need to change any other methods in this class.
                 */


            }
            catch (Exception e) {
                PrintErrorMessage(e);
            }
            PrintResultMessage(fee);
           
        }
        private static void PrintUsage(){
            Console.WriteLine("Please provide these parameters (without brackets) : <CountryCode> <DateStart> <DateEnd>");
            PrintAnyKeyMessage();
            Console.ReadKey();
        }
        private static void PrintAnyKeyMessage(){
            Console.WriteLine("Press any key to continue");
        }
        private static void PrintResultMessage(string fee){
            Console.WriteLine("Penalty Fee is {0}", fee);
            PrintAnyKeyMessage();
            Console.ReadKey();
        }
        private static void PrintErrorMessage(Exception e) {
            Console.WriteLine("Exception : " + e.Message);
            Console.WriteLine("Stacktrace : ");
            Console.WriteLine(e.StackTrace);
            PrintAnyKeyMessage();
            Console.ReadKey();
        }
    }

}
