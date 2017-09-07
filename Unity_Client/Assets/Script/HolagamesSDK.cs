using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Runtime.InteropServices;

namespace Holagames
{
    public enum HolagamesSDKType
    {
        Analytics = 1,
        Share = 2,
        Social = 4,
        IAP = 8,
        Ads = 16,
        User = 32,
        Push = 64,
    };

    public enum UserWrapper
    {
        ExitByGame = 0,
        ExitBySdk = 1,

        InitSuccess = 100,
        InitFalied = 101,

        LoginSuccess = 200,
        LoginFailed = 201,
        LoginCancel = 202,
        Logining = 203,
        LoginSwitch = 204,

        LogoutSuccess = 300,
        LogoutFailed = 301,
        notLogin = 302,
        GameContinue = 351,
        GameExit = 352,

        PaySuccess = 400,
        PayFailed = 401,
        PayCancel = 402,
        PayCheck = 403,

    };

    public class HolagamesSDK
    {
#if UNITY_IPHONE

        /*[DllImport("__Internal")]
        private static extern void _registerActionResultCallback(int type, string gameObject,string func);

        [DllImport("__Internal")]
        private static extern void _LogOut();

        [DllImport("__Internal")]
        private static extern void _entryaGameCenter();
        
        [DllImport("__Internal")]
        private static extern void _Login();

        [DllImport("__Internal")]
        private static extern void _Init();
        
        [DllImport("__Internal")]
        private static extern string _getUserID();
                
        [DllImport("__Internal")]
        private static extern void _hideToolBar();

        [DllImport("__Internal")]
        private static extern void _showToolBar();


          [DllImport("__Internal")]
        private static extern void _pay(string msg);*/


#endif

        private static HolagamesSDK _instance;
        public static HolagamesSDK instance;


        public static HolagamesSDK getInstance()
        {
            if (null == _instance)
            {
                _instance = new HolagamesSDK();
            }
            return _instance;
        }

        public HolagamesSDK()
        {
#if UNITY_ANDROID
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
        }

        /// <summary>
        /// sdk初始化
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appkey"></param>
        public void init(string appid, string appkey, string privateKey, string oauthLoginServer)
        {
#if UNITY_ANDROID
            Debug.Log("UNITY_ANDROID===HolaSdkInit!!");
            jo.Call("HolaSdkInit", appid, appkey, privateKey, oauthLoginServer);
#elif UNITY_IPHONE
            //_Init();
#endif
        }

        // 登录
        public void Login(string LoginString)
        {
#if UNITY_ANDROID
            Debug.Log("UNITY_ANDROID===HolaSdk==Login!!");
            jo.Call("Login", LoginString);
#endif

#if UNITY_IPHONE
            // _Login();
#endif
        }

        // 注销
        public void Logout()
        {
#if UNITY_ANDROID
            jo.Call("Logout");
#endif
#if UNITY_IPHONE

            //_LogOut();
#endif
        }

        // 切换帐号
        public void SwitchLogin()
        {
#if UNITY_ANDROID
            jo.Call("SwitchLogin");
#endif
        }

        // 当前登陆状态
        public bool isLogin()
        {
#if UNITY_ANDROID
            return jo.Call<bool>("isLogin");
#else
            return false;
#endif
        }

        // 隐藏悬浮窗
        public void hideToolBar()
        {
#if UNITY_ANDROID
            Debug.Log("hideToolBar");
            jo.Call("hideToolBar");
#endif
        }

        // 显示悬浮窗
        public void showToolBar()
        {
#if UNITY_ANDROID
            Debug.Log("showToolBar");
            jo.Call("showToolBar");
#endif
        }

        // 进入用户中心
        public void entryGameCenter()
        {
#if UNITY_ANDROID
            Debug.Log("entryGameCenter");
            jo.Call("entryGameCenter");
#endif

#if UNITY_IPHONE
            //_entryaGameCenter();
#endif
        }

        public void ExitSDK()
        {
#if UNITY_ANDROID
            jo.Call("Logout");
#endif
        }


        // 支付
        public void Pay(string msg)
        {
#if UNITY_ANDROID
            Debug.Log("andriod pay=" + msg);
            jo.Call("Pay", msg);
#elif UNITY_IPHONE
            //_pay(msg);
#endif
        }


        // 支付
        public void CheckBill(string msg)
        {
#if UNITY_ANDROID
            Debug.Log("andriod CheckBill=" + msg);
            jo.Call("CheckBill", msg);
#elif UNITY_IPHONE
            //_pay(msg);
#endif
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public string getUserID()
        {
#if UNITY_ANDROID
            return jo.Call<string>("getUserID");
#elif UNITY_IPHONE
            return "";//_getUserID();
#else
            return string.Empty;
#endif
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public string getChannelId()
        {
#if UNITY_ANDROID
            return jo.Call<string>("getChannelId");
#else
            return string.Empty;
#endif
        }


        public void registerActionCallback(int type, MonoBehaviour gameObject, string functionName)
        {
#if UNITY_ANDROID
            if (mAndroidJavaClass == null)
            {
                mAndroidJavaClass = new AndroidJavaClass("com.holagames.sdk.MessageHandle");
            }
            string gameObjectName = gameObject.gameObject.name;
            mAndroidJavaClass.CallStatic("registerActionResultCallback", new object[] { type, gameObjectName, functionName });
#endif

#if UNITY_IPHONE
            //_registerActionResultCallback(type, gameObject.gameObject.name, functionName);
#endif
        }

        public void loginGameRole(string type, string msg)
        {
#if UNITY_ANDROID
            jo.Call("LoginGameRole", type, msg);
#endif

        }

        public void CreateRole(string type, string msg)
        {
#if UNITY_ANDROID
            jo.Call("CreateRole", type, msg);
#endif
        }

#if UNITY_ANDROID
        private AndroidJavaClass jc;
        private AndroidJavaObject jo;

        private AndroidJavaClass mAndroidJavaClass;
#endif
        /**
     	@brief the Dictionary type change to the string type 
    	 @param Dictionary
    	 @return  string
    	*/
        public static Dictionary<string, string> stringToDictionary(string message)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (null != message)
            {
                    string[] tokens = message.Split('&');

                    for (var i = 0; i < tokens.Length; i++)
                    {
                        string[] _s = tokens[i].Split('=');
                        if (_s.Length == 2)
                        {
                            param.Add(_s[0], _s[1]);
                        }
                        else
                        {
                            string _v = "";
                            int _index = tokens[i].IndexOf("=");
                            _v = tokens[i].Substring(_index + 1);

                            param.Add(_s[0], _v);
                        }
                    }
            }

            return param;
        }


        public static string dictionaryToString(Dictionary<string, string> maps)
        {
            StringBuilder param = new StringBuilder();
            if (null != maps)
            {
                foreach (KeyValuePair<string, string> kv in maps)
                {
                    if (param.Length == 0)
                    {
                        param.AppendFormat("{0}={1}", kv.Key, kv.Value);
                    }
                    else
                    {
                        param.AppendFormat("&{0}={1}", kv.Key, kv.Value);
                    }
                }
            }
            //			byte[] tempStr = Encoding.UTF8.GetBytes (param.ToString ());
            //			string msgBody = Encoding.Default.GetString(tempStr);
            return param.ToString();
        }
    }

}
