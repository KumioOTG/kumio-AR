using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private CoinType coinType;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private List<Sprite> icons;

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
    }

    void Update()
    {
        if (gameManager.coins[(int)coinType] != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            icon.sprite = icons[0];
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            icon.sprite = icons[1];
        }
    }

    public void ReleaseCoin()
    {
        gameManager.ReleaseCoin(coinType);
    }
}
