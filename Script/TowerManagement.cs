using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject pistolTower;
    [SerializeField] private GameObject sniperTower;
    [SerializeField] private GameObject shotgunTower;

    private GameObject placingTower;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            setTower(pistolTower);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            setTower(shotgunTower);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            setTower(sniperTower);
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ClearSelected();
        }

        if (placingTower)
        {
            if(!placingTower.GetComponent<TowerPlacement>().isPlacing)
            {
                placingTower = null;
            }
        }
    }

    private void ClearSelected()
    {
        if (placingTower)
        {
            Destroy(placingTower);
            placingTower = null;
        }
    }
    public void setTower(GameObject tower)
    {
        ClearSelected();
        placingTower = Instantiate(tower);
    }
}


