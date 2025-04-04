using UnityEngine;

namespace NewEichGames.Tween
{
    public abstract class TweenAnimation : MonoBehaviour
    {
        public bool reinitializeOnPlay;

        private bool initialized;

        public virtual void AnimationStart()
        {
            if (initialized == false || reinitializeOnPlay)
            {
                initialized = true;
                Initialize();
            }
        }

        public virtual void Initialize() { }

        public virtual void UpdateAnimation(float t) { }

        public virtual void CompletedAnim() { }
    }
}
