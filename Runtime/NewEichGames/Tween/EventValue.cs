using UnityEngine;
using UnityEngine.Events;

namespace NewEichGames.Tween
{
    public class EventValue : TweenAnimation
    {
        
        public float valueFrom = 0, valueTo = 1;
        public UnityEvent<float> evnt;

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);
            evnt.Invoke(Mathf.Lerp(valueFrom, valueTo, t));
           
        }
    }
}