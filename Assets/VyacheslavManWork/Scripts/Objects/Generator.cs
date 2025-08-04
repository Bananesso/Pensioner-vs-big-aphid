using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Generator : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private int maxConnectedTargets = 3;
    [SerializeField] private float electrolyzeInterval = 1f;

    [Header("������ �����")]
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _wireStart;

    [Header("�����������")]
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
        if (_linePrefab == null)
        {
            Debug.LogError("�� �������� ������ �����!");
            return;
        }

        GameObject lineObj = Instantiate(_linePrefab, this.transform);
        lineObj.name = "LineTo_" + target.name;

        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("� ������� ��� LineRenderer!");
            Destroy(lineObj);
            return;
        }

        lineRenderer.positionCount = 2;

        if (_wireStart != null && target.WireEnd != null)
        {
            lineRenderer.SetPosition(0, _wireStart.transform.position);
            lineRenderer.SetPosition(1, target.WireEnd.transform.position);
        }

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