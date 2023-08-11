using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject body;
    Coin taken;

    private void Start()
    {
        if(body == null) body = gameObject;
    }

    void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance = mouse - body.transform.position;
        float tan = distance.y / distance.x;
        body.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan(tan)/Mathf.PI*180+(distance.x>0?0:180));
        if(taken==null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.OverlapPoint((Vector2)mouse);

                if (hit != null)
                {
                    var coin = hit.GetComponentInParent<Coin>();
                    if (coin != null)
                    {
                        taken = coin;
                        coin.TakeUp();
                        print("got");
                    }
                }
            }
        }
        else
        {
            mouse.z = taken.transform.position.z;
            taken.transform.position = mouse;
            if (Input.GetMouseButtonUp(0))
            {
                taken.TakeDown();
                taken = null;
                print("put");
            }
            else if (Vector2.Distance(body.transform.position, taken.transform.position) < 1.5f)
            {
                taken.Collect();
                taken = null;
                print("collected");
            }
        }
    }
}
