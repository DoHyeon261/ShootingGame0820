using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 해상도 관리자
// 플레이하는 디바이스의 해상도와 제작할때 기준이 된 해상도가 서로 다를때,
// 레터박스와 세터박스의 기법을 활용해서 제작한 비율이 유지가 되도록.
public class ResolutionManager : Singleton<ResolutionManager>
{
    private Camera mainCam;
    private Canvas canvas;
    private CanvasScaler canvasScalier;

    private Vector2 fixedAspectRatio = new Vector2(9, 16);

    protected override void DoAwake()
    {
        base.DoAwake();// 부모클래스의 내용을 한 번 실행 후에 본인의 doAwake를 호출.

        //초기 설정
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
