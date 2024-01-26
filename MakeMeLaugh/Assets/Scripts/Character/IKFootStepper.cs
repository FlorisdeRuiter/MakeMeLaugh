using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootStepper : MonoBehaviour
{
    [SerializeField] private Transform leftCastPoint, rightCastPoint, leftFoot, rightFoot;
    [SerializeField] private float stepTriggerDistance, stepSpeed, stepHeight;
    private Coroutine rightStepRoutine, leftStepRoutine;

    private void Update()
    {
        //determine if feet are too far away
        //if outside radius, step foot towards new location
    }

    private void CheckFoot(Transform castPoint, Transform target)
    {
        //check distance
        
    }

    private IEnumerator StepRoutine(Transform target, Vector3 targetPoint, Coroutine routine)
    {
        Vector3 startPoint = target.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / stepSpeed;
            target.position = GetStepInterpolation(startPoint, targetPoint, t);

            yield return null;
        }

        target.position = GetStepInterpolation(startPoint, targetPoint, 1);
        routine = null;
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
