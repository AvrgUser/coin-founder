using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] Walk walker;
    [SerializeField] GameObject body;
    [SerializeField] float height=2, speed = 10;
    bool jumping;

    void Start()
    {
        if (walker == null) walker = GetComponent<Walk>();
        if(walker!=null)
        {
            walker.OnMove = StartJump;
            walker.OnEndMove = EndJump;
        }
    }

    public void StartJump()
    {
        StartCoroutine(Jump());
        walker.OnMove = StartJump;
    }

    public void EndJump()
    {
        walker.OnEndMove = EndJump;
        jumping = false;
    }

    IEnumerator Jump()
    {
        Vector3 defaultPosition = body.transform.localPosition;
        float currentHeight = 0;
        bool up = true;
        jumping = true;
        while (jumping)
        {
            float sqrt = (height - currentHeight) / height;
            if (up)
            {
                currentHeight += Mathf.Sqrt(sqrt + 0.1f) * Time.deltaTime * speed + Time.deltaTime/10;
                if (currentHeight > height) up = false;
            }
            else
            {
                if (currentHeight < 0) up = true;
                currentHeight -= Mathf.Sqrt(sqrt + 0.1f) * Time.deltaTime * speed + Time.deltaTime/10;
            }
            body.transform.localPosition = defaultPosition+new Vector3(0, currentHeight, 0);
            yield return null;
        }
        body.transform.localPosition = defaultPosition;
    }
}
