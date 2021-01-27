using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ProceduralPopAnimations : MonoBehaviour
{
    static List<Transform> scaleInTransform = new List<Transform>();
    static List<Transform> positionInTransform = new List<Transform>();


    public static IEnumerator Pop(RectTransform target, float finalIndentHorizontal, float finalIndentVertical, AnimationCurve curve, float speed, System.Action end = null)
    {
        Vector2 tempVector = new Vector2();
        float t = 0f;
        float x = 0f;
        while (t <= 1f)
        {
            x = curve.Evaluate(t);

            tempVector.x = finalIndentHorizontal * x;
            tempVector.y = finalIndentVertical * x;
            target.offsetMin = tempVector;

            tempVector.x = -finalIndentHorizontal * x;
            tempVector.y = -finalIndentVertical * x;
            target.offsetMax = tempVector;


            t += Time.deltaTime * speed;
            yield return 0;
        }

        x = curve.Evaluate(1f);

        tempVector.x = finalIndentHorizontal * x;
        tempVector.y = finalIndentVertical * x;
        target.offsetMin = tempVector;

        tempVector.x = -finalIndentHorizontal * x;
        tempVector.y = -finalIndentVertical * x;
        target.offsetMax = tempVector;


        end?.Invoke();
    }


    public static IEnumerator ImageFade(Image target, AnimationCurve curve, float speed, System.Action end = null, float maximumTransparency=1f)
    {
        Color tempColor = target.color;
        float t = 0f;
        Time.timeScale = 1f;
        while (t <= 1f)
        {
            tempColor.a = curve.Evaluate(t) * maximumTransparency;
            target.color = tempColor;

            t += Time.deltaTime * speed;
            yield return 0;
        }

        tempColor.a = curve.Evaluate(1f) * maximumTransparency;
        target.color = tempColor;
        end?.Invoke();
    }

    public static IEnumerator LocalScaleTransform(Transform target, AnimationCurve curvex, AnimationCurve curvey, float speed, System.Action end = null)
    {
        if (scaleInTransform.Contains(target))
            yield break;
        else
            scaleInTransform.Add(target);
        float t = 0f;
        Time.timeScale = 1f;
        Vector3 tempScale = target.localScale;

        while (t <= 1f)
        {
            tempScale.x = curvex.Evaluate(t);
            tempScale.y = curvey.Evaluate(t);

            target.localScale = tempScale;

            t += Time.deltaTime * speed;
            yield return 0;
        }

        tempScale.x = curvex.Evaluate(1f);
        tempScale.y = curvey.Evaluate(1f);
        target.localScale = tempScale;
        scaleInTransform.Remove(target);
        end?.Invoke();
    }

    public static IEnumerator LocalPositionTransform(Transform target, AnimationCurve curvex, AnimationCurve curvey, float speed, System.Action end = null)
    {
        if (positionInTransform.Contains(target))
            yield break;
        else
            positionInTransform.Add(target);
        float t = 0f;
        Time.timeScale = 1f;
        Vector3 tempPos = target.localPosition;

        while (t <= 1f)
        {
            tempPos.x = curvex.Evaluate(t);
            tempPos.y = curvey.Evaluate(t);

            target.transform.localPosition = tempPos;

            t += Time.deltaTime * speed;
            yield return 0;
        }

        tempPos.x = curvex.Evaluate(1f);
        tempPos.y = curvey.Evaluate(1f);
        target.transform.localPosition = tempPos;
        positionInTransform.Remove(target);
        end?.Invoke();
    }
}
