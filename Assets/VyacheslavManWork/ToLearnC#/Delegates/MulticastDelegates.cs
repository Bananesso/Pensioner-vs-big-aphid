using UnityEngine;

public class MulticastDelegates : MonoBehaviour
{
    private delegate void NoParameters(); //������ ������������� multicast ���������

    void Main()
    {
        NoParameters noPrm = new(ShowMessage1);

        noPrm += ShowMessage2;

        noPrm();

        noPrm -= ShowMessage1;

        noPrm();
    }

    private void ShowMessage1() => Debug.Log($"�������, ������!");
    private void ShowMessage2() => Debug.Log($"����� ����, ���� �� ���������� ����������������������?");



    private delegate void NoParameters2(); //������ ������������� ��������� ������� � ���������

    void Main2()
    {
        NoParameters2 noPrm = delegate ()
        {
            Debug.Log($"���� ��������� ������ �� ���������� � �������� �� ���������. �����");
        };

        Test(noPrm);
    }

    private void Test(NoParameters2 noPrm) //�����, ����������� � �������� ��������� ��� �������� ��� ����������
    {
        noPrm();
    }
}
