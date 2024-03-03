using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisable : MonoBehaviour
{
    public Behaviour comp;

    float update;
    // Update is called once per frame
    void Update()
    {
        if(update <= 0) {
            comp.enabled = !comp.enabled;
            update = Random.value/2 + .2f;
        } else { update -= Time.deltaTime; }
    }
}
