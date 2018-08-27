using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour {
    public static CinematicCamera cinematicCameraSingleton;
   
    public List<CinematicCameraSwap> cameraAnchors = new List<CinematicCameraSwap>();
    Camera cam;
    Animator anim;
    Transform cameraTrans;
    float curScrollVelocity;
    float point;
    bool zooming;
    public float camZoomMin = 15;
    public float camZoomMax = 80;

    public float scrollRateMultiplier= 0.5f;
    void Awake()
    {
        cinematicCameraSingleton = this;

    }
    void Start()
    {
        cameraTrans = this.transform;
        anim = GetComponent<Animator>();
        cam = cameraTrans.GetComponent<Camera>();
    }

    void FixedUpdate()
    {

        float smoother = 0;
        float scr = Input.GetAxis("Mouse ScrollWheel");
        curScrollVelocity = Mathf.SmoothStep(smoother, scr, Time.deltaTime * scrollRateMultiplier);

        cam.fieldOfView += curScrollVelocity;
        /*
        if (!zooming)
        {
            StopCoroutine("ApplyZoom");
            StartCoroutine("ApplyZoom", p);


        }*/
    }
  /* IEnumerator ApplyZoom(float p)
    {
        while(cam.fieldOfView != p)
        {
            zooming = true;
         
            float cZ = Mathf.SmoothStep(cam.fieldOfView, p, Time.deltaTime * scrollReturnRate);
            cam.fieldOfView = cZ;
            

            yield return new WaitForSeconds(0.02f);

        }

        yield return new WaitForSeconds(0.02f);

        zooming = false;

    }*/
    public void SwitchToAnchor(int anchor)
    {
        if (cameraTrans != null)
        {
            cameraTrans.parent = cameraAnchors[anchor].point;
            cameraTrans.position = cameraAnchors[anchor].point.position;
            cameraTrans.rotation = cameraAnchors[anchor].point.rotation;
        }
        anim.CrossFade(cameraAnchors[anchor].crossfade, 0.2f);
    }


}
[System.Serializable]
public struct CinematicCameraSwap
{
    public Transform point;
    public string crossfade;

}