using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Uyarı mesajı gösterip, OK/Cancel ile onay alan panel.
/// </summary>
public class WarnPanelManager : MonoBehaviour
{
    [Tooltip("Uyarı mesajının gösterileceği alan")]
    public TextMeshProUGUI WarnText;

    [SerializeField] private Button OkButton;
    [SerializeField] private Button CancelButton;

    public static WarnPanelManager Instance { get; private set; }

    /// <summary>
    /// Uyarı mesajının ok butonuna tıklandıgında tetiklenen event. (GameObject Disable olunca abonelikler silinir.)
    /// </summary>
    public event Action OkButtonClicked;

    /// <summary>
    /// Uyarı mesajının iptal butonuna tıklandıgında tetiklenen event. (GameObject Disable olunca abonelikler silinir.)
    /// </summary>
    public event Action CancelButtonClicked;

    void Awake() => Instance = this;

    private void Start()
    {
        CameraManager.LockCamera(gameObject, this);

        OkButton.onClick.AddListener(() =>
        {
            OkButtonClicked?.Invoke();
            CameraManager.UnlockCamera(gameObject, this);
        });

        CancelButton.onClick.AddListener(() =>
        {
            CancelButtonClicked?.Invoke();
            CameraManager.UnlockCamera(gameObject, this);
        });
    }

    void OnDisable()
    {
        // Abonelerini temizle
        OkButtonClicked = null;
        CancelButtonClicked = null;
    }

    /// <summary>
    /// Uyarı mesajını ayarlar.
    /// </summary>
    public void SetWarnText(string message)
    {
        if (WarnText != null)
            WarnText.text = message;
        else
            Debug.LogWarning("WarnText referansı atanmadı.");
    }
}
