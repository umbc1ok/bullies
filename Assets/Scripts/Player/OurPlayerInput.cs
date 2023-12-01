using UnityEngine;
using UnityEngine.InputSystem;

public class OurPlayerInput : MonoBehaviour
{
    [SerializeField] private Player player;

    public void Catch(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed)
            return;

        player.Catch();
    }

    public void UpdateSpeed(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed)
            return;

        player.Move(callbackContext.ReadValue<Vector2>());
    }
}
