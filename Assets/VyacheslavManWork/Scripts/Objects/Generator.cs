using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Generator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int maxConnectedTargets = 3;
    [SerializeField] private float electrolyzeInterval = 1f;

    [Header("Префаб линии")]
    [SerializeField] private GameObject linePrefab;

    [Header("Техническое")]
    public List<ElectricTarget> connectedTargets = new List<ElectricTarget>();
    private Coroutine electrolyzeCoroutine;
    private Dictionary<ElectricTarget, LineRenderer> targetLines = new Dictionary<ElectricTarget, LineRenderer>();

    void Start()
    {
        StartCoroutine(ElectrolyzeTargetsRoutine());
    }

    IEnumerator ElectrolyzeTargetsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(electrolyzeInterval);
            ElectrolyzeAllTargets();
        }
    }

    void ElectrolyzeAllTargets()
    {
        foreach (var target in connectedTargets)
        {
            if (target != null)
            {
                var electrolyzed = target.GetComponent<ObjectElectrolyzed>();
                if (electrolyzed != null)
                {
                    electrolyzed.Interact(this.gameObject);
                }
            }
        }
    }

    public bool AddTarget(ElectricTarget target)
    {
        if (connectedTargets.Count >= maxConnectedTargets)
        {
            return false;
        }

        if (!connectedTargets.Contains(target))
        {
            connectedTargets.Add(target);
            CreateLineToTarget(target);
            return true;
        }
        return false;
    }

    public void RemoveTarget(ElectricTarget target)
    {
        if (connectedTargets.Remove(target))
        {
            RemoveLineToTarget(target);
        }
    }

    private void CreateLineToTarget(ElectricTarget target)
    {
        if (linePrefab == null)
        {
            Debug.LogError("Не назначен префаб линии!");
            return;
        }

        GameObject lineObj = Instantiate(linePrefab, this.transform);
        lineObj.name = "LineTo_" + target.name;

        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("В префабе нет LineRenderer!");
            Destroy(lineObj);
            return;
        }

        lineRenderer.positionCount = 2;
        targetLines[target] = lineRenderer;
    }

    private void RemoveLineToTarget(ElectricTarget target)
    {
        if (targetLines.TryGetValue(target, out LineRenderer line))
        {
            if (line != null)
            {
                Destroy(line.gameObject);
            }
            targetLines.Remove(target);
        }
    }
}