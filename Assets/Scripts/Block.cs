using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockStructure block;
}
[System.Serializable]
public class BlockStructure
{
    public Vector3 position;
    public Type type;

    public BlockStructure(Vector3 position, Type type)
    {
        this.position = position;
        this.type = type;
    }

    public override bool Equals(object obj)
    {
        if (obj is BlockStructure other)
        {
            return position == other.position && type == other.type;
        }
        return false;
    }
}

public enum Type
{
    cube, cilinder, pyramid
}