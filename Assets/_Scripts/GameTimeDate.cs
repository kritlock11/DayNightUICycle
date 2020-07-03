using System;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    [Serializable]
    public class GameTimeDate
    {
        public int Hour;
        public int Day;
        public int Month;
        public int Year;

        public GameTimeDate(int newHour, int newDay, int newMonth, int newYear)
        {
            Hour = Mathf.Clamp(newHour, 0, 24);
            Day = Mathf.Clamp(newDay, 0, Variables.DaysInAMonth[newMonth - 1]);
            Month = Mathf.Clamp(newMonth - 1, 0, Variables.DaysInAMonth.Length - 1);
            Year = Mathf.Clamp(newYear, 0, int.MaxValue);
        }

        public void Change(int numberOfHours)
        {
            Hour += numberOfHours;
            PassValidate();
        }

        private void PassValidate()
        {
            if (Hour >= 24)
            {
                Hour = 0;
                Day++;
            }

            if (Day > Variables.DaysInAMonth[Month])
            {
                Day = 1;
                Month++;
            }

            if (Month > Variables.MonthNames.Length - 1)
            {
                Month = 0;
                Year++;
            }
        }
    }
}