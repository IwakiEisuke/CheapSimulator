// ������A�N�V�����̃N���X
using UnityEngine;

public class RunAwayAction : Action
{
    public RunAwayAction()
    {
        Name = "Run Away";
        //PreConditions["health"] = 0.0f;  // ���N��Ԃ�0�ȏ�ł���Ύ��s�\
        //Effects["safe"] = 1.0f;          // �A�N�V�������s��A���S�ɂȂ�
        Cost = 1.0f;                     // �A�N�V�����̃R�X�g
    }

    public override bool Perform(GameObject agent)
    {
        // �����郍�W�b�N������
        Debug.Log("Running away!");
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}