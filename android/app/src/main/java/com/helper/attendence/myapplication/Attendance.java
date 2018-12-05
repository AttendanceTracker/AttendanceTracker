package com.helper.attendence.myapplication;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Date;

import static android.support.constraint.Constraints.TAG;

/* File created by: Zack Witherspoon
/  Description: This class is a simple class that holds the Date and Attendance
/
*/

public class Attendance {
    @SerializedName("Date")
    @Expose
    private Date Date;
    @SerializedName("DidAttend")
    @Expose
    private Boolean DidAttend;

    //Function created by: Zack Witherspoon
    public ArrayList<Attendance> getAttendance(Long studentId, Long classId) {
        HttpClient http = new HttpClient();
        String params = "studentId=" + studentId.toString() + "&classid=" + classId.toString();
        System.out.println("Parmas =" + params);
        Log.i(TAG, "Running GET call.");
        String response = http.getCall("/api/attendance/Get", params);

        System.out.println("Response =|" + response + "|");
        if(!response.equals("Failed")) {
            if(!response.equals("")) {
                try {
                        Type object = new TypeToken<ArrayList<Attendance>>(){}.getType();
                        Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd").create();
                        ArrayList<Attendance> returnVal= gson.fromJson(response, object);
                        return returnVal;
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        else
        {
            Log.d(TAG, "ERROR, the GET call did not work");
            ArrayList<Attendance> returnVal = null;
            return returnVal;
        }
        return null;
    }

    //Function created by: Zack Witherspoon
    public Date getDate() {
        return Date;
    }

    //Function created by: Zack Witherspoon
    public void setDate(java.util.Date date) {
        Date = date;
    }

    //Function created by: Zack Witherspoon
    public Boolean getDidAttend() {
        return DidAttend;
    }

    //Function created by: Zack Witherspoon
    public void setDidAttend(Boolean didAttend) {
        DidAttend = didAttend;
    }
}
