using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

//onclick

public class control : MonoBehaviour
{
    // Start is called before the first frame update
    UniWebView webView;

    [SerializeField]
    RectTransform myUITransfrom;

    public Button yourButton;

    static bool Begin = false;
    static private JEnumerable<JToken> Inside_Payload = new JEnumerable<JToken>();

    //-----Get, Set------

    public static JEnumerable<JToken> Get_JToken(){
        return Inside_Payload;
    }

    public static void Set_Begin(){
        Begin = false;
    }

    public static bool Get_Begin(){
        return Begin;
    }

    public bool is_display = false;

    //-----Start Here-----

    void Start()
    {

        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        var webviewGameObject = new GameObject("UniWebView");
        // var uuu = UniWebViewHelper.StreamingAssetURLForPath("index.html");
        // url = Path.Combine(Application.streamingAssetsPath, "SampleVideo_1280x720_5mb.mp4");
        // Debug.Log(Application.streamingAssetsPath);
        is_display = false;
        webView = webviewGameObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0 , 0 , Screen.width , Screen.height);
        webView.ReferenceRectTransform = myUITransfrom;
        // webView.Load("https://www.google.com");
        webView.Load("https://edo-controller.web.app");
        //var indexURL = UniWebViewHelper.StreamingAssetURLForPath("pages/index.html");
        //var accessURL = UniWebViewHelper.StreamingAssetURLForPath("/");
        //webView.Load(indexURL, true, accessURL);
        //webView.Load("https://www.google.com");
        // webView.Load("localhost:5000");
        webView.CleanCache();
        UniWebView.ClearCookies();
        webView.AddUrlScheme("code");
        webView.Show();
        webView.OnPageFinished += (view , statusCode , url) => {
            if(!is_display){
            is_display = true;
            string stg_name = SceneManager.GetActiveScene().name;
            print("last " + stg_name[stg_name.Length - 1]);
            if((stg_name[stg_name.Length - 1] == '1') || (stg_name[stg_name.Length - 1] == '2' )){
                try{
                    webView.EvaluateJavaScript("display1();", (payload) => {
                    });
                }catch(Exception e){
                    print(e);
                }
            }
            else if((stg_name[stg_name.Length - 1] == 'x') || (stg_name[stg_name.Length - 1] == 'y' )){
                try{
                    webView.EvaluateJavaScript("display3();", (payload) => {
                    });
                }catch(Exception e){
                    print(e);
                }
            }
            else{
                try{
                    webView.EvaluateJavaScript("display2();", (payload) => {
                    });
                }catch(Exception e){
                    print(e);
                }
            }
        }
        };
        webView.OnShouldClose += (view) => {
            webView = null;
            return true;
        };
    }

    void TaskOnClick(){
        try{
            webView.EvaluateJavaScript("defined();", (payload)=>{
            if (payload.resultCode.Equals("0"))
            {
                try
                {
                    //new
                    Debug.Log("Clicked!");
                    JObject o = JObject.Parse(payload.data);
                    JEnumerable<JToken> jt = o["payload"].Children();
                    print(o);
                    // foreach(JToken token in jt){
                    //     print(token);
                    // }
                    Inside_Payload = jt;
                    Begin = true;
                    print(Begin);
                }
                catch(Exception e)
                {
                    print(e);
                }
            }
            else
            {
                Debug.Log("Something goes wrong: " + payload.data);
            }
            });
        }catch(Exception error){
            print(error);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(LevelLoader.closing == true)
        {
            print("WebView is Closing");
            webView.Hide();
            // Destroy(this);
        }

        
    }

    void FixedUpdate() {
        // if(webView == null){
        //     return;
        // }
        // webView.OnMessageReceived += (view, message) => {
        //     JObject o = JObject.Parse(message.Path);
        //     JEnumerable<JToken> jt = o["payload"].Children();
        //     Inside_Payload = jt;
        //     Begin = true;
        // };
        // Begin = false;
    }

    

}
