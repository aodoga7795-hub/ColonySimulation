using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistManager : MonoBehaviour
{
    /// <summary>
    /// []�͔z��Ƃ����A��̕ϐ��̒��ŕ�����ColonistAI���Ǘ��ł���
    /// </summary>
    public ColonistAI[] Colonists;

    

    // Update is called once per frame
    void Update()
    {
        //�L�[�{�[�h��Space�L�[�������ꂽ��
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //for���́i�����l�A�����l���w��̒l�ɂȂ�܂ŁA�����l�𑝌�������j�܂ł̉񐔏������s��
            //i�̓R���j�X�g�̐�
            for(int i = 0; i < Colonists.Length; i++)
            {
                Colonists[i].State = ColonistAI.ColonistState.Mine;
            }
        }
    }
}
