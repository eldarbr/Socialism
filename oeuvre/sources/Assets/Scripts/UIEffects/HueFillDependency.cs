using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HueFillDependency : MonoBehaviour
{
    [SerializeField] private Image _image;

    private static Color _tempColor = new Color();

    void Update()
    {
        _image.color = ColorFromHSV((int)Mathf.Lerp(0f, 128f, _image.fillAmount), 61f / 100f, 85f / 100f);
    }

    public static Color ColorFromHSV(float h, float s, float v, float a = 1)
    {
        // no saturation, we can return the value across the board (grayscale)
        if (s == 0)
            return new Color(v, v, v, a);

        // which chunk of the rainbow are we in?
        float sector = h / 60;

        // split across the decimal (ie 3.87 into 3 and 0.87)
        int i = (int)sector;
        float f = sector - i;

        float p = v * (1 - s);
        float q = v * (1 - s * f);
        float t = v * (1 - s * (1 - f));

        // build our rgb color
        //Color color = new Color(0, 0, 0, a);
       
        _tempColor.r = 0f;
        _tempColor.g = 0f;
        _tempColor.b = 0f;
        _tempColor.a = a;

        switch (i)
        {
            case 0:
                _tempColor.r = v;
                _tempColor.g = t;
                _tempColor.b = p;
                break;

            case 1:
                _tempColor.r = q;
                _tempColor.g = v;
                _tempColor.b = p;
                break;

            case 2:
                _tempColor.r = p;
                _tempColor.g = v;
                _tempColor.b = t;
                break;

            case 3:
                _tempColor.r = p;
                _tempColor.g = q;
                _tempColor.b = v;
                break;

            case 4:
                _tempColor.r = t;
                _tempColor.g = p;
                _tempColor.b = v;
                break;

            default:
                _tempColor.r = v;
                _tempColor.g = p;
                _tempColor.b = q;
                break;
        }

        return _tempColor;
    }

}
