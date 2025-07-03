using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseMenuItemSO : ScriptableObject
{
    [Serializable]
    public class AttributeData
    {
        public int id;
        public GameObject prefab;
    }

    public string ItemName;
    public Sprite ItemComponentIcon;
    public Sprite ItemMainSprite;
    public Sprite ItemSecondSprite;
    public Sprite ItemThirdSprite;

    [TextArea(10, 20)]
    public List<string> ItemDetais = new();
    public List<AttributeData> Attributes = new();
}