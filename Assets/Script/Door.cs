using UnityEngine;
using UnityEngine.Playables;


public class Door : MonoBehaviour
{
    public GameObject Player; //�v���C���[��ݒ�
    public GameObject DoorOut;//�ړ���̃h�A��ݒ�
    [SerializeField] private PlayableDirector MoveTimeline;

    private Vector2 playerPosition;//�v���C���[�̃|�W�V�������擾����ϐ�

    void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");//�v���C���[�̈ʒu���
    }

    private void OnTriggerEnter2D(Collider2D collision)//�����蔻���void
    {
        if (collision.gameObject.tag == "Player") //�h�A�ƃv���C���[������������
        {
            Player.gameObject.transform.position = DoorOut.transform.position;//�v���C���[�̈ʒu�ɑJ�ڐ�̃h�A����������
            MoveTimeline.Play();
        } 
    }
}