using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public int Stage;
    public Vector2 LastMapPosition;
    public string CurrentID;
    public int Major;
    public int Minor;
    public HashSet<string> Inventory = new HashSet<string>();
}