using UnityEngine;
using System.Collections;
using Holagames;
using System.Collections.Generic;
using System;
using System.Text;

public class HuaWeiGameCenter : MonoBehaviour
{
    public bool IsInit = false;
    public bool IsCheckBill = false;
    public float Timer = 0;
    public string Message = "AAAA";
    public string Message1 = "AAAA";

    void Awake()
    {
        //UnityEngine.NotificationServices.deviceToken.ToString();
        HolagamesSDK.getInstance().registerActionCallback((int)HolagamesSDKType.User, this, "HolaSdkCallBack");
        HolagamesSDK.getInstance().registerActionCallback((int)HolagamesSDKType.IAP, this, "HolaSdkCallBack");
    }

   

    void AndroidReceive(string content)
    {
        Debug.Log(content);
        Message1 = content;
    }

    void AndroidReceiveChangeUser(string ChangeUser)
    {
        Debug.Log("ChangeUser");
        StartSDK();
    }

    void AndroidReceiveLoginFailed(string Value)
    {

    }

    void AndroidReceiveLogin(string Value)
    {
        string Account = Value.Split('_')[0];
        string Password = Value.Split('_')[1];
        Debug.Log(Account + " " + Password);

    }

    public void StartSDK()
    {
#if XIAOMI
        HolagamesSDK.getInstance().init("2882303761517475726", "5331747594726", "HfM9lFxOF5S0lMdWbkXgQQ==", "");  
        return;
#elif OPPO   
        HolagamesSDK.getInstance().init("", "ab03f4746b8C0A1d68624598177f64AD", "", "");  
        return;
#elif UC
        HolagamesSDK.getInstance().init("", "", "", "");  
        return;
#elif QIHOO360
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif VIVO
        HolagamesSDK.getInstance().init("9885e4eaa65e696ed43360ac994a05df", "42816dacc5cfbe9fd0e95cfe35f2eebc", "20160201102033928864", "");  
        return;
#elif QQ
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif BAIDU
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif IQIYI
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif MZW
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif AMIGO
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif MEIZU
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif COOLPAD
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif LESHI
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif DPWNJOY
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif ANZHI
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif WDJ
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif LENOVO
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif PYW
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif HOLA
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif TT
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif HAIMA
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif JOLO
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif DEBUG
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif QIANHUAN
        HolagamesSDK.getInstance().init("","","","");
        return;
#else
        HolagamesSDK.getInstance().init("", "", "", "");
#endif
    }


    private void HolaSdkCallBack(string msg)
    {
        Debug.Log("HolaSdkCallBack==" + msg);
        Message = msg;
        Dictionary<string, string> dic = HolagamesSDK.stringToDictionary(msg);
        int code = Convert.ToInt32(dic["code"]);
        string result = dic["msg"];
        switch (code)
        {
            case (int)UserWrapper.InitFalied:
                Debug.LogError("SDK Init Failed");
                break;

            case (int)UserWrapper.InitSuccess:
                Debug.LogError("SDK InitSuccess");
                Message1 = "SDK InitSuccess";
                IsInit = true;
                break;
            case (int)UserWrapper.LoginCancel:
#if XIAOMI
                HolagamesSDK.getInstance().Login("");
#endif
                break;

            case (int)UserWrapper.LoginSwitch:
                break;

            case (int)UserWrapper.LoginFailed:
                break;

            case (int)UserWrapper.Logining:
                break;

            case (int)UserWrapper.LoginSuccess:
                Debug.LogError("SDK LoginSuccess");
                string _chane = "QQ";

                string Account = _chane + result;
                string Password = "t_" + result;
                Debug.Log(Account + " " + Password);

                Message = Account + " " + Password;

                break;
            case (int)UserWrapper.LogoutFailed:
                break;

            case (int)UserWrapper.LogoutSuccess:
                Message1 = "SDK LogoutSuccess";
                break;

            case (int)UserWrapper.notLogin:
                break;

            case (int)UserWrapper.PayFailed:
                IsCheckBill = false;
                Message = "UserWrapper.PayFailed" + msg;
                break;

            case (int)UserWrapper.PaySuccess:
                IsCheckBill = false;
                Message = "UserWrapper.PaySuccess";
                break;

            case (int)UserWrapper.PayCheck:
                IsCheckBill = true;
                Message = "UserWrapper.PayCheck";
                break;
        }
    }

