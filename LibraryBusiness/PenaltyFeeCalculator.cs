using System;
using System.Collections.Generic;
using System.Text;
using LibraryConfigUtilities;

namespace LibraryBusiness
{
    /* Description,
     * settingList member holds configuration parameters stored in the App.config file, 
     * please explore the properties and methods in the Country class to get a better understanding.
     * 
     * Please implement this class accordingly to accomplish requirements.
     * Feel free to add any parameters, methods, class members, etc. if necessary
     */
    public class PenaltyFeeCalculator{
        
        private List<Country> settingList = new LibrarySetting().LibrarySettingList;
        

        public PenaltyFeeCalculator() {
           
        }

        public String Calculate(string CountryCode, DateTime DateStart, DateTime DateEnd) 
        {

            int days =0;
            string fee="";
            Country c = null;
            for (int k = 0; k < settingList.Count; k++)
            {
                if (CountryCode == settingList[k].CountryCode)
                {
                    c = settingList[k];
                    
                    
                }
            }
            if (c == null)
            {
                throw new Exception("Country code is invalid(can’t find any configuration value for the country)");
            }
            DateStart = DateStart.AddDays(c.PenaltyAppliesAfter);
            days = Convert.ToInt32((DateEnd - DateStart).TotalDays);
            days++;
            for (int t = 0; t < c.HolidayList.Count; t++)
            {
                if (DateTime.Compare(DateEnd, c.HolidayList[t]) >= 0 && DateTime.Compare(DateStart, c.HolidayList[t]) <= 0)
                {
                    days--;
                }
            }
            while (DateTime.Compare(DateEnd, DateStart) != 0)
            {
                if (c.WeekendList.Contains(DateStart.DayOfWeek))
                {
                    days--;
                }
                DateStart = DateStart.AddDays(1);
            }
            fee = String.Format("{0:0.00}", (days * Convert.ToDouble(c.DailyPenaltyFee)));

            return fee+c.Currency;
        }
    }
}
