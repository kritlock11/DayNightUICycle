using UnityEngine;
using TMPro;
#pragma warning disable 0649

namespace _Scripts
{
    public interface IView
    {
        void UpdateCurTime(GameTimeDate currentTime);
    }

    public class View : MonoBehaviour, IView
    {
        [SerializeField] private TextMeshProUGUI CurrentTimeTextDay;
        [SerializeField] private TextMeshProUGUI CurrentTimeTextMonth;
        [SerializeField] private TextMeshProUGUI CurrentTimeTextYear;
        [SerializeField] private TextMeshProUGUI CurrentTimeText;

        public void UpdateCurTime(GameTimeDate currentTime)
        {
            CurrentTimeText.text = $"{currentTime.Hour}:00";
            
            CurrentTimeTextDay.text = currentTime.Day.ToString("0");
            CurrentTimeTextMonth.text = Variables.MonthNames[currentTime.Month];
            CurrentTimeTextYear.text = currentTime.Year.ToString("0");
        }
    }
}