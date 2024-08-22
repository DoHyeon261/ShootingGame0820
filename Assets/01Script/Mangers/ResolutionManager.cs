using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ػ� ������
// �÷����ϴ� ����̽��� �ػ󵵿� �����Ҷ� ������ �� �ػ󵵰� ���� �ٸ���,
// ���͹ڽ��� ���͹ڽ��� ����� Ȱ���ؼ� ������ ������ ������ �ǵ���.
public class ResolutionManager : Singleton<ResolutionManager>
{
    private Camera mainCam;
    private Canvas canvas;
    private CanvasScaler canvasScalier;

    private Vector2 fixedAspectRatio = new Vector2(9, 16);

    protected override void DoAwake()
    {
        base.DoAwake();// �θ�Ŭ������ ������ �� �� ���� �Ŀ� ������ doAwake�� ȣ��.

        //�ʱ� ����
        ApplySetting();
    }
    private void ApplySetting()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
        if(canvas == null)
        {
            canvas = FindObjectOfType<Canvas> ();
        }

        if (canvasScalier == null)
            canvasScalier = FindObjectOfType<CanvasScaler>();

        if(mainCam != null)
        {
            SetCamerAspecRatio();
        }

        if (canvas != null && canvasScalier != null)
        {
            ConfigureCanvas();
        }
    }
    private void SetCamerAspecRatio()
    {
        Rect rt = mainCam.rect;

        float screenAspect = (float)(Screen.width / Screen.height);
        float targetAspect = fixedAspectRatio.x / fixedAspectRatio.y;

        if(screenAspect >= targetAspect)
        {
            float width = targetAspect / screenAspect;
            rt.width = (1f - width) * screenAspect;
            rt.height = 1f;
            rt.y = 0f;
            rt.x = (1f - width) / 2f;
        }
        else
        {
            float height = screenAspect / targetAspect;
            rt.width = 1f;
            rt.height = height;
            rt.x = 0f;
            rt.y = (1f - height) / 2f;
        }
        mainCam.rect = rt;
    }

    private void ConfigureCanvas()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCam;
        canvas.planeDistance = 1f;

        canvasScalier.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScalier.referenceResolution = new Vector2(1920, 1080);
        canvasScalier.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScalier.matchWidthOrHeight = 0.5f;
    }
}
