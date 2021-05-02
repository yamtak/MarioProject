using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;//GameManager�ƂȂ�
    public LayerMask blockLayer;//�v���C���[�̏�Ԃ�block�Ɛݒ�
    public enum DIRECTION_TYPE//�v���C���[�̓�������ԂƂ��đ�����
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    private float currentTime = 0f;//currenttime���Ă���

    [SerializeField] AudioClip SE_Jump;
    [SerializeField] AudioClip SE_Damage;
    AudioSource audioSource;

    //-------------------------------------------------------------------------------
    //����̏ꍇ�͔O���̂悤�ɏ����邪�A�G���[���o�Ă��܂����A�ǂ����p���̖��̂悤
    Rigidbody2D rigidbody2DPlayer;
    [SerializeField] float speed;
    [SerializeField] private float jumpPower = 4000;//�W�����v�l�c�c����������+�����Ă��ǂ������A����͈�񉟂�

    Animator animator;//Rb2d�Ǝ����l��
    //-------------------------------------------------------------------------------
    private void Start()
    {
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();//�J�n������d�͂𔭐��A�ݒ��1�ɂ��Ă��邯�ǁA�d�߂ɂ��ăW�����v�͂��������Ă��ǂ�����
        animator = GetComponent<Animator>();//Animator�������Ă����
        audioSource = GetComponent<AudioSource>();
    }
    //-------------------------------------------------------------------------------
    private void Update()
    {
        //��������
        float x = Input.GetAxis("Horizontal");
        currentTime += Time.deltaTime;//����͂����ŗǂ��̂��s��

        //��~�̏ꍇ
        if (x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }

        //�E�ړ�
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("Speed", x);
        }
        //���ړ�
        else if (x < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("Speed", -x);
        }
        if (IsGround() && Input.GetKeyDown("space"))//�n�ʂւ̐ݒu���X�y�[�X�����ADown���������u�ԂŗL��
        {
            Jump();//Jump�̍��ڎQ��
        }

    }
    //-------------------------------------------------------------------------------
    private void FixedUpdate()//���܂����^�C�~���O�ōX�V�A�������͎�
    {
        switch (direction)//��Ԃ��X�C�b�`������switch��
        {
            //switch(��)
            //case �萔
            //�c�c���s����;
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
        //rigidbody2D�̑��x�ω���new�l�����i2���̑��x�Ay��velocity�j
        //�c�cAddForce�ł��ǂ��̂��ȂƎv�������ǉ��ړ��͎��ʂ������Ȃ��������T�\�E
        rigidbody2DPlayer.velocity = new Vector2(speed, rigidbody2DPlayer.velocity.y);
    }
    //-------------------------------------------------------------------------------
    private void Jump() //jump�������ɍX�V
    {
        rigidbody2DPlayer.AddForce(Vector2.up * jumpPower);//rigidbody2D�̑��x�ω���new�l�����i2���̑��x�Ay��velocity�j
        animator.SetTrigger("Jumping");
        audioSource.PlayOneShot(SE_Jump);
    }
    //-------------------------------------------------------------------------------
    bool IsGround() //�ݒu�������
    {
        Vector3 leftStartPositon = transform.position - Vector3.right * 0.5f;
        Vector3 rightStartPositon = transform.position + Vector3.right * 0.5f;
        Vector3 endPositon = transform.position - Vector3.up * 0.2f;
        //Debug.DrawLine(leftStartPositon, endPositon);
        //Debug.DrawLine(rightStartPositon, endPositon);
        return Physics2D.Linecast(leftStartPositon, endPositon, blockLayer)
            || Physics2D.Linecast(rightStartPositon, endPositon, blockLayer);
        //||�_���a�i���ӂ��E�Ӂj�������ڂ��Ă�����
    }
    //-------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    //Unity�̊֐��A�����ł̂ݎg�p
    //�A�^�b�`����Trigger�ɕʂ̃I�u�W�F�N�g���ڂ������ɋN��
    {
        if (collision.gameObject.tag == "Trap")//����Trap�^�O�ɐG�ꂽ�ꍇ�AGameOver�֑J��
        {
            gameManager.GameOver();
        }

        if (collision.gameObject.tag == "Clear")//����Clear�^�O�ɐG�ꂽ�ꍇ�AGameClear�֑J��
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
