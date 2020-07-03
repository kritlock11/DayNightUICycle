using UnityEngine;
#pragma warning disable 0649

namespace _Scripts
{
    public class Initer : MonoBehaviour
    {
        [SerializeField] private View _view;
        [SerializeField] private DayNightUICycle _dayNightCycleCarousel;
        private InputManager _inputManager = new InputManager();

        private void Start()
        {
            GameTimeManager.Instance.Init(_dayNightCycleCarousel, _inputManager, _view);
        }
    }
}
