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
        public AttributeObjectType attributeObjectType;
    }

    public enum AttributeObjectType
    {
        Dropdown,
        Toggle,
        InputField,
    }

    public string ItemName;
    public Sprite ItemComponentIcon;
    public Sprite ItemMainSprite;
    public Sprite ItemSecondSprite;
    public Sprite ItemThirdSprite;
    public bool IsItemDetailsActive = true;
    public bool IsItemAttributesActive = true;

    [TextArea(10, 20)]
    public List<string> ItemDetais = new();
    public List<AttributeData> Attributes = new();
}