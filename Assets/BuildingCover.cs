using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCover : MonoBehaviour
{
    public Player currentPlayer;
    private bool _inCover;

    private CanvasGroup coverGroup;
    private float coverAlpha;

    [SerializeField]
    private float coverFadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        coverGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inCover && coverGroup.alpha < 1)
        {
            coverGroup.alpha += Time.deltaTime * coverFadeSpeed;
        }

        if (_inCover && coverGroup.alpha > 0)
        {
            coverGroup.alpha -= Time.deltaTime * coverFadeSpeed;
            Debug.Log(coverGroup.alpha);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            currentPlayer = player;
            _inCover = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            currentPlayer = null;
            _inCover = false;
        }
    }
}
