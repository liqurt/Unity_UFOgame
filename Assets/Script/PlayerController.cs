using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int count;
    public Text WinText;
    public Text CountText;
    public float speed;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        WinText.text = "";
        count = 0;
        SetCountText();
        if (Application.platform == RuntimePlatform.Android)
        {
            CountText.fontSize = 30;
            WinText.fontSize = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }

    void FixedUpdate()
    {
        /* PC 스탠드얼론 */
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }

        /* Android */
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.acceleration.x;
            dir.y = Input.acceleration.y;
            if (dir.sqrMagnitude > 1)
                dir.Normalize();
            dir *= Time.deltaTime;
            transform.Translate(dir * speed);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        CountText.text = "Count : " + count.ToString();
        if(count >= 8)
        {
            WinText.text = "You win!";
        }
    }
}
