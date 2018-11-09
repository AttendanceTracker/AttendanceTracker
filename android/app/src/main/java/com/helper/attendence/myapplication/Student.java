package com.helper.attendence.myapplication;

import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;

import static android.support.constraint.Constraints.TAG;

public class Student {

    private HttpClient httpRequests;
    private String fname;
    private String lname;
    private String email;
    private Long cwid;
    private Long imei;

    public Student() {httpRequests = new HttpClient();}
    public Student(String fname, String lname, String email, Long cwid) {
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.cwid = cwid;
        this.httpRequests = new HttpClient();
    }

    public Student getStudent(Long sCWID) throws IOException {
        System.out.println("Starting my search!");
        Log.i(TAG,"Starting my search!");
        Log.e(TAG,"Starting my search!");

        String fname = "";
        String lname = "";
        String email = "";
        Long cwid = -1L;

        String params = "cwid=" + sCWID.toString();
        Log.i(TAG, "Running GET call.");
        String response = httpRequests.getCall("/api/Student/Get", params);

        try {
            JSONObject myResponse = new JSONObject(response);
            System.out.println("result after Reading JSON Response");
            System.out.println("CWID- " + myResponse.getString("CWID"));
            System.out.println("FirstName- " + myResponse.getString("FirstName"));
            System.out.println("LastName- " + myResponse.getString("LastName"));
            System.out.println("Email- " + myResponse.getString("Email"));

            fname = myResponse.getString("FirstName");
            lname = myResponse.getString("LastName");;
            email = myResponse.getString("Email");;
            cwid = Long.parseLong(myResponse.getString("CWID"));

        }
        catch (Exception e)
        {
            e.printStackTrace();
            try {
                throw e;
            } catch (JSONException e1) {
                e1.printStackTrace();
            }
        }

        Log.i(TAG, "Registered a new student. \nFname: " + fname + "\nLname: " + lname + "\nEmail: " + email + "\nCWID: " + cwid);
        return  new Student(fname, lname, email, cwid);
    }

    public void printAll() {
        System.out.println("---------------------------------------------------------------------");
        System.out.println("Printing all the info: ");
        System.out.println("First Name: " + getFname());
        System.out.println("Last Name: " + getLname());
        System.out.println("Email: " + getEmail());
        System.out.println("CWID: "+ getCwid());
        System.out.println("---------------------------------------------------------------------");
    }
    public void registerDevice(Long imei, String cwid) {

    }

    public Student registerStudent(String fname, String lname, String email, Long cwid) {

        HashMap<String, String> postDataParams = new HashMap<>();
        postDataParams.put("cwid", Long.toString(cwid));
        postDataParams.put("firstname", fname);
        postDataParams.put("lastname", lname);
        postDataParams.put("email", email);
        Log.i(TAG, "Running POST call.");
        String response = httpRequests.postCall("/api/Student/Register",postDataParams);

        try {
            JSONObject myResponse = new JSONObject(response);
            System.out.println("result after Reading JSON Response");
            System.out.println("FirstName- " + myResponse.getString("FirstName"));
            System.out.println("LastName- " + myResponse.getString("LastName"));
            System.out.println("Email- " + myResponse.getString("Email"));
            System.out.println("CWID- " + myResponse.getString("CWID"));

            fname = myResponse.getString("FirstName");
            lname = myResponse.getString("LastName");;
            email = myResponse.getString("Email");;
            cwid = Long.parseLong(myResponse.getString("CWID"));

        }
        catch (Exception e)
        {
            e.printStackTrace();
            try {
                throw e;
            } catch (JSONException e1) {
                e1.printStackTrace();
            }
        }

        Log.i(TAG, "Registered a new student. \nFname: " + fname + "\nLname: " + lname + "\nEmail: " + email + "\nCWID: " + cwid);
        return  new Student(fname, lname, email, cwid);
    }

//    public getAttendance(Long cwid,
//
//    }


    public String getFname() {
        return fname;
    }

    public void setFname(String fname) {
        this.fname = fname;
    }

    public String getLname() {
        return lname;
    }

    public void setLname(String lname) {
        this.lname = lname;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Long getCwid() {
        return cwid;
    }

    public void setCwid(Long cwid) {
        this.cwid = cwid;
    }

    public Long getImei() {
        return imei;
    }

    public void setImei(Long imei) {
        this.imei = imei;
    }

}
