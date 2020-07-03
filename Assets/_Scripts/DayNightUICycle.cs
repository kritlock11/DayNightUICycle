using UnityEngine;
using System;
#pragma warning disable 0649


namespace _Scripts
{
    public interface IDayNightView
    {
        void UpdatePositions();
    }
    
    public class DayNightUICycle : MonoBehaviour, IDayNightView
    {
        [SerializeField] private RectTransform _midday;
        [SerializeField] private RectTransform _midnight;
        [SerializeField] private RectTransform _dawn;
        [SerializeField] private RectTransform _dusk;

        private const double Radius = 150;
        private const double GameTimeSpeedCoefficient = 2 * Math.PI / 24;
        private const double MidnightDefaultValue = Math.PI * 0.5;
        private const double DawnDefaultValue = Math.PI;
        private const double MiddayDefaultValue = Math.PI * 1.5;
        private const double DuskDefaultValue = Math.PI * 2;

        private double _phase;
        private Vector3 _vec = Vector3.zero;

        public void UpdatePositions()
        {
            _phase -= GameTimeManager.Instance.DeltaTime * GameTimeSpeedCoefficient;

            UpdatePosition(_midnight, MidnightDefaultValue);
            UpdatePosition(_dawn, DawnDefaultValue);
            UpdatePosition(_midday, MiddayDefaultValue);
            UpdatePosition(_dusk, DuskDefaultValue);
        }

        private void UpdatePosition(RectTransform obj, double timePhase)
        {
            var x = Math.Cos(_phase + timePhase) * Radius;
            var y = Math.Sin(_phase + timePhase) * Radius;

            UpdateVec((float)x, (float)y);

            obj.transform.localPosition = _vec;
        }

        private void UpdateVec(float x, float y, float z = default)
        {
            _vec.x = x;
            _vec.y = y;
            _vec.z = z;
        }
    }


}
