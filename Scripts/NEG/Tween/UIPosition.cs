using UnityEngine;

namespace NEG.Tween
{
    public class UIPosition : TweenAnimation
    {
        public Vector2 targetPos;
        public bool worldPosition;

        private RectTransform rt;
        private Vector2 startPos, endPos;

        public override void Initialize()
        {
            base.Initialize();
            if (rt == null)
                rt = GetComponent<RectTransform>();

            startPos = rt.anchoredPosition;
            if (worldPosition)
                endPos = targetPos;
            else
                endPos = startPos + targetPos;
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            rt.anchoredPosition = Vector2.LerpUnclamped(startPos, endPos, t);
        }
    }
}
