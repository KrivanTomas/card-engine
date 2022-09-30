using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ClassicCardReference", menuName = "ClassicCardReference")]
public class ClassicCardReference : ScriptableObject
{
    public ClassicCardObject[] cards;
    public Object[] obj;
    public bool idk;
    private void OnValidate() {
        cards = (AssetDatabase.LoadAllAssetsAtPath("Assets/Cards/ClassicCards/ScriptableObjects/A") as ClassicCardObject[]);
        obj = AssetDatabase.LoadAllAssetsAtPath("Assets/Cards/ClassicCards/ScriptableObjects/A");
    }
}
