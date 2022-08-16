using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlug : MonoBehaviour
{
    //기능들
    private List<BasePlugAble> plugs;
    //우선권이 필요한 기능들
    private List<BasePlugAble> overridePlugs;

    //현재 기능 코드들
    private int currentPlugs;
    //기본 기능 코드들
    private int defaultPlugs;
    //잠긴 기능 코드들
    private int plugsLocked;

    //캐싱
    public Transform playerCamera;
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private ObitCamera cameraScript;

    private float h;
    private float v;

    public float lerpTurn = 0.05f;
    private bool flagChangeFOV;
    public float runFOV = 100;
    private Vector3 dirLast;
    private bool flagRun;
    private float hFloat;
    private float vFloat;
    private bool flagOnGround;
    private Vector3 colliderGround;

    public float getHorizontal { get => h; } 
    public float getVertical { get => v; }
    public ObitCamera getCameraScript { get => cameraScript; }
    public Rigidbody getRigidbody { get => playerRigidbody; }
    public Animator getAnimator { get => playerAnimator; }

    //지금 어떤 기본 플러그가 꽃혀있는가?
    public int getDefaultPlug { get => defaultPlugs; }

    private void Awake()
    {
        plugs = new List<BasePlugAble>();
        overridePlugs = new List<BasePlugAble>();
        playerAnimator = GetComponent<Animator>();
        cameraScript = playerCamera.GetComponent<ObitCamera>();
        playerRigidbody = GetComponent<Rigidbody>();
        colliderGround = GetComponent<Collider>().bounds.extents;

        hFloat = 0.0f;
        vFloat = 0.0f;
        flagOnGround = true;
    }
    public bool getFlagMoving()
    {
        //return (h != 0f) || (v != 0f);
        return Mathf.Abs(h) > Mathf.Epsilon || Mathf.Abs(v) > Mathf.Epsilon;
    }

    public bool getFlagHorizontalMoving()
    {
        return Mathf.Abs(h) > Mathf.Epsilon;
    }

    public bool getFlagRun()
    {
        foreach(BasePlugAble basePlugAble in plugs)
        {
            //if(!basePlugAble.flagAllowRun)
            {
                return false;
            }
        }

        foreach (BasePlugAble overPlugAble in overridePlugs)
        {
            //if(!overridePlugs.flagAllowRun)
            {
                return false;
            }
        }
        return false;
    }


    public bool getFlagReadyRunning()
    {
        return flagRun && getFlagMoving() && getFlagRun();
    }

    public bool getFlagGrounded()
    {
        Ray ray = new Ray(playerTransform.position + Vector3.up * 2 * colliderGround.x, Vector3.down);
        return Physics.SphereCast(ray, colliderGround.x, colliderGround.x + 0.2f);
    }
}


public abstract class BasePlugAble : MonoBehaviour
{
    protected float spdFloat;
    protected int plugsCode;
    protected ControllerPlug controllerPlug;
    protected bool getFlagRun;

    private void Awake()
    {
        this.controllerPlug = GetComponent<ControllerPlug>();
        getFlagRun = true;
        plugsCode = this.GetType().GetHashCode();
    }

    public int GetPlugsCode { get => plugsCode; }
    public virtual void childLateUpdate() { }
    public virtual void childFixedUpdate() { }
    public virtual void OnOverride() { }

}