package com.helper.attendence.myapplication;

import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;

import static android.support.constraint.Constraints.TAG;

public class Device {

    private Long imei;
    private HttpClient httpRequests;
    Device() { httpRequests = new HttpClient(); this.imei = -1L; }
    Device(Long v) { httpRequests = new HttpClient(); setImei(v); }

    // /api/Device/Register
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

    // /api/Device/Update
//    public void updateDevice(Student std, Long phoneId) {
//        Long newCwid = -1L;
//        Long newImei= -1L;
//
//        System.out.println("Changing phoneID: " + phoneId + " to be with student id: " + std.getCwid() );
//
//        HashMap<String, String> putDataParams = new HashMap<>();
//        putDataParams .put("StudentID", std.getCwid().toString());
//
//        String params = "imei=" + phoneId.toString();
//        Log.i(TAG, "Running PUT call.");
//        String response = httpRequests.putCall("api/Device/Update", params, putDataParams);
//        try {
//            JSONObject myResponse = new JSONObject(response);
//            System.out.println("result after Reading JSON Response");
//            System.out.println("IMEI- " + myResponse.getString("IMEI"));
//            System.out.println("CWID- " + myResponse.getString("StudentID"));
//
//            newImei = Long.parseLong(myResponse.getString("IMEI"));
//            newCwid = Long.parseLong(myResponse.getString("StudentID"));
//
////            std.setCwid(newCwid);
//            std.setImei(newImei);
//        }
//        catch (Exception e)
//        {
//            e.printStackTrace();
//            try {
//                throw e;
//            } catch (JSONException e1) {
//                e1.printStackTrace();
//            }
//        }
//
//        Log.i(TAG, "Updated a device IMEI: " + newCwid + " to cwid: " + newImei);
//    }



    public Long getImei() {
        return imei;
    }

    public void setImei(Long imei) {
        this.imei = imei;
    }
}
