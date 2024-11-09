using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "ScriptableObjects/Structure", order = 1)]
public class Structure : ScriptableObject
{
    public BlockStructure[] blocks;
}
