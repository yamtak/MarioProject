using UnityEngine;

public class Player : MonoBehaviour
{
    //GameManager�ƂȂ�
    [SerializeField] GameManager gameManager;
    //�v���C���[�̏�Ԃ�block�Ɛݒ�
    public LayerMask blockLayer;
    //currenttime���Ă���
    private float currentTime = 0f;
    //-------------------------------------------------------------------------------
    //private Animation anim;
    //public AnimationClip Run;
    //public AnimationClip Jump;
    //-------------------------------------------------------------------------------
    //�v���C���[�̓�������ԂƂ��đ�����
    public enum DIRECTION_TYPE 
    { 
        STOP,
        RIGHT,
        LEFT,    
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    //-------------------------------------------------------------------------------
    //����̏ꍇ�͔O���̂悤�ɏ�����
    //���A�G���[���o�Ă��܂����A�ǂ����p���̖��̂悤
    Rigidbody2D rigidbody2D;
    //Rb2d�Ǝ����l��
    Animator animator;

    //�ړ��l
    [SerializeField] private float speed;
    //�W�����v�l�c�c����������+�����Ă��ǂ������A����͈�񉟂�
    [SerializeField] private float jumpPower = 4000;
    //-------------------------------------------------------------------------------
    private void Start() 
    {
        //�J�n������d�͂𔭐��A�ݒ��1�ɂ��Ă��邯�ǁA�d�߂ɂ��ăW�����v�͂��������Ă��ǂ�����
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Animator�������Ă����
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


        //��������
        float x = Input.GetAxis("Horizontal");
        //����͂����ŗǂ��̂��s��
        currentTime += Time.deltaTime;

        //animator.SetFloat("Speed", x);

        //��~�̏ꍇ
        if (x == 0) 
        {
            direction = DIRECTION_TYPE.STOP;
        }

        //�E�ړ�
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
            transform.localScale = new Vector2(1, 1);
            animator.SetFloat("Speed", x);
        }
        //���ړ�
        else if (x < 0) 
        {
            direction = DIRECTION_TYPE.LEFT;
            transform.localScale = new Vector2(-1, 1);
            animator.SetFloat("Speed", -x);
        }
        //�n�ʂւ̐ݒu���X�y�[�X�����ADown���������u�ԂŗL��
        if (IsGround() && Input.GetKeyDown("space")) 
        {
            Jump();//Jump�̍��ڎQ��
        }

    }
    //-------------------------------------------------------------------------------
    private void FixedUpdate()//���܂����^�C�~���O�ōX�V�A�������͎�
    { 

        //��Ԃ��X�C�b�`������switch��
        switch (direction) 
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
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
    //-------------------------------------------------------------------------------
    private void Jump() //jump�������ɍX�V
    {
        //rigidbody2D�̑��x�ω���new�l�����i2���̑��x�Ay��velocity�j
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        animator.SetTrigger("Jumping");
    }
    //-------------------------------------------------------------------------------
    bool IsGround() //�ݒu�������
    { 
        //�ݒu����Ő���\��������@����������collider�ł��ǂ��̂��ȁH
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
        //�������̃p�^�[���H
    { 
        //����Trap�^�O�ɐG�ꂽ�ꍇ�AGameOver�֑J��
        if (collision.gameObject.tag == "Trap") 
        {
            gameManager.GameOver();
        }

        //����Clear�^�O�ɐG�ꂽ�ꍇ�AGameClear�֑J��
        if (collision.gameObject.tag == "Clear") 
        {
            gameManager.GameClear();
        }
    }
    //-------------------------------------------------------------------------------
}
