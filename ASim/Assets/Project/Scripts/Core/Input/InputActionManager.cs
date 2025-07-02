/// <summary>
/// Uygulama genelinde giriş (input) işlemlerini yöneten merkezi sınıftır.
/// Giriş sistemi başlatılır ve global olarak erişilebilir.
/// </summary>
public static class InputActionManager
{
    private static InputActionsMap _inputActionsMap;

    /// <summary>
    /// Global giriş haritasına erişim sağlar. 
    /// İlk çağrıldığında otomatik olarak başlatılır ve etkinleştirilir.
    /// </summary>
    public static InputActionsMap InputActionsMap
    {
        get
        {
            if (_inputActionsMap == null)
            {
                _inputActionsMap = new InputActionsMap();
                _inputActionsMap.Enable();
            }

            return _inputActionsMap;
        }
    }
}
