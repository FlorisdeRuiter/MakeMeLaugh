using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootStepper : MonoBehaviour
{
    [SerializeField] private Transform castPoint, foot;
    [SerializeField] private float stepTriggerDistance, stepSpeed, stepHeight;
    [SerializeField] private LayerMask floorMask;
    private Coroutine stepRoutine;

    private void FixedUpdate()
    {
        if (stepRoutine == null)
         CheckFoot();
    }

    private void CheckFoot()
    {
        Vector3 target;
        RaycastHit hit;
        if (!Physics.Raycast(castPoint.position, Vector3.down, out hit, Mathf.Infinity, floorMask))
            return;

        target = hit.point;

        if (Vector3.Distance(foot.position, target) > stepTriggerDistance)
        {
            stepRoutine = StartCoroutine(StepRoutine(target));
            return;
        }

        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, floorMask);
        foot.position = hit.point;
    }

    private IEnumerator StepRoutine(Vector3 targetPoint)
    {
        Vector3 startPoint = foot.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / stepSpeed;
            foot.position = GetStepInterpolation(startPoint, targetPoint, t);

            yield return null;
        }

        foot.position = GetStepInterpolation(startPoint, targetPoint, 1);
        transform.position = foot.position;
        stepRoutine = null;
    }

    private Vector3 GetStepInterpolation(Vector3 start, Vector3 end, float percent)
    {
        Vector3 result = new Vector3();
        result.x = LeanTween.easeInOutQuart(start.x, end.x, percent);
        result.z = LeanTween.easeInOutQuart(start.z, end.z, percent);
        result.y = Mathf.Sin(percent * Mathf.PI) * stepHeight;
        return result;
    }
}
