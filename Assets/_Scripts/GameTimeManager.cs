using UnityEngine;

namespace _Scripts
{
    public class GameTimeManager : MonoBehaviour
    {
        private int _speedInd;
        private int _speedBeforePause;
        private int _minSpeed;
        private int _maxSpeed;
        private int _tickDelta;

        private GameTimeDate _currentTime;
        private float _timeSinceLastUpdate;

        private IDayNightView _iDayNightView;
        private IInputManager _iInputManager;
        private IView _iView;
        
        public static GameTimeManager Instance => _instance;
        private static GameTimeManager _instance;

        public void Init(IDayNightView iDayNightView, IInputManager iInputManager, IView iView)
        {
            _iDayNightView = iDayNightView;
            _iInputManager = iInputManager;
            _iView = iView;
        }

        private void Awake()
        {
            _instance = this;
        }
        
        private void Start()
        {
            StartSettingsInit();

            _iView.UpdateCurTime(_currentTime);
            OnInputSubscribe();
            SetTimeScale();

        }

        private void Update()
        {
            CurTimeUpdate(_tickDelta);
            _iInputManager.OnUpdate();
        }

        private void LateUpdate()
        {
            _iDayNightView.UpdatePositions();
        }

        private void OnDestroy()
        {
            if (!_instance == this)
                return;
            _instance = null;

            OnInputUnSubscribe();
        }
        
        private void StartSettingsInit()
        {
            _tickDelta = 1;
            _minSpeed = 1;
            _maxSpeed = Variables.UpdateIntervals.Length - 1;
            _speedInd = _speedBeforePause = 2;

            var sHour = 0;
            var sDay = 31;
            var sMonth = 12;
            var sYear = 2020;
            _currentTime = new GameTimeDate(sHour, sDay, sMonth, sYear);
        }

        private void CurTimeUpdate(int delta)
        {
            if (IsZeroSpeed())
                return;

            if (_timeSinceLastUpdate > delta)
            {
                _timeSinceLastUpdate -= delta;
                _currentTime.Change(delta);
                _iView.UpdateCurTime(_currentTime);
            }

            _timeSinceLastUpdate += Time.deltaTime;
        }

        private void OnSpeedUp()
        {
            if (IsZeroSpeed())
                return;

            SpeedUp();
            SetTimeScale();
        }

        private void OnSpeedDown()
        {
            if (IsZeroSpeed())
                return;

            SpeedDown();         
            SetTimeScale();
        }

        private void OnSpeedPause()
        {
            if (!IsZeroSpeed())
            {
                SetSpeedBeforePause(_speedInd);
                SetSpeedInd(0);
            }
            else
            {
                SetSpeedInd(_speedBeforePause);
                SetSpeedBeforePause(-1);
            }
            SetTimeScale();
        }
        
        private void SetTimeScale()
        {
            if (!IsZeroSpeed())
            {
                var len = Variables.UpdateIntervals.Length;
                var scale = Variables.UpdateIntervals[len - _speedInd];
                Time.timeScale = scale;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        
        private void SetSpeedBeforePause(int speed) => _speedBeforePause = speed;
        private void SetSpeedInd(int speed) => _speedInd = speed;
        private bool IsZeroSpeed() => _speedInd == 0;
        private void SpeedUp() => _speedInd = Mathf.Clamp(_speedInd + 1, _minSpeed, _maxSpeed);
        private void SpeedDown() => _speedInd = Mathf.Clamp(_speedInd - 1, _minSpeed, _maxSpeed);

        
        private void OnInputSubscribe()
        {
            _iInputManager.OnSpeedDown += OnSpeedDown;
            _iInputManager.OnSpeedUp += OnSpeedUp;
            _iInputManager.OnSpeedPause += OnSpeedPause;
        }
        private void OnInputUnSubscribe()
        {
            _iInputManager.OnSpeedDown -= OnSpeedDown;
            _iInputManager.OnSpeedUp -= OnSpeedUp;
            _iInputManager.OnSpeedPause -= OnSpeedPause;
        }
    }
}