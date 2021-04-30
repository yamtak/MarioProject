using UnityEngine;

public class Player : MonoBehaviour
{
    //GameManagerとつなぐ
    [SerializeField] GameManager gameManager;
    //プレイヤーの状態をblockと設定
    public LayerMask blockLayer;
    //currenttimeしておく
    private float currentTime = 0f;
    //-------------------------------------------------------------------------------
    //private Animation anim;
    //public AnimationClip Run;
    //public AnimationClip Jump;
    //-------------------------------------------------------------------------------
    //プレイヤーの動きを状態として捉える
    public enum DIRECTION_TYPE 
    { 
        STOP,
        RIGHT,
        LEFT,    
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    //-------------------------------------------------------------------------------
    //今回の場合は念仏のように唱える
    //が、エラーが出てしまった、どうやら継承の問題のよう
    Rigidbody2D rigidbody2D;
    //Rb2dと似た様相
    Animator animator;

    //移動値
    [SerializeField] private float speed;
    //ジャンプ値……押し続けて+させても良いかも、今回は一回押し
    [SerializeField] private float jumpPower = 4000;
    //-------------------------------------------------------------------------------
    private void Start() 
    {
        //開始したら重力を発生、設定上1にしてあるけど、重めにしてジャンプ力を強くしても良いかも
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Animatorを持ってくるよ
        animator = GetComponent<Animator>();
    }
    //-------------------------------------------------------------------------------
    private void Update() 
    {
        //{
        //    if (gameStatus == GAME_STATUS.START) 
        //    {
        //        return;
        //    }
        //}


        //ｘ軸操作
        float x = Input.GetAxis("Horizontal");
        //これはここで良いのか不明
        currentTime += Time.deltaTime;

        //animator.SetFloat("Speed", x);

        //停止の場合
        if (x == 0) 
        {
            direction = DIRECTION_TYPE.STOP;
        }

        //右移動
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
            transform.localScale = new Vector2(1, 1);
            animator.SetFloat("Speed", x);
        }
        //左移動
        else if (x < 0) 
        {
            direction = DIRECTION_TYPE.LEFT;
            transform.localScale = new Vector2(-1, 1);
            animator.SetFloat("Speed", -x);
        }
        //地面への設置兼スペース押下、Downが押した瞬間で有効
        if (IsGround() && Input.GetKeyDown("space")) 
        {
            Jump();//Jumpの項目参照
        }

    }
    //-------------------------------------------------------------------------------
    private void FixedUpdate()//決まったタイミングで更新、多分入力時
    { 

        //状態をスイッチさせるswitch文
        switch (direction) 
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
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
    //-------------------------------------------------------------------------------
    private void Jump() //jumpした時に更新
    {
        //rigidbody2Dの速度変化はnew値を代入（2次の速度、yはvelocity）
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        animator.SetTrigger("Jumping");
    }
    //-------------------------------------------------------------------------------
    bool IsGround() //設置した状態
    { 
        //設置判定で線を表示する方法があったがcolliderでも良いのかな？
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
        //扉もこのパターン？
    { 
        //もしTrapタグに触れた場合、GameOverへ遷移
        if (collision.gameObject.tag == "Trap") 
        {
            gameManager.GameOver();
        }

        //もしClearタグに触れた場合、GameClearへ遷移
        if (collision.gameObject.tag == "Clear") 
        {
            gameManager.GameClear();
        }
    }
    //-------------------------------------------------------------------------------
}
