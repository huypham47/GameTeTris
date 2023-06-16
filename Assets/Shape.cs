using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShapeScriptableObject", menuName ="Shape")]
public class Shape : ScriptableObject
{
    public List<Vector2> rc;
    public Color32 color;
    public int height;
    public int heightRotate;
}
