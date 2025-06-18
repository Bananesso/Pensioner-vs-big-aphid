using UnityEngine;

public class MulticastDelegates : MonoBehaviour
{
    private delegate void NoParameters(); //пример использования multicast делегатов

    void Main()
    {
        NoParameters noPrm = new(ShowMessage1);

        noPrm += ShowMessage2;

        noPrm();

        noPrm -= ShowMessage1;

        noPrm();
    }

    private void ShowMessage1() => Debug.Log($"Учитесь, братья!");
    private void ShowMessage2() => Debug.Log($"Зачем жить, если не заниматься самосовершенствованием?");



    private delegate void NoParameters2(); //пример использования анонимных методов в делегатах

    void Main2()
    {
        NoParameters2 noPrm = delegate ()
        {
            Debug.Log($"Хочу научиться играть на виолончели и говорить на испанском. Зачем");
        };

        Test(noPrm);
    }

    private void Test(NoParameters2 noPrm) //метод, принимающий в качестве параметра тип делегата без параметров
    {
        noPrm();
    }
}
