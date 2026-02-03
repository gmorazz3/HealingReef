using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image AloeIcon;
    public Image ItemTrail2Icon;

    void Start()
    {
        // Fully hidden at start
        AloeIcon.enabled = false;
        ItemTrail2Icon.enabled = false; // <-- FIXED
    }

    public void SetAloe(bool hasAloe)
    {
        AloeIcon.enabled = hasAloe; // <-- also fixed
    }

    public void SetItemTrail2(bool hasItemTrail2)
    {
        ItemTrail2Icon.enabled = hasItemTrail2;
    }
}
