
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public LayerMask blockLayer;
    public enum DIRECTION_TYPE 
    { 
        STOP,
        RIGHT,
        LEFT,    
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rigidbody2D;
    private float speed;
    private float jumpPower = 500;
    //private bool goJump = false;
    //private bool canJump = false;

    private void Start() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    
    }

    private void Update() 
    {
        float x = Input.GetAxis("Horizontal");

        if (x == 0) 
        {
            direction = DIRECTION_TYPE.STOP;
        }

        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
            transform.localScale = new Vector2(1, 1);
        }

        else if (x < 0) 
        {
            direction = DIRECTION_TYPE.LEFT;
            transform.localScale = new Vector2(-1, 1);
        }

        if (IsGround() && Input.GetKeyDown("space")) 
        {
            Jump();
        }

    }

    private void FixedUpdate()//決まったタイミングで更新
    { 
        switch (direction) 
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 5;
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -5;
                break;


        
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    
    }
    void Jump() 
    {
        rigidbody2D.AddForce(Vector2.up * jumpPower);
    }

    bool IsGround() 
    { 
        Vector3 leftStartPositon = transform.position - Vector3.right * 0.5f;
        Vector3 rightStartPositon = transform.position + Vector3.right * 0.5f;
        Vector3 endPositon = transform.position - Vector3.up * 0.2f;
        Debug.DrawLine(leftStartPositon, endPositon);
        Debug.DrawLine(rightStartPositon, endPositon);
        return Physics2D.Linecast(leftStartPositon, endPositon, blockLayer)
            || Physics2D.Linecast(rightStartPositon, endPositon, blockLayer);
    }
        
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.tag == "Trap") 
        {
            Debug.Log("ゲームオーバー");
            gameManager.GameOver();
        }

        if (collision.gameObject.tag == "Clear") 
        {
            Debug.Log("クリア");
            gameManager.GameClear();
        }
    }
}
