public class InputActionManager
{
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
    private static InputActionsMap _inputActionsMap;
}