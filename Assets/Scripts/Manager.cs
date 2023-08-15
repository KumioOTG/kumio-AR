using CodeMonkey.Utils;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType
{
    AVREA,
    SAECLA,
    GERIT,
    QUI,
    PORTAM,
    CONSTRVIT,
    AURO
}



public enum FloatingMenuType
{
    Coin,
    Info,
    Object,
    Route
}

public class Manager : MonoBehaviour
{
    public List<CoinBehaviour> coins;
    public List<AudioClip> listenedNarrations;
    public List<CollectibleObjectBehaviour> collectedObjects;

    private AudioClip narrationToAddToListened;

    void Start()
    {
        listenedNarrations = new List<AudioClip>();
        collectedObjects = new List<CollectibleObjectBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {


        foreach (GameObject indexFingerTip in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (indexFingerTip != null && indexFingerTip.name == "IndexTip Proxy Transform")
            {
                SphereCollider collider = indexFingerTip.GetComponent<SphereCollider>();
                if (collider == null)
                {
                    indexFingerTip.tag = "PlayerHand";
                    Rigidbody rigidbody = indexFingerTip.AddComponent<Rigidbody>() as Rigidbody;
                    rigidbody.useGravity = false;
                    collider = indexFingerTip.AddComponent<SphereCollider>() as SphereCollider;
                    collider.radius = 1f;
                    collider.isTrigger = true;
                }
            }
        }
    }

    public void CheckAndAddToListenedNarrations(AudioClip narration)
    {
        if (!listenedNarrations.Contains(narration))
        {
            narrationToAddToListened = narration;
            FunctionTimer.Create(AddToListenedNarrations, narration.length, "AddToNarrationInventory");
        }
    }

    private void AddToListenedNarrations()
    {
        listenedNarrations.Add(narrationToAddToListened);
        narrationToAddToListened = null;
    }

    public void ReleaseCoin(CoinType coin)
    {
        if (coins[(int)coin] != null)
        {
            coins[(int)coin].Activate();
            coins[(int)coin] = null;
        }
    }

    public void CheckAndAddToCollectedObjects(CollectibleObjectBehaviour obj)
    {
        if (!collectedObjects.Contains(obj))
        {
            collectedObjects.Add(obj);
        }
    }

    public void ReleaseObject(CollectibleObjectBehaviour obj)
    {
        if(obj.gameObject.activeSelf != true)
        {
            obj.Activate();
        }
    }
}
