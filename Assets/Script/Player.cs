using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float jump = 500f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(direction*120, 0);
        rb.AddForce(force);

        if (Input.GetKeyDown(KeyCode.Space))//ジャンプ思いつかず
        {
            transform.localPosition = new Vector2(
                transform.localPosition.x,
                transform.localPosition.y + jump);//アニメで飛ばす

        }
    }
}
