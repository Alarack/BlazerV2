using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour {

    public PlayerDetector myDetector;
    public int numOfUses;


    public virtual bool UseRestrictionsMet()
    {
        if (myDetector.playerDetected == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void ActivationFunction()
    {
        if (numOfUses > 0)
        {
            numOfUses -= 1;
        }
        if (numOfUses == -1)
        {
            Debug.Log("Level Object has Infinite Uses");
        }
        if (numOfUses == 0)
        {
            Destroy(this);
        }
    }
}
