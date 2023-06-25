using UnityEngine;
using UnityEditor;

namespace Gilzoide.RoundedCorners.Editor
{
    [CustomPropertyDrawer(typeof(RoundedCorner))]
    public class RoundedCornersPropertyDrawer : PropertyDrawer
    {
        public static readonly GUIContent[] _subLabels = new GUIContent[]
        {
            new GUIContent("Radius"),
            new GUIContent("Triangles"),
        };
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty firstProperty = property.Copy();
            firstProperty.Next(true);
            EditorGUI.MultiPropertyField(position, _subLabels, firstProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float lineHeight = EditorGUIUtility.singleLineHeight;
            if (EditorGUIUtility.wideMode)
            {
                return lineHeight;
            }
            else
            {
                return lineHeight + EditorGUIUtility.standardVerticalSpacing + lineHeight;
            }
        }
    }
}
