using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TerrainEffect")]
public class TerrainEffect : ScriptableObject {

    public string terrEffName;
    public Effect touchEffect;
    public float duration;
    public AnimationClip effAnim;
    public LayerMask affectsWhat;
    private TerrainTile affectedTile;



    public TerrainEffect(string _terrEffName, Effect _touchEffect, float _duration, AnimationClip _effAnim, LayerMask _affectsWhat, TerrainTile _affectedTile)
    {
        terrEffName = _terrEffName;
        touchEffect = _touchEffect;
        duration = _duration;
        effAnim = _effAnim;
        affectsWhat = _affectsWhat;
        affectedTile = _affectedTile;
    }
}
