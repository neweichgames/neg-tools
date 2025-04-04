using UnityEditor;

#if UNITY_EDITOR
namespace NEG.Tween
{
    [CustomEditor(typeof(TweenAnimator))]
    public class TweenAnimatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TweenAnimator anim = (TweenAnimator)target;

            anim.reverseOnEnd = EditorGUILayout.Toggle("Reverse on End", anim.reverseOnEnd);
            if (anim.reverseOnEnd)
            {
                anim.reverseOnEndDelay = EditorGUILayout.FloatField("Reverse on End Delay", anim.reverseOnEndDelay);
            }

            anim.deactivateOnEnd = EditorGUILayout.Toggle("Deactivate on End", anim.deactivateOnEnd);
            if (anim.deactivateOnEnd)
            {
                anim.deactivateOnReverseOnly = EditorGUILayout.Toggle("Deactivate on Reverse Only", anim.deactivateOnReverseOnly);
            }
        }
    }
}
#endif
