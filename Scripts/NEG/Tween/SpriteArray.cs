using UnityEngine;

namespace NEG.Tween
{
    public class SpriteArray : TweenAnimation
    {
        public Sprite[] animSprites;
        public int numRepeat = 1;

        private SpriteRenderer sr;

        public override void Initialize()
        {
            base.Initialize();

            sr = GetComponent<SpriteRenderer>();
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            if (t < 1)
                sr.sprite = animSprites[(int)(t * numRepeat * animSprites.Length) % animSprites.Length];
            else
                // To make sure that when animation is finished we end on last sprite in animation ... modulo arthimatic would give us first sprite when t = 1
                sr.sprite = animSprites[animSprites.Length - 1];
        }
    }
}