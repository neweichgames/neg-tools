using UnityEngine;

namespace NewEichGames.Tween
{
    public class SpriteOpacity : TweenAnimation
    {
        public float targetOpacity;

        private SpriteRenderer sr;
        private float startOpacity;

        public override void Initialize()
        {
            base.Initialize();

            sr = GetComponent<SpriteRenderer>();
            startOpacity = sr.color.a;
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(startOpacity, targetOpacity, t));
        }
    }
}