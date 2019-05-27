using PNN.Utils.Attributes;
using UnityEditor;
using UnityEngine;

namespace PNN.Utils.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(NamedArrayAttribute))]
    public class NamedArrayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual NamedArrayAttribute Attribute
        {
            get { return (NamedArrayAttribute)attribute; }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object obj = EditorHelper.GetTargetObjectOfProperty(property);

            EditorGUI.PropertyField(position, property, new GUIContent(obj.ToString(), label.tooltip), true);
        }
    }
}