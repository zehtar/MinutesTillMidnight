using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public Animator menuPanelAnimator; // Drag сюда MenuPanel

    public bool showingWeapon = false;

    public void TogglePanels()
    {
        if (showingWeapon)
        {
            //menuPanelAnimator.SetTrigger("GunHide");
            menuPanelAnimator.SetTrigger("PlayerShow");
        }
        else
        {
            //menuPanelAnimator.SetTrigger("PlayerHide");
            menuPanelAnimator.SetTrigger("GunShow");
        }

        showingWeapon = !showingWeapon;
    }
}