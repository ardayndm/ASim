using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentMenuManager : MonoBehaviour
{
    public static ComponentMenuManager Instance { get; private set; }

    public TMP_InputField SearchInputField;
    public Transform ComponentItemsContent;
    public Button CloseMenuButton, SelectItemButton;

    public Image InfoMainImage, InfoSecondImage, InfoThirdImage;
    public TextMeshProUGUI InfoComponentNameText;
    public GameObject InfoDetailsScrollViewObject, InfoAttributeScrollViewObject;
    public Transform InfoDetailsContent, InfoAttributesContent;

    public List<BaseMenuItem> MenuItems = new();


    private void Awake() => Instance = this;

    void Start()
    {

    }

}