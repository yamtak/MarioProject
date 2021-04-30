using UnityEngine;
using UnityEngine.Playables;


public class Door : MonoBehaviour
{
    public GameObject Player; //プレイヤーを設定
    public GameObject DoorOut;//移動先のドアを設定
    [SerializeField] private PlayableDirector MoveTimeline;

    private Vector2 playerPosition;//プレイヤーのポジションを取得する変数

    void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");//プレイヤーの位置情報
    }

    private void OnTriggerEnter2D(Collider2D collision)//当たり判定のvoid
    {
        if (collision.gameObject.tag == "Player") //ドアとプレイヤーが当たった時
        {
            Player.gameObject.transform.position = DoorOut.transform.position;//プレイヤーの位置に遷移先のドアが代入される
            MoveTimeline.Play();
        } 
    }
}