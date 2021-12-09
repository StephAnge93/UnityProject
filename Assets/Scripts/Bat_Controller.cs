using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Controller : MonoBehaviour
{

    public GameObject Basket;
    public GameObject Ball_Prefab;
    List<Vector3> All_Positions = new List<Vector3>();
    GameObject Bat_Child;
    Rigidbody2D Rb;

    Vector3 Po1;
    Vector3 Po2;
    float Bat_Speed;

    //[HideInInspector]
    public bool _touch_down;
    //[HideInInspector]
    public bool _can_bat_fly;
    //[HideInInspector]
    public bool _game_over;
    public bool _level_complete;


    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Bat_Child = transform.GetChild(0).gameObject;
        Po1 = Po2 = Bat_Child.transform.position;
    }

    // Update is called once per frame
    int i;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ScreenCapture.CaptureScreenshot("123"+i+".png");
            i++;
        }


        if (_game_over || _level_complete) return;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
            {
                _touch_down = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && _touch_down && _can_bat_fly && !UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
            {
                _touch_down = false;
                _can_bat_fly = false;
                StartCoroutine(Basket_Follow());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
            {
                _touch_down = true;
            }
            else if (Input.GetMouseButtonUp(0) && _touch_down && _can_bat_fly  && !UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
            {
                _touch_down = false;
                _can_bat_fly = false;
                StartCoroutine(Basket_Follow());
            }
        }

        if (_touch_down && _can_bat_fly)
        {
            Bat_Fly();
        }
    }

    void Bat_Fly()
    {
        // Make Free From Basket
        //Game_Controller.Instance.Instruction_Panel.SetActive(false);
        Basket.transform.parent = null;
        if (!Basket.GetComponent<Rigidbody2D>())
        {
            Rb.bodyType = RigidbodyType2D.Dynamic;
            Basket.AddComponent<Rigidbody2D>().gravityScale = 0;
            Po1 = Po2 = Bat_Child.transform.position;
        }

        // Bat Rotation On Finger Position
        Vector3 target_po = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diff = target_po.x - transform.position.x;
        float angle = Mathf.Clamp(-diff * 30, -60, 60);
        Bat_Speed = Mathf.Clamp(Mathf.Abs(2 * angle / 40), 2, 4);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);

        // Bat Movement On Finger Position
        Rb.gravityScale = 0;
        transform.position += transform.up * Time.deltaTime * Bat_Speed;
        Ball_Generate();
    }

    void Ball_Generate()
    {
        Po2 = Bat_Child.transform.position;
        float distance = Mathf.Abs(Vector3.Distance(Po2, Po1));
        if (distance >= 0.075f)
        {
            Vector3 direction = (Po2 - Po1).normalized * 0.075f;
            Vector3 targetPosition = Po1 + direction;
            All_Positions.Add(targetPosition);

            Vector3 po = targetPosition;
            po.z += 0.2f;
            GameObject ball = Instantiate(Ball_Prefab, po, Quaternion.identity);
            Po1 = targetPosition;
        }
    }

    IEnumerator Basket_Follow()
    {
        // Move Basket On Ball Path
        int i = 0;
        int offset = Mathf.CeilToInt(Vector3.Distance(transform.position, Basket.transform.position)) + 2;
        while (i < All_Positions.Count)
        {
            Vector3 po = All_Positions[i];
            po.z = Basket.transform.position.z;

            while (Vector3.Distance(po, Basket.transform.position) != 0)
            {
                Vector3 vectorToTarget = po - Basket.transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                Basket.transform.rotation = Quaternion.Slerp(Basket.transform.rotation, q, Time.deltaTime * 20);
                Basket.transform.position = Vector3.MoveTowards(Basket.transform.position, po, 1f);
                yield return null;
            }

            if (i == All_Positions.Count - 1) break;
            i += offset;
            if (i >= All_Positions.Count) i = All_Positions.Count - 1;
        }

        // Basket Reach at Bat
        foreach(Ball_Controller ball in FindObjectsOfType<Ball_Controller>())
        {
            Destroy(ball.gameObject);
        }
        All_Positions.Clear();
        Basket.transform.SetParent(transform);
        Basket.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Basket.transform.localPosition = new Vector3(0, -0.3f, 0.1f);
        Destroy(Basket.GetComponent<Rigidbody2D>());

        if (transform.rotation.eulerAngles.z > 10f && transform.rotation.eulerAngles.z < 100f)
        {
            while (transform.rotation.eulerAngles.z > 10f && transform.rotation.eulerAngles.z < 100f)
            {
                yield return null;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 30);
            }
        }
        else
        {
            while (transform.rotation.eulerAngles.z > 200f && transform.rotation.eulerAngles.z < 350f)
            {
                yield return null;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 360, 0), Time.deltaTime * 30);
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_level_complete)
        {
            float y = transform.position.y + 8;
            while(transform.position.y < y)
            {
                Rb.gravityScale = 0;
                transform.position += new Vector3(0, 0.05f, 0);
                yield return null;
            }
        }
        else
        {
            Rb.gravityScale = 3;
        }

    }

    public int Fruits;
    public int Coins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_game_over || _level_complete) return;

        if (collision.tag == "fruit")
        {
            Sound_Controller.Instance.Play_Sound(1,Sound_Controller.Instance.Fruit_Clip);
            Destroy(collision.gameObject);
            Fruits += 1;
            PlayerPrefs.SetInt("Total_Fruits", PlayerPrefs.GetInt("Total_Fruits", 0) + 1);
            Game_Controller.Instance.Set_Fruit_Txt();
        }

        if (collision.tag == "coin")
        {
            Sound_Controller.Instance.Play_Sound(2,Sound_Controller.Instance.Coin_Clip);
            Destroy(collision.gameObject);
            Coins += 5;
            PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins", 0) + 5);
            Game_Controller.Instance.Set_Coin_Txt();
        }


        if (collision.tag == "obstecles")
        {
            Obstecles_Touch(collision);
        }
        else if (collision.tag == "wall")
        {

            if (collision.gameObject.GetComponent<Obj_X_Right_Left_Anim>() != null) collision.gameObject.GetComponent<Obj_X_Right_Left_Anim>().Stop = true;
            if (collision.gameObject.GetComponent<Obj_Y_Right_Left_Anim>() != null) collision.gameObject.GetComponent<Obj_Y_Right_Left_Anim>().Stop = true;

            if (collision.transform.GetChild(0).transform.position.y > transform.position.y && _can_bat_fly)
            {
                Sound_Controller.Instance.Play_Sound(1, Sound_Controller.Instance.Wall_Touch_Clip);
                _touch_down = false;
                _can_bat_fly = false;
                StartCoroutine(Shake_Camera());
                StartCoroutine(Basket_Follow());
            }
            else
            {
                Rb.bodyType = RigidbodyType2D.Static;
                _can_bat_fly = true;
            }
        }
        else if (collision.tag == "finish")
        {
            Sound_Controller.Instance.Play_Sound(1,Sound_Controller.Instance.Level_Complete_Clip);
            _level_complete = true;
            _touch_down = false;
            _can_bat_fly = false;
            StartCoroutine(Basket_Follow());
            Game_Controller.Instance.Open_Level_Complete();
        }
    }

    public void Obstecles_Touch(Collider2D collision)
    {
        if (_game_over) return;
        _game_over = true;
        _touch_down = false;
        _can_bat_fly = false;
        Sound_Controller.Instance.Play_Sound(1,Sound_Controller.Instance.Gameover_Clip);
        Game_Controller.Instance.Open_Level_Fail();
        StartCoroutine(Shake_Camera());

        Vector3 direction = transform.position - collision.gameObject.transform.position;
        direction.Normalize();
        Rb.gravityScale = 1;
        Rb.AddForce(direction * 500);
        Rb.AddTorque(50);
        if (!Basket.GetComponent<Rigidbody2D>())
        {
            Basket.AddComponent<Rigidbody2D>();
        }
        Basket.GetComponent<Rigidbody2D>().gravityScale = 1;
        Basket.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(1, 2)) * 300);
        Basket.GetComponent<Rigidbody2D>().AddTorque(50);
        foreach (Ball_Controller ball in FindObjectsOfType<Ball_Controller>())
        {
            ball.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            ball.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            ball.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(1, 2)) * 300);
            ball.gameObject.GetComponent<Rigidbody2D>().AddTorque(50);
        }
    }

    IEnumerator Shake_Camera()
    {
        Vector3 po = Camera.main.transform.position;
        yield return new WaitForEndOfFrame();
        Camera.main.transform.position = po + new Vector3(Random.Range(-0.05f, 0.05f), 0.2f, 0);
        yield return new WaitForEndOfFrame();
        Camera.main.transform.position = po;
    }

}
