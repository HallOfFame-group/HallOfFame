using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateData : MonoBehaviour {

    HitContrller hitController;

    private void Awake()
    {
        hitController = transform.root.GetComponent<HitContrller>();
    }

    public void updateTriggered()
    {
        hitController.attackTriggered = false;
    }

    public void clearHits()
    {
        hitController.hits = 0;
    }
}
