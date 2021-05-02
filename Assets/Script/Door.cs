using UnityEngine;
using UnityEngine.Playables;


public class Door : MonoBehaviour
{
    public GameObject Player; //プレイヤーを設定
    public GameObject DoorOut;//移動先のドアを設定
    [SerializeField] private PlayableDirector MoveTimeline;
    private Vector2 playerPosition;//プレイヤーのポジションを取得する変数
    Animator animator;
    Rigidbody2D rigidbody2DDoor;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");//プレイヤーの位置情報
        animator = GetComponent<Animator>();
        rigidbody2DDoor = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//当たり判定のvoid
    {
        if (collision.gameObject.tag == "Player") //ドアとプレイヤーが当たった時
        {
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Player.gameObject.transform.position = DoorOut.transform.position;//プレイヤーの位置に遷移先のドアが代入される
            animator.SetTrigger("Open");
            MoveTimeline.Play();
        }
    }
}
