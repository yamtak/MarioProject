using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] GameManager gameManager;
    [SerializeField] LayerMask blockLayer;
    public enum DIRECTION_TYPE
    {
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction;

    Rigidbody2D rigidbody2DEnemy;
    float speed;

    //-------------------------------------------------------------------------------
    private void Start()
    {
        rigidbody2DEnemy = GetComponent<Rigidbody2D>();
        direction = DIRECTION_TYPE.RIGHT;
    }

    //-------------------------------------------------------------------------------
    private void Update()
    {
        if (!IsGround())
        {
            ChangeDirection();
        }
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    void ChangeDirection()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }



    //-------------------------------------------------------------------------------
    private void FixedUpdate()//決まったタイミングで更新、多分入力時
    {

        switch (direction)
        {
            case DIRECTION_TYPE.RIGHT:
                speed = 1;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -1;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody2DEnemy.velocity = new Vector2(speed, rigidbody2DEnemy.velocity.y);
    }
}
