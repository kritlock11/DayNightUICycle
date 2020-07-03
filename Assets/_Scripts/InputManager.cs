using System;
using UnityEngine;

namespace _Scripts
{
    public interface IInputManager
    {
        void OnUpdate();

        event Action OnSpeedUp;
        event Action OnSpeedDown;
        event Action OnSpeedPause;
    }
    
    public class InputManager : IInputManager
    {
        public event Action OnSpeedUp;
        public event Action OnSpeedDown;
        public event Action OnSpeedPause;


        public void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Plus) || Input.GetKeyUp(KeyCode.KeypadPlus))
            {
                OnSpeedUp?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Minus) || Input.GetKeyUp(KeyCode.KeypadMinus))
            {
                OnSpeedDown?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                OnSpeedPause?.Invoke();
            }
        }
    }
}
