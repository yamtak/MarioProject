using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;//GameManagerとつなぐ
    public LayerMask blockLayer;//プレイヤーの状態をblockと設定
    public enum DIRECTION_TYPE//プレイヤーの動きを状態として捉える
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    private float currentTime = 0f;//currenttimeしておく

    [SerializeField] AudioClip SE_Jump;
    [SerializeField] AudioClip SE_Damage;
    AudioSource audioSource;

    //-------------------------------------------------------------------------------
    //今回の場合は念仏のように唱えるが、エラーが出てしまった、どうやら継承の問題のよう
    Rigidbody2D rigidbody2DPlayer;
    [SerializeField] float speed;
    [SerializeField] private float jumpPower = 4000;//ジャンプ値……押し続けて+させても良いかも、今回は一回押し

    Animator animator;//Rb2dと似た様相
    //-------------------------------------------------------------------------------
    private void Start()
    {
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();//開始したら重力を発生、設定上1にしてあるけど、重めにしてジャンプ力を強くしても良いかも
        animator = GetComponent<Animator>();//Animatorを持ってくるよ
        audioSource = GetComponent<AudioSource>();
    }
    //-------------------------------------------------------------------------------
    private void Update()
    {
        //ｘ軸操作
        float x = Input.GetAxis("Horizontal");
        currentTime += Time.deltaTime;//これはここで良いのか不明

        //停止の場合
        if (x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }

        //右移動
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("Speed", x);
        }
        //左移動
        else if (x < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("Speed", -x);
        }
        if (IsGround() && Input.GetKeyDown("space"))//地面への設置兼スペース押下、Downが押した瞬間で有効
        {
            Jump();//Jumpの項目参照
        }

    }
    //-------------------------------------------------------------------------------
    private void FixedUpdate()//決まったタイミングで更新、多分入力時
    {
        switch (direction)//状態をスイッチさせるswitch文
        {
            //switch(式)
            //case 定数
            //……実行処理;
            //break;
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                break;
        }
        //rigidbody2Dの速度変化はnew値を代入（2次の速度、yはvelocity）
        //……AddForceでも良いのかなと思ったけど横移動は質量を感じない方がヨサソウ
        rigidbody2DPlayer.velocity = new Vector2(speed, rigidbody2DPlayer.velocity.y);
    }
    //-------------------------------------------------------------------------------
    private void Jump() //jumpした時に更新
    {
        rigidbody2DPlayer.AddForce(Vector2.up * jumpPower);//rigidbody2Dの速度変化はnew値を代入（2次の速度、yはvelocity）
        animator.SetTrigger("Jumping");
        audioSource.PlayOneShot(SE_Jump);
    }
    //-------------------------------------------------------------------------------
    bool IsGround() //設置した状態
    {
        Vector3 leftStartPositon = transform.position - Vector3.right * 0.5f;
        Vector3 rightStartPositon = transform.position + Vector3.right * 0.5f;
        Vector3 endPositon = transform.position - Vector3.up * 0.2f;
        //Debug.DrawLine(leftStartPositon, endPositon);
        //Debug.DrawLine(rightStartPositon, endPositon);
        return Physics2D.Linecast(leftStartPositon, endPositon, blockLayer)
            || Physics2D.Linecast(rightStartPositon, endPositon, blockLayer);
        //||論理和（左辺かつ右辺）両方が接している状態
    }
    //-------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    //Unityの関数、物理でのみ使用
    //アタッチしたTriggerに別のオブジェクトが接した時に起動
    {
        if (collision.gameObject.tag == "Trap")//もしTrapタグに触れた場合、GameOverへ遷移
        {
            gameManager.GameOver();
        }

        if (collision.gameObject.tag == "Clear")//もしClearタグに触れた場合、GameClearへ遷移
        {
            gameManager.GameClear();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            gameManager.GameOver();
            audioSource.PlayOneShot(SE_Damage);
        }


    }
    //-------------------------------------------------------------------------------




    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "MoveFloor")
    //    {
    //        transform.SetParent(collision.transform);
    //    }
    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "MoveFloor")
    //    {
    //        transform.SetParent(null);
    //    }
    //}

}
