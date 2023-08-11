using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    public Action OnMove { set => onMove.Add(value); }
    public Action OnEndMove { set => onEndMove.Add(value); }

    List<Action> onMove = new List<Action>();

    List<Action> onEndMove = new List<Action>();

    bool moving = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0, speed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        else
        {
            if(moving) EndMove();
        }
    }

    void Move(Vector3 step)
    {
        transform.position += step;
        if(!moving) foreach(var action in onMove.ToArray())
        {
            onMove.Remove(action);
            action();
        }
        moving = true;
    }

    void EndMove()
    {
        moving = false;
        foreach (var action in onEndMove.ToArray())
        {
            onEndMove.Remove(action);
            action();
        }
    }
}
