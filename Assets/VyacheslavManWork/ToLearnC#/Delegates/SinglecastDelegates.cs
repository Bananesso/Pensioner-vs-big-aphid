using System.Net.Http.Headers;
using UnityEngine;

public class SinglecastDelegates : MonoBehaviour
{
    //���� singlecast ��������

    //�� ��������� ������
    public delegate void NoParameters(); //void ������, ��� ������� �������� ������� ������ �� ������, ������� ������ �� ���������� � ������ �� ���������, �.�. � ������� �����

    void Main()
    {
        NoParameters noPrm = new(ShowMessage);
        noPrm();
    }
    public void ShowMessage()
    {
        Debug.Log("While true learn");
    }

    //���������� ������
    private delegate string WithParameters(string x, string y); //� ������ x � y ����� ���������� �� ��� //string ������, ��� ������� ��������� �� �����, ��� ���������� string

    void Main2()
    {
        WithParameters withPrm = new(ShowMessage2);
        string value = withPrm("Knowledge", "is beautiful�");
        Debug.Log(value);
    }

    public string ShowMessage2(string alpha, string beta)
    {
        Debug.Log($"{alpha}, {beta} I NEED MORE KNOWLEDGE");
        return "LearningIsSoBeautiful!";
    }
}