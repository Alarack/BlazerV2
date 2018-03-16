using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AltarOutcome : MonoBehaviour {

	// Update is called once per frame
	public virtual void Outcome (Altar altar) {
        Debug.Log(this.GetType().ToString());
    }
}
