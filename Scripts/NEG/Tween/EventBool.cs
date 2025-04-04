using UnityEngine;
using UnityEngine.Events;

namespace NEG.Tween
{
    /// <summary>
    /// Calls event every time it passes threshold with a boolean paramter representing if it is above threshold
    /// </summary>
    public class EventBool : TweenAnimation
    {
        [Range(0, 1)]
        public float eventPos;
        public bool invert;
        public UnityEvent<bool> evnt;
        private bool active;

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            if (t > eventPos == !active)
            {
                active = !active;
                evnt.Invoke(active ^ invert);
            }
        }
    }
}