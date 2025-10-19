using UnityEngine;

public class Colonist : MonoBehaviour
{
    public string Name = "Taro";
    public float MoveSpeed = 2.0f;

    private Vector3 targetPosition = new Vector3(2,0,2);


    // �Q�[���̍ŏ���1�񂾂����s�����Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"{Name}���R���j�[�ɓ������܂���");
        SetRandomTarget();


    }

    // �Q�[���̎��s���A��Ɏ��s�����
    void Update()
    {
        //�ړ��������B�ړ�����̂ɕK�v�Ȃ͎̂����̈ʒu�ƁA�s���ׂ��ʒu�B
        //�����̈ʒu��TargetPosition�܂ňړ�������B
        //.��"��"�ڑ����ƍl����

        transform.position = Vector3.MoveTowards(
            transform.position,targetPosition,MoveSpeed*Time.deltaTime);

        //if���́A���������ʓ��̏�����������A�����ʓ��̏������s��
        //�����̈ʒu�ƃ^�[�Q�b�g�̈ʒu��10�p���߂��Ȃ�����
        if (Vector3.Distance(transform.position,targetPosition)<0.1f) {
            //���̃^�[�Q�b�g�̈ʒu�����߂�

            SetRandomTarget();

        }



    }

    //������ʒu�����߂鏈��������
    void SetRandomTarget()
    {
        targetPosition = new Vector3(
            Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

    }

}
