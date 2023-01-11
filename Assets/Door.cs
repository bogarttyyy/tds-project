using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool isLocked;
    [SerializeField]
    private Transform doorObject;

    public InteractProgressBar progressBar;

    [SerializeField]
    private float unlockProgress;

    private bool _playerInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        unlockProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.TryGetComponent(out Player player))
        //{
        //    _playerInTrigger = true;
        //}

        if (!isLocked)
        {
            doorObject.gameObject.SetActive(false);
        }

        if (isLocked)
        {
            progressBar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isLocked & unlockProgress < 1)
        {
            unlockProgress += Time.deltaTime;
            progressBar.ProgressBar.fillAmount = unlockProgress;
        }
        else
        {
            doorObject.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);
            isLocked = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.TryGetComponent(out Player player))
        //{
        //    _playerInTrigger = false;

        //}
        unlockProgress = 0;
        progressBar.gameObject.SetActive(false);
        progressBar.ProgressBar.fillAmount = 0;

        doorObject.gameObject.SetActive(true);
    }
}
