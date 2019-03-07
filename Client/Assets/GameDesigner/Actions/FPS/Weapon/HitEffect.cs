using UnityEngine;
using System.Collections;

[System.Serializable]
public class HitEffect
{
    public string tag = "Untagged";
    public GameObject effect;
    public float destroyTime = 1;

    public HitEffect(string tag = "Untagged")
    {
        this.tag = tag;
    }
}
