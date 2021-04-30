using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum DIRECTION_TYPE
    {
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.RIGHT;

    Rigidbody2D rigidbody2D;
    float speed;

    //-------------------------------------------------------------------------------
    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = DIRECTION_TYPE.RIGHT;
    }

    //-------------------------------------------------------------------------------
    private void FixedUpdate()//決まったタイミングで更新、多分入力時
    {

        switch (direction) {
            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector2(1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector2(-1, 1);
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
}
