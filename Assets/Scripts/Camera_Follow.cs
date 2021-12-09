using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    GameObject Bat;
    float po1;
    float po2;

    private void Awake()
    {
        float ratio = (float)Screen.height / Screen.width;
        float diff = ratio - 1.3333f;
        Camera.main.orthographicSize += Mathf.Clamp(diff * 0.5f / 0.8320f, 0, 0.5f);

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Find_Bat());
    }

    IEnumerator Find_Bat()
    {
        yield return new WaitUntil(() => FindObjectOfType<Bat_Controller>() != null);
        Bat = FindObjectOfType<Bat_Controller>().gameObject;

        po1 = Bat.transform.position.y;
        po2 = Bat.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Bat == null) return;
        po1 = Bat.transform.position.y;
        if (FindObjectOfType<Bat_Controller>()._can_bat_fly && po1 >= transform.position.y)
        {
            float diff = po1 - po2;

            Vector3 po = transform.position;
            po.y += diff;
            transform.position = po;
        }
        po2 = po1;
    }
}
