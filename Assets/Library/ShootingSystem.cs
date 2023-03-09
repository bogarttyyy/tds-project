using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    public Firearm gun;

    public PlayerControls playerControls;
    private InputAction fire;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire_performed;
    }

    private void OnDisable()
    {
        fire.Disable();
        fire.performed -= Fire_performed;
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        if (gun != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Transform projectileTransform = Instantiate(gun.firearmData.projectilePrefab, gun.endpoint.position, Quaternion.identity);

            Vector3 shootDir = (mousePos - gun.endpoint.position);
            shootDir.z = 0;
            projectileTransform.GetComponent<Projectile>().Setup(shootDir.normalized, gun.firearmData.projectileSpeed);
        }
        else
        {
            Debug.Log("No Gun to Fire!!");
        }
    }

    public void EquipGun(Component component, object gun)
    {
        Transform gunObject = (Transform)gun;
        this.gun = gunObject.GetComponent<Firearm>();
    }
}
