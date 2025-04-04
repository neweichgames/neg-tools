using UnityEngine;
using UnityEngine.Events;

namespace NEG.Tween
{
    public class Event : TweenAnimation
    {
        [Range(0, 1)]
        public float eventPos;
        public UnityEvent evnt;
        private bool calledEvent;

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);
            
            // TODO: Add implementation for reverse event calls and re inits
            if (t > eventPos && calledEvent == false)
            {
                evnt.Invoke();
                calledEvent = true;
            }
        }

        public override void CompletedAnim()
        {
            base.CompletedAnim();

            calledEvent = false;
        }
    }
}