    void Update()
    {
        if (IsCheckBill)
        {
            Message1 = Timer.ToString();
            Timer += Time.deltaTime;
            if (Timer > 1)
            {
                Timer -= 1;
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        jo.Call("CheckBill", "10001-1-1-6");
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("initSDK", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "Init";
            StartSDK();
        } 

        if (GUILayout.Button("Login", GUILayout.Height(80), GUILayout.Width(100)))
        {

            Message1 = "Login";
#if QQ
        HolagamesSDK.getInstance().Login("WX");
#else
            HolagamesSDK.getInstance().Login("");
#endif

        }

        if (GUILayout.Button("ToGame", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "ToGame";
            HolagamesSDK.getInstance().loginGameRole("create", "roleId=10278&roleName=阳光社&roleLevel=12&zoneId=androids26&zoneName=混服22服幽灵杀手&roleCTime=1481543091&roleLevelMTime=1481547775&vip=0");
        }

        if (GUILayout.Button("CreateRole", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "CreateRole";
#if IQIYI
            HolagamesSDK.getInstance().CreateRole("", "");
#elif VIVO
            HolagamesSDK.getInstance().CreateRole("", "");
#elif HOLA 
            HolagamesSDK.getInstance().CreateRole("", "");
#endif
        }

        if(GUILayout.Button("EnterGameCenter", GUILayout.Height(80),GUILayout.Width(100)))
        {
            Message1 = "EnterGameCenter";
#if HOLA
            HolagamesSDK.getInstance().entryGameCenter();
#endif
        }

        if (GUILayout.Button("ShowFloatView", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "ShowFloatView";
#if PYW
            HolagamesSDK.getInstance().showToolBar();
#elif VIVO
            HolagamesSDK.getInstance().showToolBar();
#elif HUAWEI
            HolagamesSDK.getInstance().showToolBar();
#elif MEIZU
            HolagamesSDK.getInstance().showToolBar();
#elif QIANHUAN
            HolagamesSDK.getInstance().showToolBar();
#endif
        }

        if (GUILayout.Button("Pay", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "Pay";
#if UC
            HolagamesSDK.getInstance().Pay("1_60_10001_1_1_1_1_1_1");  //UC
#elif QQ
            HolagamesSDK.getInstance().Pay("1_60_10001_1_1_1_1_1_1");  //QQ zoneid, diamond, guid, serverid, paytype		
#elif QIHOO360
            HolagamesSDK.getInstance().Pay("100_60钻石_1_xxx_10001_10001" + UnityEngine.Random.Range(1,10000) + "_10001-1-1-6");  //360  Product_Price Product_Name Product_Id Role_Name Role_Id OrderId guid-server_id-pay_type-cash
#elif VIVO
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");  //VIVO diamond, zoneid, guid, name, level, paytype, ProductName
#elif OPPO
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");  //VIVO diamond, zoneid, guid, name, level, paytype, ProductName
#elif BAIDU
             HolagamesSDK.getInstance().Pay("1_AndroidS10_10001_xxx_1_1_60钻石");//BAiDU diamond, zoneid, guid, name, level, paytype, ProductName
#elif HUAWEI
             HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//HUAWEI diamond, zoneid, guid, name, level, paytype, ProductName
#elif IQIYI
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//IQIYI diamond, zoneid, guid, name, level, paytype, ProductName
#elif MZW
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//mzw diamond, zoneid, guid, name, level, paytype, ProductName
#elif AMIGO
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//am diamond, zoneid, guid, name, level, paytype, ProductName
#elif MEIZU
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//meizu diamond, zoneid, guid, name, level, paytype, ProductName
#elif COOLPAD
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//coolpad diamond, zoneid, guid, name, level, paytype, ProductName
#elif LESHI
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//leshi diamond, zoneid, guid, name, level, paytype, ProductName
#elif DOWNJOY
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//downjoy diamond, zoneid, guid, name, level, paytype, ProductName
#elif ANZHI
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//anzhi diamond, zoneid, guid, name, level, paytype, ProductName
#elif WDJ
            HolagamesSDK.getInstance().Pay("1_60_10001_1_1_1_1_1_1");//WDJ diamond, zoneid, guid, name, level, paytype, ProductName
#elif LENOVO
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//lenovo diamond, zoneid, guid, name, level, paytype, ProductName
#elif PYW
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石_1");//pyw diamond, zoneid, guid, name, level, paytype, ProductName
#elif HOLA
            HolagamesSDK.getInstance().Pay("1_AndroidS1_10001_6_1_1_60钻石_1");//hola diamond, zoneid, guid, name, level, paytype, ProductName,servername
#elif TT
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石");//tt diamond, zoneid, guid, name, level, paytype, ProductName
#elif XIAOMI
            HolagamesSDK.getInstance().Pay("1_1_10001_xxx_1_1_60钻石_1");//xiaomi diamond, zoneid, guid, name, level, paytype, ProductName
#elif JOLO
            HolagamesSDK.getInstance().Pay("1_AndroidS1_10001_6_1_1_60钻石_1");//聚乐 diamond, zoneid, guid, name, level, paytype, ProductName
#elif DEBUG
            HolagamesSDK.getInstance().Pay("http://www.baidu.com");//debug url
#elif QIANHUAN
            HolagamesSDK.getInstance().Pay("1_AndroidS1_10001_6_1_1_60钻石_1");//千幻 diamond, zoneid, guid, name, level, paytype, ProductName
#else
            HolagamesSDK.getInstance().Pay("");
#endif
        }

        
        if (GUILayout.Button("CheckBill", GUILayout.Height(80), GUILayout.Width(100)))
        {
//#if DEBUG
            HolagamesSDK.getInstance().CheckBill("http://www.baidu.com");  //debug
//#endif
            Message1 = "CheckBill";
            //HolagamesSDK.getInstance().CheckBill("http://gdown.baidu.com/data/wisegame/ba226d3cf2cfc97b/baiduyinyue_4920.apk");
        }

        if (GUILayout.Button("ExitSDk", GUILayout.Height(80), GUILayout.Width(100)))
        {
#if QQ
            HolagamesSDK.getInstance().Logout();
#else
            HolagamesSDK.getInstance().ExitSDK();
#endif
        }

        if (GUILayout.Button("ChangeAccount", GUILayout.Height(80), GUILayout.Width(100)))
        {
            Message1 = "ChangeAccount";
            HolagamesSDK.getInstance().SwitchLogin();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label(Message, GUILayout.Height(200), GUILayout.Width(500));
        GUILayout.Label(Message1, GUILayout.Height(200), GUILayout.Width(500));
        GUILayout.EndHorizontal();
    }



    /// <summary>
    /// 实现php的URLENCODE函数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();

        for (var i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                sb.Append(c);
            }
            else
            {
                byte[] bystr = charToByte(str[i]);

                sb.Append(@"%");

                for (int ij = 0; ij < bystr.Length; ij++)
                {
                    if (bystr[ij] != 0)
                    {
                        sb.Append(Convert.ToString(bystr[ij], 16).ToUpper());
                    }
                }
            }
        }
        return (sb.ToString());
    }

    public byte[] charToByte(char c)
    {
        byte[] b = new byte[2];
        b[0] = (byte)((c & 0xFF00) >> 8);
        b[1] = (byte)(c & 0xFF);
        return b;
    }


    /// <summary>
    /// ios登录取帐号
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    IEnumerator LoginPhp(WWW www)
    {

        yield return www;
        Debug.LogError(www.text);
        Debug.LogError(www.error);
        if (www.error == null && www.text != "0")
        {
            string _chane = "KY_";

#if KY
#elif TBT
            _chane = "TBT_";
            if (www.text == "-1")
            {
                HolagamesSDK.getInstance().Login("");
                return;
            }
#endif

            string Account  = _chane + www.text;
            string Password = "t_" + www.text;
            Debug.Log(Account + " " + Password);

        }

    }
}
