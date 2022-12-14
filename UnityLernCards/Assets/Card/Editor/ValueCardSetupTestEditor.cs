using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ValueCardSetupTest))]
public class ValueCardSetupTestEditor : Editor
{
    private ValueCardSetupTest thisTarget => target as ValueCardSetupTest;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Randomize"))
        {
            thisTarget.RandomizeCardsValues();
        }
    }
}