using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public int id;
    public string name;

    public Sprite sprite;

    //Static, use for reference
    public float float1;

    //Dynamic, will be copied to ItemData
    public float dfloat1;

}

[System.Serializable]
public class ItemData
{
    public int amount;
    public List<float> float1;

    public void Add(ItemData other)
    {
        amount += other.amount;
        if (float1 != null && other.float1 != null)
        {
            float1.AddRange(other.float1);
        }
        other.amount = 0;
        other.float1 = null;
    }
}
