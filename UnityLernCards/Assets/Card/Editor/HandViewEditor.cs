using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HandView))]
public class HandViewEditor : Editor
{
    private HandView _target => target as HandView;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update view"))
        {
            _target.UpdateCardPositions();
        }
    }
}