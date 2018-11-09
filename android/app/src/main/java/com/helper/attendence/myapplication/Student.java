package com.helper.attendence.myapplication;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;

import static android.support.constraint.Constraints.TAG;

public class Student {

    private String fname;

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

    private String lname;
    private String email;
    private Long cwid;
    private Long imei;

    public Student() {};
    public Student(String fname, String lname, String email, Long cwid) {
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.cwid = cwid;
    }

    public Student getStudent(Long sCWID) throws IOException {
        System.out.println("Starting my search!");
        Log.i(TAG,"Starting my search!");
        Log.e(TAG,"Starting my search!");

        String fname = "";
        String lname = "";
        String email = "";
        Long cwid = -1L;



        URL obj = new URL("https://attendancetracker.us/api/Student/Get?cwid=" + sCWID.toString());
        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        // optional default is GET
        con.setRequestMethod("GET");
        //add request header
        con.setRequestProperty("User-Agent", "Mozilla/5.0");
        int responseCode = con.getResponseCode();
        System.out.println("\nSending 'GET' request to URL : " + "https://attendancetracker.us/api/Student/Get");
        System.out.println("Response Code : " + responseCode);
        BufferedReader in = new BufferedReader(
                new InputStreamReader(con.getInputStream()));
        String inputLine;
        StringBuffer response = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();
        //print in String
        System.out.println(response.toString());
        //Read JSON response and print
        try {
            JSONObject myResponse = new JSONObject(response.toString());
            System.out.println("result after Reading JSON Response");
            System.out.println("responseCode- " + responseCode);
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

    public void registerStudent(String fname, String lname, String email, Long cwid) {

    }

//    public getAttendance(Long cwid,
//
//    }


    public static String getParamsString(Map<String, String> params)
            throws UnsupportedEncodingException {
        StringBuilder result = new StringBuilder();

        for (Map.Entry<String, String> entry : params.entrySet()) {
            result.append(URLEncoder.encode(entry.getKey(), "UTF-8"));
            result.append("=");
            result.append(URLEncoder.encode(entry.getValue(), "UTF-8"));
            result.append("&");
        }

        String resultString = result.toString();
        return resultString.length() > 0
                ? resultString.substring(0, resultString.length() - 1)
                : resultString;
    }


}
