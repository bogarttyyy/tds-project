using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerShootProjectile : MonoBehaviour
{
    public PlayerControls playerControls;
    private InputAction fire;

    [SerializeField]
    private Transform projectile;

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log($"Separated Fire Action: ({transform.position.x}, {transform.position.y})");
        Transform projectileTransform = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 shootDir = (mousePos - transform.position);
        shootDir.z = 0;
        Debug.Log(shootDir);
        projectileTransform.GetComponent<Projectile>().Setup(shootDir.normalized);
    }
}
