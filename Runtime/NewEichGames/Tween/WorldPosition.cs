using UnityEngine;

namespace NewEichGames.Tween
{
    public class WorldPosition : TweenAnimation
    {
        public Vector2 targetPos;
        public bool targetRelativeToStartPos = true;
        public bool localPosSpace;

        private Vector2 startPos, endPos;

        public override void Initialize()
        {
            base.Initialize();

            startPos = localPosSpace ? transform.localPosition : transform.position;

            endPos = targetPos;
            if (targetRelativeToStartPos)
                endPos = startPos + targetPos;
        }

        public override void UpdateAnimation(float t)
        {
            base.UpdateAnimation(t);

            Vector2 pos = Vector2.Lerp(startPos, endPos, t);

            if (localPosSpace)
                transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
            else
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}
