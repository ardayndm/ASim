using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menüde gösterilecek bir bileşenin verilerini tutar.
/// Prefab, simgeler, açıklamalar ve ilişkilendirilmiş özellikler gibi bilgiler içerir.
/// </summary>
[CreateAssetMenu(fileName = "MenuItemSO", menuName = "UI/Menu Item", order = 0)]
public class MenuItemSO : ScriptableObject
{
    [Header("Temel Bilgiler")]
    [Tooltip("Bu item'ın sahnede oluşturulacak prefab'ı")]
    public GameObject Prefab;

    [Tooltip("Menüde gösterilecek simge (küçük ikon)")]
    public Sprite Icon;

    [Tooltip("Bu item'a verilecek isim (UI gösterim için)")]
    public string Name;

    [Header("Detaylı Bilgi Görselleri")]
    [Tooltip("Ana bilgi resmi (örnek: elektrik bağlantısı)")]
    public Sprite MainInfoSprite;

    [Tooltip("İkincil bilgi resmi (örnek: yön bilgisi)")]
    public Sprite SecondInfoSprite;

    [Tooltip("Üçüncül bilgi resmi (örnek: enerji tüketimi)")]
    public Sprite ThirdInfoSprite;

    [Header("Açıklama ve Ek Özellikler")]
    [Tooltip("Item ile ilgili bilgi satırları (tooltips vs.)")]
    [TextArea(10, 20)]
    public List<string> ItemInfoDescriptions = new();

    [Tooltip("Item sahneye eklendiğinde oluşturulacak attribute prefab'ları")]
    public List<GameObject> AttributePrefabs = new();
}
