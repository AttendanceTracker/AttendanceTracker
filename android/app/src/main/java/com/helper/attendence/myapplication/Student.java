package com.helper.attendence.myapplication;

import android.util.Log;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import org.json.JSONException;
import org.json.JSONObject;
import java.io.Serializable;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;

import static android.support.constraint.Constraints.TAG;

public class Student implements Serializable {

    private HttpClient httpRequests;
    private String fname;
    private String lname;
    private String email;
    private Long cwid;
    private Device studentDevice;
    private ArrayList<sClass> classes;


    public Student(Long Imei) {
        this.studentDevice= new Device(Imei);
        this.httpRequests = new HttpClient();
    }

    public Student() {httpRequests = new HttpClient();}

    public Student(String sFName, String sLName, String sEmail, Long sCwid) {
        this.fname = sFName;
        this.lname = sLName;
        this.email = sEmail;
        this.cwid = sCwid;
        this.studentDevice = new Device();
        this.classes = null;
        this.httpRequests = new HttpClient();
    }

    public Student(String sFName, String sLName, String sEmail, Long sCwid, Long imei) {
        this.fname = sFName;
        this.lname = sLName;
        this.email = sEmail;
        this.cwid = sCwid;
        this.studentDevice = new Device();
        this.httpRequests = new HttpClient();
        this.classes = null;
        this.studentDevice= new Device(imei);
    }

