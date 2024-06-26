using System;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.TimerSystem.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timerText;
        
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            InvokeRepeating(nameof(ReduceTime), 1f, 1f);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
             _signalBus.Subscribe<StopTimerSignal>(OnStopTimer);
             _signalBus.Subscribe<ResumeTimerSignal>(OnResumeTimer);
        }
        
        private void OnStopTimer()
        {
            CancelInvoke(nameof(ReduceTime));
        }
        
        private void OnResumeTimer()
        {
            InvokeRepeating(nameof(ReduceTime), 1f, 1f);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<StopTimerSignal>(OnStopTimer);
            _signalBus.Unsubscribe<ResumeTimerSignal>(OnResumeTimer);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void ReduceTime()
        {
            var time = timerText.text;
            var minute = int.Parse(time[0].ToString());
            var secondFirstIndex = int.Parse(time[2].ToString());
            var secondSecondIndex = int.Parse(time[3].ToString());
            
            if(secondFirstIndex == 0 && secondSecondIndex == 0)
            {
                if(minute == 0)
                {
                    CancelInvoke(nameof(ReduceTime));
                    // TODO: Game Over
                    return;
                }
                
                minute--;
                secondFirstIndex = 5;
                secondSecondIndex = 9;
            }
            else
            {
                if(secondSecondIndex == 0)
                {
                    secondFirstIndex--;
                    secondSecondIndex = 9;
                }
                else
                {
                    secondSecondIndex--;
                }
            }
            
            timerText.text = minute + "." + secondFirstIndex + secondSecondIndex;
        }
    }
}