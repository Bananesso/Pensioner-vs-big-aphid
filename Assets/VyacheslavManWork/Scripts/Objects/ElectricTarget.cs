using UnityEngine;

public class ElectricTarget : MonoBehaviour, IInteractable
{
    private Generator connectedGenerator;
    private bool isConnected;

    public void Interact(GameObject interactor)
    {
        ElectricianTool tool = interactor.GetComponent<ElectricianTool>();
        if (tool != null)
        {
            if (!tool.Scissors && !isConnected)
                ConnectToGenerator();

            else if (tool.Scissors && isConnected)
                DisconnectFromGenerator();
        }
    }

    private void ConnectToGenerator()
    {
        if (connectedGenerator == null)
            connectedGenerator = FindObjectOfType<Generator>();

        if (connectedGenerator != null && connectedGenerator.AddTarget(this))
        {
            isConnected = true;
        }
    }

    private void DisconnectFromGenerator()
    {
        if (connectedGenerator != null)
        {
            connectedGenerator.RemoveTarget(this);
            isConnected = false;
        }
    }
}