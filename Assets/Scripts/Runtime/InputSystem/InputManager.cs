using System;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private KeyCode _sendAnswerKeyCode;
        
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_sendAnswerKeyCode))
            {
                _signalBus.Fire(new SendAnswerButtonClickedSignal());
            }
        }
    }
}