    public boolean checkIn(Double lat, Double longitude, String payload, Long CWID){

        //Payload is {"classid":3,"payload": "somestring"}

        String[] tokens = payload.split("[ {\":,}]+");

        /*
         *
         * tokens[0] = null
         * tokens[1] = "classid"
         * tokens[2] = classId#
         * tokens[3] = "payload"
         * tokens[4] = actual payload
         *
         */
        Long classID = Long.valueOf(tokens[2]);


//        for (int i = 0; i < tokens.length ; i++){
//            System.out.println("TOKEN " + i + ": " + tokens[i]);
//        }

        HashMap<String, String> postDataParams = new HashMap<>();
        postDataParams.put("StudentID", CWID.toString());
        postDataParams.put(tokens[4], payload); //tokens[4] = actual payload
        postDataParams.put("Latitude", lat.toString());
        postDataParams.put("Longitude", longitude.toString());

        System.out.println("id:" + CWID);
        System.out.println("Lat" + lat);
        System.out.println("Long:" + longitude);
        System.out.println("Payload:" + payload);
        System.out.println("~~~~~~~~~~~~~~~~~~~~~~~~~~      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        String api = "/api/Attendance/CheckIn?classID=" + classID; //put classID here

        Log.i(TAG, "Running POST call.");
        String response = httpRequests.postCall(api,postDataParams);
        if(!response.equals("Failed")) {

            if (response.equals("false")) {
                System.out.println("FAILED BITCH");
                //handle this error
                return false;
            }
            else {
                System.out.println("YASSSS BITCH");
                //Implement feedback on success
                return true;
            }
        }
        else {
            //handle this
        }
        return false;
    }

    public Student getStudent(Student std) {
        String fname = "";
        String lname = "";
        String email = "";
        Long cwid = -1L;

        String params = "cwid=" + std.getCwid().toString();
        Log.i(TAG, "Running GET call.");
        String response = httpRequests.getCall("/api/Student/Get", params);
        System.out.println("Response for getStudent:|" + response + "|");
        if(!response.equals("Failed")) {
            if(response.equals("")) {
                System.out.println("RESPONSE + ''");
                std.registerStudent(std);
                Student x = std.getStudent(std);
                System.out.println("New student  = ");
                x.printAll();
            }
            else {
                try {
                    JSONObject myResponse = new JSONObject(response);
                    System.out.println("result after Reading JSON Response");
                    System.out.println("CWID- " + myResponse.getString("CWID"));
                    System.out.println("FirstName- " + myResponse.getString("FirstName"));
                    System.out.println("LastName- " + myResponse.getString("LastName"));
                    System.out.println("Email- " + myResponse.getString("Email"));

                    fname = myResponse.getString("FirstName");
                    lname = myResponse.getString("LastName");
                    email = myResponse.getString("Email");
                    cwid = Long.parseLong(myResponse.getString("CWID"));
                    std.setFname(fname);
                    std.setLname(lname);
                    std.setEmail(email);
                } catch (Exception e) {
                    e.printStackTrace();
                    try {
                        throw e;
                    } catch (JSONException e1) {
                        e1.printStackTrace();
                    }
                }
            }
            Log.i(TAG, "*****************************************************************\n" +
                    "Found a student. Fname: " + fname + "Lname: " + lname + "Email: " + email + "CWID: " + cwid +
                    "*****************************************************************");
        }
        else {
            Log.d(TAG, "ERROR, the getStudent call did not work");
        }
        return std;
    }

    public void printAll() {
        System.out.println("---------------------------------------------------------------------");
        System.out.println("Printing all the info: ");
        System.out.println("First Name: " + getFname());
        System.out.println("Last Name: " + getLname());
        System.out.println("Email: " + getEmail());
        System.out.println("CWID: "+ getCwid());
        System.out.println("IMEI: " +  getImei());
        System.out.println("---------------------------------------------------------------------");
    }

    public Boolean registerStudent(Student std) {
        Boolean bResponse = false;
        String newFName = "";
        String newLName = "";
        String newEmail = "";
        Long newCwid = -1L;

        HashMap<String, String> postDataParams = new HashMap<>();
        postDataParams.put("cwid", std.getCwid().toString());
        postDataParams.put("firstname", std.getFname());
        postDataParams.put("lastname", std.getLname());
        postDataParams.put("email", std.getEmail());
        Log.i(TAG, "Running POST call.");
        String response = httpRequests.postCall("/api/Student/Register",postDataParams);
        System.out.println("Response =|" + response + "|");
        if (response.equals("Already Exists")) {
            return null;
        }
        else if(!response.equals("Failed")) {
            try {
                JSONObject myResponse = new JSONObject(response);
                System.out.println("result after Reading JSON Response");
                System.out.println("FirstName- " + myResponse.getString("FirstName"));
                System.out.println("LastName- " + myResponse.getString("LastName"));
                System.out.println("Email- " + myResponse.getString("Email"));
                System.out.println("CWID- " + myResponse.getString("CWID"));

                newFName = myResponse.getString("FirstName");
                newLName = myResponse.getString("LastName");
                newEmail = myResponse.getString("Email");
                newCwid = Long.parseLong(myResponse.getString("CWID"));
            } catch (Exception e) {
                e.printStackTrace();
                try {
                    throw e;
                } catch (JSONException e1) {
                    e1.printStackTrace();
                }
            }
            bResponse = true;
            Log.i(TAG, "*****************************************************************\n" +
                    "Registered a new student. Fname: " + newFName + "Lname: " + newLName + "Email: " + newEmail + "CWID: " + newCwid +
                    " *****************************************************************");
        }
        else {
            Log.d(TAG, "ERROR, the registerStudent call did not work");
        }
        return bResponse;
    }

    public Boolean updateStudent(Student std, String changedFName, String changedLName, String changedEmail, Long permanentCWID) {
        Boolean bResponse = false;
        String newFName = "";
        String newLName = "";
        String newEmail = "";
        Long newCwid = -1L;

        System.out.println("Changing first name to: " + changedFName + " Lname to: " + changedLName + " and Email to: " + changedEmail);

        HashMap<String, String> putDataParams = new HashMap<>();
        putDataParams .put("firstname", changedFName);
        putDataParams .put("lastname", changedLName);
        putDataParams .put("email", changedEmail);

        String params = "cwid=" + permanentCWID.toString();
        Log.i(TAG, "Running PUT call.");
        String response = httpRequests.putCall("api/Student/Update", params, putDataParams);
        System.out.println("Response =|" + response + "|");
        if (response.equals("Already Exists")) {
            bResponse = false;
        }
        else if(!response.equals("Failed")) {
            try {
                JSONObject myResponse = new JSONObject(response);
                System.out.println("result after Reading JSON Response");
                System.out.println("FirstName- " + myResponse.getString("FirstName"));
                System.out.println("LastName- " + myResponse.getString("LastName"));
                System.out.println("Email- " + myResponse.getString("Email"));
                System.out.println("CWID- " + myResponse.getString("CWID"));

                newFName = myResponse.getString("FirstName");
                newLName = myResponse.getString("LastName");
                newEmail = myResponse.getString("Email");
                newCwid = Long.parseLong(myResponse.getString("CWID"));

                std.setFname(newFName);
                std.setLname(newLName);
                std.setEmail(newEmail);
                std.setCwid(newCwid);
            } catch (Exception e) {
                e.printStackTrace();
                try {
                    throw e;
                } catch (JSONException e1) {
                    e1.printStackTrace();
                }
            }
            Log.i(TAG, "*****************************************************************\n" +
                    "Updated a new student. \nFname: " + fname + "\nLname: " + lname + "\nEmail: " + email + "\nCWID: " + cwid + "\n" +
                    "*****************************************************************\n");
            bResponse = true;
        }
        else {
            Log.d(TAG, "ERROR, the PUT call did not work");
        }
        return bResponse;
    }


    public Long getCwidFromDevice(Long deviceID) {
        Long sCwid = -1L;
        System.out.println("Starting Device find!");
        Log.i(TAG,"Starting Device find!");
        Log.e(TAG,"Starting Device find!");

        String params = "imei=" + deviceID.toString();
        Log.i(TAG, "Running GET call.");
        String response = httpRequests.getCall("/api/Device/Get", params);
        System.out.println("Response =|" + response + "|");
        if(!response.equals("Failed")) {
            if(!response.equals("")) {
                try {
                    JSONObject myResponse = new JSONObject(response);
                    System.out.println("result after Reading JSON Response");
                    System.out.println("CWID- " + myResponse.getString("StudentID"));
                    sCwid = Long.parseLong(myResponse.getString("StudentID"));
                    Log.i(TAG, "*****************************************************************\n" +
                            "Returning CWID:  " + sCwid + " from imei: " + deviceID + "\n" +
                            "*****************************************************************\n");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        else
        {
            Log.d(TAG, "ERROR, the GET call did not work");
        }
        return  sCwid;
    }

    // /api/Device/Register
    public Boolean registerDeviceToStudent(Student std, Long sImei) {

        Boolean bResponse = false;
        Long newCwid = -1L;
        Long newImei = -1L;

        String response = std.studentDevice.registerDevice(std.getCwid(), sImei);
        if (response.equals("Already Exists")) {
            bResponse = false;
        }
        else if(!response.equals("Failed")) {
            try {
                JSONObject myResponse = new JSONObject(response);
                System.out.println("result after Reading JSON Response");
                System.out.println("IMEI- " + myResponse.getString("IMEI"));
                System.out.println("CWID- " + myResponse.getString("StudentID"));


                newCwid = Long.parseLong(myResponse.getString("StudentID"));
                newImei= Long.parseLong(myResponse.getString("IMEI"));
                std.setImei(newImei);
//                bResponse = true;
                Log.i(TAG, "*****************************************************************\n" +
                        "Registered a device: " + newImei+ " to student : " + newCwid + "\n" +
                        "*****************************************************************");
            } catch (Exception e) {
                e.printStackTrace();
                try {
                    throw e;
                } catch (JSONException e1) {
                    e1.printStackTrace();
                }
            }
            bResponse = true;
        }
        else
        {
            Log.d(TAG, "ERROR, the registerDeviceToStudent call did not work");
        }
        return bResponse;
    }

    // /api/Device/Update
    public Boolean updateDevice(Student std, Device sDevice) {
        Boolean bResponse = false;
        Long newCwid = -1L;
        Long newImei= -1L;

        System.out.println("Changing IMEI: " + sDevice.getImei() + " to be with CWID: " + std.getCwid() );

        HashMap<String, String> putDataParams = new HashMap<>();
        putDataParams .put("StudentID", std.getCwid().toString());

        String params = "imei=" + sDevice.getImei().toString();
        Log.i(TAG, "Running PUT call.");
        String response = httpRequests.putCall("api/Device/Update", params, putDataParams);
        if (response.equals("Already Exists")) {
            bResponse = false;
        }
        else if(!response.equals("Failed")) {
            try {
                JSONObject myResponse = new JSONObject(response);
                System.out.println("result after Reading JSON Response");
                System.out.println("IMEI- " + myResponse.getString("IMEI"));
                System.out.println("CWID- " + myResponse.getString("StudentID"));

                newImei = Long.parseLong(myResponse.getString("IMEI"));
                newCwid = Long.parseLong(myResponse.getString("StudentID"));

//            std.setCwid(newCwid);
                std.setImei(newImei);
            } catch (Exception e) {
                e.printStackTrace();
                try {
                    throw e;
                } catch (JSONException e1) {
                    e1.printStackTrace();
                }
            }
            bResponse = true;
            Log.i(TAG, " *****************************************************************\n" +
                    "Updated a new Device. New Imei: " + newImei + " is linked to CWID : " + newCwid +
                    "*****************************************************************");
        }
        else {
            Log.d(TAG, "ERROR, the updateDevice call did not work");
        }
        return bResponse;
    }


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

    public Device getStudentDevice() { return studentDevice; }

    public Long getImei() { return this.studentDevice.getImei(); }

    public void setImei(Device d) { this.studentDevice = d; }

    public void setImei(Long i) {Device d = new Device(i); setImei(d);}

    public ArrayList<sClass> getClasses() { return classes; }

    public sClass getSpecificClass(int x) { return getClasses().get(x); }

    public void setClasses(String classString) {
        Type object = new TypeToken<ArrayList<sClass>>(){}.getType();
        Gson gson = new Gson();
        ArrayList<sClass> returnVal= gson.fromJson(classString, object);
        this.classes = returnVal;
    }

}
