using UnityEngine;

namespace NEG.Tween
{
    public class WorldRotation : TweenAnimation
    {
        public float targetRot;
        public bool worldRot;

        private float startRot, endRot;

        public override void Initialize()
        {
            base.Initialize();

            startRot = transform.eulerAngles.z;
            endRot = worldRot ? targetRot : startRot + targetRot;
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            float rot = Mathf.Lerp(startRot, endRot, t);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rot);
        }
    }
}