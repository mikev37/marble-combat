using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centurion : MarblePawn
{
    public int SQUAD_MAX, FOLLOW_DISTANCE;
    public List<Transform> positions;
    List<MarblePawn> followers = new List<MarblePawn>();
    protected override void Charge(Transform t) {
        base.Charge(t);
        allies.RemoveAll(s => s == null);
        foreach (MarblePawn mp in followers) {
            mp.GetComponentInChildren<HeadSensor>().gameObject.SetActive(enabled);
            mp.GetComponentInChildren<RandomDisable>().GetComponent<CircleCollider2D>().enabled = true;
            mp.GetComponentInChildren<RandomDisable>().enabled = true;
        }
    }
    public override void Normal() {
        base.Normal();
        allies.RemoveAll(s => s == null);
        followers.RemoveAll(s => s == null);
        /*for (int go = 0; go < followers.Count; go++) {
            Transform to = positions[go];
            MarblePawn mp = followers[go];
            if(Vector2.Distance(mp.transform.position,to.transform.position) > 1) {
                mp.moveDirection = (to.transform.position - mp.transform.position).normalized;
            }
            mp.turnRotation = transform.forward;
            mp.GetComponentInChildren<RandomDisable>().GetComponent<CircleCollider2D>().enabled = false;
            mp.GetComponentInChildren<RandomDisable>().enabled = false;
        }*/
    }

    private void ReSortFollowers() {
        followers.Clear();
        List<MarblePawn> newFollowers = new List<MarblePawn>();
        newFollowers.AddRange(allies);
        for (int go = 0; go < SQUAD_MAX; go++) {
            Transform to = positions[go];
            newFollowers.Sort((x, y) => (int)Vector2.Distance(x.transform.position, to.transform.position) - (int)Vector2.Distance(y.transform.position, to.transform.position));
            MarblePawn mp = newFollowers[0];
            if (Vector2.Distance(mp.transform.position, transform.position) < FOLLOW_DISTANCE) {
                newFollowers.RemoveAt(0);
                followers.Add(mp);
            } else break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
        if (otherMB) {
            if (key != otherMB.key) {
                if (!enemies.Contains(otherMB))
                    enemies.Add(otherMB);
            } else {
                if (!allies.Contains(otherMB)) {
                    allies.Add(otherMB);
                    if (followers.Count < SQUAD_MAX && Vector2.Distance(otherMB.transform.position,transform.position) < FOLLOW_DISTANCE) {
                        ReSortFollowers();
                    }
                }
            }
        }
    }

}
