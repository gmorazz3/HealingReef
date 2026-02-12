using UnityEngine;

public class PortalTester : MonoBehaviour
{
    public PortalWhirlpool portal;

    void Update()
    {
        // Press P to activate portal
        if (Input.GetKeyDown(KeyCode.P))
        {
            portal.ActivatePortal();
        }
    }
}
