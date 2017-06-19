using UnityEngine;
using System.Collections;
using Text = UnityEngine.UI.Text;

public class PlayerIcon : MonoBehaviour {

    public Text ready;
    public Text notReady;


    int playNumber;
    public bool isReady;

    UnityEngine.UI.Image rend;
    Material defaultMaterial;
    static WebCamTexture wt;

    // Use this for initialization
    void Awake ()
    {
        isReady = false;
        rend = GetComponent<UnityEngine.UI.Image>();
        defaultMaterial = rend.material;
        wt = null;

        //wt = new WebCamTexture();
        //wt.Play();
    }

    void Start()
    {
        if (wt != null)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Attached Webcam(s):");
        for (var i = 0; i < devices.Length; i++)
            Debug.Log(devices[i].name);
        
            wt = new WebCamTexture();
            wt.Play();
        }
    }
    // Update is called once per frame
    void Update ()
    {
        if (!Pauser.paused)
        {
            ready.enabled = false;
            notReady.enabled = false;
            isReady = false;
        }
        else
        {
            if (isReady)
            {
                ready.enabled = true;
                notReady.enabled = false;
            }
            else
            {
                ready.enabled = false;
                notReady.enabled = true;
            }
        }

        if (Input.GetKeyDown("0"))
            SetToDefault();

    }

    public void SetToDefault()
    {
        rend.material = defaultMaterial;
        isReady = true;
        Pauser.ResetTimer();
    }

    public void TakePicture()
    {
        Pauser.ResetTimer();
        Texture2D tex;
        //RectTransform rt = GetComponent<RectTransform>();
        //tex = new Texture2D(Mathf.CeilToInt(rt.sizeDelta.x), Mathf.CeilToInt(rt.sizeDelta.y), TextureFormat.RGB24, false);

        //Get texture from webcam

        tex = new Texture2D(wt.width, wt.height);
        tex.SetPixels(wt.GetPixels());
        tex.Apply();

        Material mat = new Material(defaultMaterial);
        mat.color = Color.white;
        mat.mainTexture = tex;
        rend.material = mat;

        isReady = true;

        //wt.Stop();
    }

    public void SetReady(bool value)
    {
        isReady = value;
        Pauser.ResetTimer();
    }

    public void SetPlayerNumber(int number)
    {
        playNumber = number;
    }
    public int GetPlayerNumber()
    {
        return playNumber;
    }

    public bool IsReady()
    {
        return isReady;
    }

    public static void StopWebcam()
    {
        wt.Stop();
    }
}
