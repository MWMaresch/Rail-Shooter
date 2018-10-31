using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpawnData))]
public class SpawnDataDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var objRect = new Rect(position.x, position.y, 151, 15);
        var timeRect = new Rect(position.x + 155, position.y, 45, 15);
        var posRect = new Rect(position.x, position.y + 16, 200, 500);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(objRect, property.FindPropertyRelative("gameObj"), GUIContent.none);
        EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("spawnTime"), GUIContent.none);
        EditorGUI.PropertyField(posRect, property.FindPropertyRelative("position"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 2;
    }

}