using UnityEngine;

public class ColonistAI : MonoBehaviour
{
    /// <summary>
    /// Enum�^�Ő錾�����R���j�X�g�̏��
    /// </summary>
    public enum ColonistState
    {
        Idle,
        Move,
        Mine,
        Sleep
    }
    public float MoveSpeed = 2.0f;
    private Vector3 targetPosition = new Vector3(2, 0, 2);

    public ColonistState State;
    /// <summary>
    /// �R���j�X�g�̏�Ԃ�ύX���邽�߂̃^�C�}�[
    /// [SerializeField]�̂悤�Ȃ��̂𑮐�(Attribute)�Ƃ���
    /// </summary>
    [SerializeField]
    private float timer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���j�X�g�̏�Ԃ�Idle����͂��߂�
        State = ColonistState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        //1�t���[���ɂ����������Ԃ�Timer���猸�Z���Ă���
        timer -= Time.deltaTime;
        //���������̒��̕ϐ����g���ď����𕪊�(switch)������
        switch (State)
        {
            case ColonistState.Idle://�ҋ@
                //�����^�C�}�[���O�b�����������
                if (timer <= 0f)
                {
                    //�R���j�X�g�̏�Ԃ𓮂��Ƃ�����ԂɕύX
                    State = ColonistState.Move;
                    //�^�[�Q�b�g�̃|�W�V���������߂Ă�����
                    targetPosition = new Vector3(
           Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    timer = 2f;
                }
                break;
            case ColonistState.Move://�ړ�
                transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                //if���́A���������ʓ��̏�����������A�����ʓ��̏������s��
                //�����̈ʒu�ƃ^�[�Q�b�g�̈ʒu��10�p���߂��Ȃ�����
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    //���̍s�����s��
                    State = ColonistState.Mine;
                    //�@�펞�Ԃ�1�b����5�b�܂ł̃����_��
                    timer = Random.Range(1f, 5f);
                  

                }
                break;
            case ColonistState.Mine://���@
                //���ō̌@�A�j���[�V�����Đ��̑���Ƀ��O���o��
                Debug.Log("Colonist is mining!");
                //���t���[����]����������
                transform.Rotate(Vector3.up * 30f * Time.deltaTime);
                if(timer <= 0f)
                {
                    
                    State = ColonistState.Sleep;
                    //timer��10�b����15�b�ɐݒ�
                    timer = Random.Range(10f,15f);
                    //state��ColonistState.Sleep�ɂ���
                    //timer10�b


                }
                break;
            case ColonistState.Sleep://�A�Q
                if (timer <= 0f)
                {
                    State = ColonistState.Idle;
                    timer = Random.Range(1f,5f);


                }

                break;
        }
    }
}
