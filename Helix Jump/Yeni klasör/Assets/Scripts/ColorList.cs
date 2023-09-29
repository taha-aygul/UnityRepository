using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =" Color", menuName ="ScriptableObjects/ColorTheme", order =1)]
[System.Serializable]
public class ColorList : ScriptableObject
{
    public Color ball, normalPiece, redPiece, cylinder, backGround;
}
