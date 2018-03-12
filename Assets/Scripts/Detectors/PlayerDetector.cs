using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : Detector {

    public bool playerDetected;
    
	// Use this for initialization
	protected void Start () {
        detectedLayers.Add(GameManager.GetPlayer().gameObject.layer);
	}

    protected override void StayDetectorFunction(Collider2D collision)
    {
        base.StayDetectorFunction(collision);
        playerDetected = true;
    }
    protected override void ExitDetectorFunction(Collider2D collision)
    {
        base.ExitDetectorFunction(collision);
        playerDetected = false;
    }
}
