using System.Net.Http.Headers;
using UnityEngine;

public class SinglecastDelegates : MonoBehaviour
{
    //ниже singlecast делегаты

    //не принимает данные
    public delegate void NoParameters(); //void значит, что делегат способен хранить ссылки на методы, которые ничего не возвращают и ничего не принимают, т.к. в скобках пусто

    void Main()
    {
        NoParameters noPrm = new(ShowMessage);
        noPrm();
    }
    public void ShowMessage()
    {
        Debug.Log("While true learn");
    }

    //прринимает данные
    private delegate string WithParameters(string x, string y); //в методе x и y могут называться не так //string значит, что делегат ссылается на метод, что возвращает string

    void Main2()
    {
        WithParameters withPrm = new(ShowMessage2);
        string value = withPrm("Knowledge", "is beautifulб");
        Debug.Log(value);
    }

    public string ShowMessage2(string alpha, string beta)
    {
        Debug.Log($"{alpha}, {beta} I NEED MORE KNOWLEDGE");
        return "LearningIsSoBeautiful!";
    }
}