using System.Collections;
using UnityEngine;

namespace NEG.Tween {
    public class TweenAnimator : MonoBehaviour
    {
        /// <summary>
        /// List of animations to animate.
        /// </summary>
        public TweenAnimation[] animations;
        public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float animationLength = 1f;
        public bool repeat;
        public bool playOnEnable = true;
        public float delayTime = 0f;

        [Header("Update mode")]
        public bool fixedUpdate;
        public bool unscaledTime;

        [Header("On End")]
        public TweenAnimator[] activateAnimationsOnEnd;

        [HideInInspector]
        public bool reverseOnEnd;
        [HideInInspector]
        public float reverseOnEndDelay = 0f;
        [HideInInspector]
        public bool deactivateOnEnd;
        [HideInInspector]
        public bool deactivateOnReverseOnly;

        private int dir;
        private bool playing;

        void OnEnable()
        {
            if (animations.Length == 0)
                animations = GetComponents<TweenAnimation>();

            if (playOnEnable && playing == false)
                Play();
        }

        public void Play(float overrideDelayTime = 0f)
        {   
            PlayAnimation(0f, overrideDelayTime > 0f ? overrideDelayTime : delayTime, 1);
        }

        public void PlayReverse(float overrideDelayTime = 0f)
        {
            if (gameObject.activeSelf == false)
                return;

            PlayAnimation(0f, overrideDelayTime > 0f ? overrideDelayTime : 0f, -1);
        }

        public void PlayAtPosition(float pos)
        {
            PlayAnimation(pos, 0f, 1);
        }

        void PlayAnimation(float pos, float delay, int dir)
        {
            if (playing)
                return;

            playing = true;

            this.dir = dir;
            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);

            StartCoroutine(Animate(pos, delay));
        }

        void StopAnimation()
        {
            playing = false;

            //End anmation ... unless reverse play is called
            if (reverseOnEnd == false || dir == -1)
            {
                for (int i = 0; i < activateAnimationsOnEnd.Length; i++)
                    activateAnimationsOnEnd[i].Play();

                if (deactivateOnEnd)
                    if (deactivateOnReverseOnly == false || dir == -1)
                        gameObject.SetActive(false);
            }
        }

        IEnumerator Animate(float t, float delay) 
        {   
            if (delay > 0f)
                yield return AnimationDelay(delay);

            //Animation Start
            for (int i = 0; i < animations.Length; i++)
                animations[i].AnimationStart();

            //Update Animations
            yield return AnimationUpdate(t);

            //Finalize Animations
            UpdateAnimations(1f);
            CompleteAnimations();

            StopAnimation();

            // Check for playing reverse
            if (dir == 1 && reverseOnEnd)
            {
                yield return AnimationDelay(reverseOnEndDelay);
                PlayReverse();
            }
        }

        IEnumerator AnimationUpdate(float t)
        {
            while (t < 1f || repeat)
            {
                if (unscaledTime == false)
                    t += Time.deltaTime / animationLength;
                else
                    t += Time.unscaledDeltaTime / animationLength;

                if (repeat && t > 1f)
                {
                    CompleteAnimations();
                    t = Mathf.Repeat(t, 1f);
                }

                UpdateAnimations(t);

                if (fixedUpdate)
                    yield return new WaitForFixedUpdate();
                else
                    yield return null;
            }
        }

        IEnumerator AnimationDelay(float delay)
        {
            if (unscaledTime)
                yield return new WaitForSecondsRealtime(delayTime);
            else
                yield return new WaitForSeconds(delayTime);
        }

        public void SetAnimPosHard(float pos)
        {
            if (playing)
                return;

            UpdateAnimations(pos);
        }

        void UpdateAnimations(float t)
        {
            for (int i = 0; i < animations.Length; i++)
            {
                animations[i].UpdateAnimation(GetValue(t));
            }
        }

        void CompleteAnimations()
        {
            foreach (TweenAnimation anim in animations)
                anim.CompletedAnim();
        }

        float GetValue(float t)
        {
            return animCurve.Evaluate(dir == 1 ? t : (1 - t));
        }

        public bool IsPlaying()
        {
            return playing;
        }

        public void SetDeactive(bool e)
        {
            deactivateOnEnd = e;
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        void OnDisable()
        {
            playing = false;
        }
    }
}