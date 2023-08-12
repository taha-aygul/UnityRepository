using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }
    public Tile tile { get; set; }
    public bool empty => tile == null; // e�er tile null ise true
    public bool occupied => tile != null; // e�er tile null de�il ise true

}
