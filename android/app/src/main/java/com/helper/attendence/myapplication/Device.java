package com.helper.attendence.myapplication;

import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.HashMap;

import static android.support.constraint.Constraints.TAG;

/* File created by: Zack Witherspoon
/  Description: A class that holds the general items for the device that is stored by the student
/
*/

public class Device implements Serializable {

    private Long imei;
    private HttpClient httpRequests;

    //Function created by: Zack Witherspoon
    Device() { httpRequests = new HttpClient(); this.imei = -1L; }

    //Function created by: Zack Witherspoon
    Device(Long v) { httpRequests = new HttpClient(); setImei(v); }

    //Function created by: Zack Witherspoon
    public String registerDevice(Long sCwid, Long sImei) {
        Long newCwid = -1L;
        Long newImei= -1L;

        HashMap<String, String> postDataParams = new HashMap<>();
        postDataParams.put("imei", sImei.toString());
        postDataParams.put("studentid", sCwid.toString());

        Log.i(TAG, "Running POST call.");
        String response = httpRequests.postCall("/api/Device/Register",postDataParams);

        return response;
    }


    //Function created by: Zack Witherspoon
    public Long getImei() {
        return imei;
    }

    //Function created by: Zack Witherspoon
    public void setImei(Long imei) {
        this.imei = imei;
    }
}
