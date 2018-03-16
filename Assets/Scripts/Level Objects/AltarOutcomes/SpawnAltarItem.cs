using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAltarItem : AltarOutcome {

    public override void Outcome(Altar altar)
    {
        Debug.Log(this.GetType().ToString());
    }
}
