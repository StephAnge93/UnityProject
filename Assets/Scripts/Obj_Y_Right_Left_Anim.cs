using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Y_Right_Left_Anim : MonoBehaviour
{
    public bool Stop = false;
    bool _up;
    float y_offset;
    float start_y;
    float end_y;
    // Start is called before the first frame update
    void Start()
    {
        _up = true;
        y_offset = 1;
        start_y = transform.position.y;

        StartCoroutine(Forward_Anim());
    }

    IEnumerator Forward_Anim()
    {
        end_y = start_y + y_offset;
        while (!Stop)
        {
            if (transform.position.x < end_y)
            {
                yield return null;
                transform.position += new Vector3(0, 0.01f, 0);
            }
            else
            {
                _up = false;
                StartCoroutine(Backward_Anim());
                break;
            }
        }
    }

    IEnumerator Backward_Anim()
    {
        end_y = start_y - y_offset;
        while (!Stop)
        {
            if (transform.position.x > end_y)
            {
                yield return null;
                transform.position -= new Vector3(0, 0.01f, 0);
            }
            else
            {
                _up = true;
                StartCoroutine(Forward_Anim());
                break;
            }
        }
    }
}
