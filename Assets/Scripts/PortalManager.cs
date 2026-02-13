using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [Header("Patients")]
    public PatientDialogue patient1;
    public PatientDialogue patient2;

    [Header("Portal")]
    public PortalWhirlpool whirlpool;

    private bool portalActivated = false;

    void Update()
    {
        if (portalActivated) return;

        if (patient1 != null && patient2 != null)
        {
            if (patient1.IsHealed && patient2.IsHealed)
            {
                whirlpool.ActivatePortal();
                portalActivated = true; // prevent multiple activations
            }
        }
    }
}
