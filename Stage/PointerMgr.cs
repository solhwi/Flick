using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerMgr : MonoBehaviour
{
    public Line[] tiles;
    public bool isMouseDown = false;
    public bool isDragged = false;
    
    void Update()
    {
        MouseStateCheck();
    }

    public void MouseStateCheck()
    {
        if (Input.touchCount > 0)
        {    //터치가 1개 이상이면.
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch tempTouchs = Input.GetTouch(i);
                Ray touchedPos = Camera.main.ScreenPointToRay(tempTouchs.position);
                if (tempTouchs.phase == TouchPhase.Began) //해당 터치가 시작됐다면
                {
                    MouseDown(touchedPos);
                }
                else if (tempTouchs.phase == TouchPhase.Stationary || tempTouchs.phase == TouchPhase.Moved) //해당 터치가 홀딩중이라면
                {
                    MousePushing(touchedPos);
                }
                else if (tempTouchs.phase == TouchPhase.Ended)
                {
                    MouseUp(touchedPos);
                }
            }
        }
        //아래는 pc 디버깅용
        if (Input.GetMouseButtonDown(0)) //마우스를 누르면
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            MouseDown(ray);
        }
        else if (Input.GetMouseButton(0)) //마우스를 누르는 동안
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            MousePushing(ray);
        }
        else if (Input.GetMouseButtonUp(0)) //마우스를 때면
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            MouseUp(ray);
        }
    }

    public void MouseDown(Ray touch)
    {
        Ray ray = touch;
        int layerMask = 1 << LayerMask.NameToLayer("Line");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero, 1.0f, layerMask);
        //Layermask로 Line 레이어만 감지하게 변경, 이래야 Note에 가로막혀 터치가 무시되지 않음
        if (hit)
        {
            Debug.Log("HIT 까지");
            Debug.Log(hit.transform.name);
            var component = hit.transform.GetComponent<Line>();
            if (component != null)
            {
                component.ToggleClick();
                Debug.Log("CLICK 까지");
            }
        }
    }

    public void MousePushing(Ray touch)
    {
        Ray ray = touch;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Tile")
            {
                hit.transform.GetComponent<Line>().isMouseHold = true;
            }
        }
    }

    public void MouseUp(Ray touch)
    {
        //Ray ray = touch;
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);
        //if (hit)
        //{
        //    if (hit.transform.gameObject.tag == "Tile")
        //    {
        //        hit.transform.GetComponent<Line>().isMouseHold = false;
        //        hit.transform.GetComponent<Line>().isMouseClicked = false;
        //    }
        //}

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].isMouseHold = false;
        }
        //때면 모든 타일오브젝트들의 isMouseHold이 해제
    }
}
