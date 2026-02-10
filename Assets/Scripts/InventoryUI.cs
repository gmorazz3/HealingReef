using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image AloeIcon;
    public Image ShellIcon;

    void Start()
    {
        // Fully hidden at start
        AloeIcon.enabled = false;
        ShellIcon.enabled = false; // <-- FIXED
    }

    public void SetAloe(bool hasAloe)
    {
        AloeIcon.enabled = hasAloe; // <-- also fixed
    }

    public void SetShell(bool hasItemTrail2)
    {
        ShellIcon.enabled = hasItemTrail2;
    }
}
