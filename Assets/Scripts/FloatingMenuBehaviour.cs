using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMenuBehaviour : MonoBehaviour
{
    [SerializeField] FloatingMenuType menuType;
    [SerializeField] Transform player;
    [SerializeField] float forwardDistance;
    [SerializeField] float horizontalDistance;
    [SerializeField] float verticalDistance;
    [SerializeField] float disablingDistance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = player.position + forwardDistance * player.forward;
        transform.rotation = player.rotation;

        switch (menuType)
        {
            case FloatingMenuType.Coin:
                transform.position += verticalDistance * player.up;
                transform.position -= horizontalDistance * player.right;
                break;
            case FloatingMenuType.Info:
                transform.position += verticalDistance * player.up;
                transform.position += horizontalDistance * player.right;
                GetComponent<AudioSource>().clip = null;
                break;
            case FloatingMenuType.Object:
                transform.position -= verticalDistance * player.up;
                transform.position -= horizontalDistance * player.right;
                break;
            case FloatingMenuType.Route:
                transform.position -= verticalDistance * player.up;
                transform.position += horizontalDistance * player.right;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && (player.position - transform.position).magnitude > disablingDistance)
        {
            gameObject.SetActive(false);
        }
    }
}
