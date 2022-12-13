using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardView))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update state"))
        {
            CardView card = target as CardView;
            card.UpdateAll();
        }
    }
}
