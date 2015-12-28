using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class ClickMenu : MonoBehaviour {
    private PlatformController _selected;

    public GameObject[] SelectionBlobs;

    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private List<GoalController> goals = new List<GoalController>();

    // Use this for initialization
    void Start () {
	    foreach (var blob in SelectionBlobs)
        {
            blob.SetActive(false);
        }

        foreach (var obj in GetMarbles())
        {
            originalPositions.Add(obj, obj.transform.position);
        }
        goals.AddRange(FindObjectsOfType<GoalController>());

        Time.timeScale = 0;
    }

    private IEnumerable<GameObject> GetMarbles()
    {
        return FindObjectsOfType<GameObject>().Where(g => g.GetComponent<Renderer>() != null && g.GetComponent<Renderer>().material.name.Contains("Marble"));
    }

    // Update is called once per frame
    void Update ()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(ray, out hitInfo);

        foreach (var blob in SelectionBlobs)
        {
            var pos = blob.transform.localPosition;
            if (hit && hitInfo.collider.gameObject == blob)
            {
                pos.z = -1.2f;
            }
            else
            {
                pos.z = -1f;
            }
            blob.transform.localPosition = pos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hit)
            {
                var platform = hitInfo.collider.GetComponent<PlatformController>();

                if (hitInfo.collider.name == "StartButton")
                {
                    hitInfo.collider.name = "StopButton";
                    hitInfo.collider.GetComponentInChildren<TextMesh>().text = "Stop";
                    Time.timeScale = 1;
                }
                else if (hitInfo.collider.name == "StopButton")
                {
                    hitInfo.collider.name = "StartButton";
                    hitInfo.collider.GetComponentInChildren<TextMesh>().text = "Go!";
                    Time.timeScale = 0;
                    foreach (var marblePair in originalPositions)
                    {
                        marblePair.Key.transform.position = marblePair.Value;
                        marblePair.Key.SetActive(true);
                        marblePair.Key.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    foreach(var goal in goals)
                    {
                        goal.Reset();
                    }
                }
                else if (SelectionBlobs.Contains(hitInfo.collider.gameObject))
                {
                    var blob = hitInfo.collider.gameObject;
                    _selected.gameObject.layer = blob.layer;
                    _selected.GetComponent<Renderer>().material = blob.GetComponent<Renderer>().material;
                    HideSelectionBlobs();
                }
                else if (platform != null)
                {
                    _selected = platform;
                    ShowSelectionBlobs();
                }
            }
            else
            {
                HideSelectionBlobs();
            }
        }

        bool finished = true;
        foreach (var goal in goals)
        {
            if (goal.gameObject.activeSelf)
                finished = false;
        }
        if (finished)
        {
            Application.LoadLevel((Application.loadedLevel + 1) % Application.levelCount);
        }
	}

    private void HideSelectionBlobs()
    {
        foreach (var blob in SelectionBlobs)
        {
            blob.SetActive(false);
        }
    }

    private void ShowSelectionBlobs()
    {
        var angle = 360.0f / SelectionBlobs.Length;

        for (int i = 0; i < SelectionBlobs.Length; i++)
        {
            SelectionBlobs[i].transform.localPosition = _selected.transform.position + Vector3.back + Quaternion.Euler(0, 0, angle * i) * (Vector3.up * 1);
            SelectionBlobs[i].SetActive(true);
        }
    }
}
