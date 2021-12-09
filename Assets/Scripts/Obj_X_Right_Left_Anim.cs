using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_X_Right_Left_Anim : MonoBehaviour
{
    public bool Stop = false;
    bool _forward;
    float x_offset;
    float start_x;
    float end_x;
    // Start is called before the first frame update
    void Start()
    {
       
        x_offset = 1;
        start_x = transform.position.x;

        if(Random.Range(0,2) == 0)
        {
            _forward = true; 
            StartCoroutine(Forward_Anim());
        }
        else
        {
            _forward = false;
            StartCoroutine(Backward_Anim());
        }
    }

    IEnumerator Forward_Anim()
    {
        end_x = start_x + x_offset;
        while (!Stop)
        {
            if (transform.position.x < end_x)
            {
                yield return null;
                transform.position += new Vector3(0.01f, 0, 0);
            }
            else
            {
                _forward = false;
                StartCoroutine(Backward_Anim());
                break;
            }
        }
    }

    IEnumerator Backward_Anim()
    {
        end_x = start_x - x_offset;
        while (!Stop)
        {
            if (transform.position.x > end_x)
            {
                yield return null;
                transform.position -= new Vector3(0.01f, 0, 0);
            }
            else
            {
                _forward = true;
                StartCoroutine(Forward_Anim());
                break;
            }
        }
    }
}
