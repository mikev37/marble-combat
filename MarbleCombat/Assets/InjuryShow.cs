using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryShow : MonoBehaviour
{
    SpriteRenderer sr;
    MarblePawn mr;
    public int THRESHOLD = 4;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mr = GetComponentInParent<MarblePawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mr.HP <= THRESHOLD) {
            sr.enabled = true;
        }
    }
}
