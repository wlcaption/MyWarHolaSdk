package sdk.holagames.com.mywarsdkforandroid;

import android.os.Bundle;
import android.content.Intent;
import com.holagames.sdk.MessageHandle;
import com.holagames.sdk.UserWrapper;
import com.holagames.sdk.Util;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    protected void onRestart() {
        super.onRestart();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onPause() {
        super.onPause();
    }

    @Override
    protected void onStop() {
        super.onStop();

    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
    }

    public void HolaSdkInit(String appid, String appkey, String privateKey,String oauthLoginServer)
    {
        String msg = "init";
        if(msg!=null){
            MessageHandle.resultCallBack(Util.User,UserWrapper.InitSuccess,"success");
        }else {
            MessageHandle.resultCallBack(Util.User,UserWrapper.InitFalied,"failed");
        }
    }

    public void Login(String p) {
        String msg = "login";
        if(msg!=null){
            MessageHandle.resultCallBack(Util.User,UserWrapper.LoginSuccess,"success");
        }else {
            MessageHandle.resultCallBack(Util.User,UserWrapper.LoginFailed,"failed");
        }
    }

    public void Pay(String payResult){
        String msg = "pay";
        if(msg!=null){
            MessageHandle.resultCallBack(Util.User,UserWrapper.PaySuccess,"success");
        }else {
            MessageHandle.resultCallBack(Util.User,UserWrapper.PayFailed,"failed");
        }
    }

    public void Logout()
    {
        String msg = "logout";
        if(msg!=null){
            MessageHandle.resultCallBack(Util.User,UserWrapper.LogoutSuccess,"success");
        }else {
            MessageHandle.resultCallBack(Util.User,UserWrapper.LogoutFailed,"failed");
        }
    }

    public void CheckBill(String userip)
    {
        String msg = "checkBill";
        if(msg!=null){
            MessageHandle.resultCallBack(Util.User,UserWrapper.InitSuccess,"success");
        }else {
            MessageHandle.resultCallBack(Util.User,UserWrapper.InitFalied,"failed");
        }
    }
}
