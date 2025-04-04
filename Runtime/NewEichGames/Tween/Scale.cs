using UnityEngine;

namespace NewEichGames.Tween
{
    public class Scale : TweenAnimation
    {
        public Vector2 targetScale = Vector2.one;

        private Vector2 startScale;

        public override void Initialize()
        {
            base.Initialize();

            startScale = transform.localScale;
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            Vector2 scale = Vector2.Lerp(startScale, targetScale, t);
            transform.localScale = new Vector3(scale.x, scale.y, 1f);
        }
    }
}