using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simülasyon panelinin yönetimini sağlar.
/// Oynatma, durdurma, şema temizleme ve bileşen menüsü kontrolü yapar.
/// </summary>
public class SimulationPanelManager : MonoBehaviour
{
    public enum SimulationState
    {
        Stopped,
        Running
    }

    [Header("UI Butonları")]
    public Button PlaySimButton;
    public Button StopSimButton;
    public Button ComponentsMenuButton;
    public Button ClearSchemaButton;

    [Header("Diğer Bileşenler")]
    public WarnPanelManager WarnPanel;
    public SchemaManager SchemaManager;
    public GameObject ComponentsMenuObject;

    /// <summary>
    /// Simülasyon durumu (durduruldu veya çalışıyor).
    /// </summary>
    public static SimulationState SimState { get; private set; } = SimulationState.Stopped;

    private void Start()
    {
        PlaySimButton.onClick.AddListener(OnPlaySimulationButtonClicked);
        StopSimButton.onClick.AddListener(OnStopSimulationButtonClicked);
        ComponentsMenuButton.onClick.AddListener(OnComponentsMenuButtonClicked);
        ClearSchemaButton.onClick.AddListener(OnClearSchemaButtonClicked);

        // Başlangıçta simülasyon durdurulmuş, buna göre buton ayarı:
        UpdateUIState();
    }

    /// <summary>
    /// Bileşenler menüsünü açar.
    /// </summary>
    private void OnComponentsMenuButtonClicked()
    {
        if (ComponentsMenuObject != null)
            ComponentsMenuObject.SetActive(true);
    }

    /// <summary>
    /// Şemayı temizleme işlemini başlatır, onay paneli açar.
    /// </summary>
    private void OnClearSchemaButtonClicked()
    {
        if (WarnPanel == null) return;

        WarnPanel.gameObject.SetActive(true);
        WarnPanel.SetWarnText("Tüm şema silinecektir. Devam etmek istiyor musunuz?");

        WarnPanel.OkButtonClicked += OnClearSchemaConfirmed;
        WarnPanel.CancelButtonClicked += OnClearSchemaCanceled;
    }

    private void OnClearSchemaConfirmed()
    {
        SchemaManager?.ClearAllSchema();

        DetachWarnPanelEvents();
        HideWarnPanel();
    }

    private void OnClearSchemaCanceled()
    {
        DetachWarnPanelEvents();
        HideWarnPanel();
    }

    /// <summary>
    /// WarnPanel buton event aboneliklerini kaldırır.
    /// </summary>
    private void DetachWarnPanelEvents()
    {
        WarnPanel.OkButtonClicked -= OnClearSchemaConfirmed;
        WarnPanel.CancelButtonClicked -= OnClearSchemaCanceled;
    }

    /// <summary>
    /// WarnPanel panelini gizler.
    /// </summary>
    private void HideWarnPanel()
    {
        if (WarnPanel != null)
            WarnPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Simülasyonu başlatır.
    /// </summary>
    private void OnPlaySimulationButtonClicked()
    {
        SimState = SimulationState.Running;
        UpdateUIState();
    }

    /// <summary>
    /// Simülasyonu durdurur.
    /// </summary>
    private void OnStopSimulationButtonClicked()
    {
        SimState = SimulationState.Stopped;
        UpdateUIState();
    }

    /// <summary>
    /// Butonların durumlarını simülasyon durumuna göre günceller.
    /// </summary>
    private void UpdateUIState()
    {
        bool isRunning = SimState == SimulationState.Running;

        PlaySimButton.interactable = !isRunning;
        StopSimButton.interactable = isRunning;

        ComponentsMenuButton.interactable = !isRunning;
        ClearSchemaButton.interactable = !isRunning;
    }
}
