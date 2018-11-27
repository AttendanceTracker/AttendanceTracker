package com.helper.attendence.myapplication;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Date;

public class Attendance {
    @SerializedName("Date")
    @Expose
    private Date Date;
    @SerializedName("DidAttend")
    @Expose
    private Boolean DidAttend;


    public ArrayList<Attendance> getAttendance(String classString) {
        Type object = new TypeToken<ArrayList<Attendance>>(){}.getType();
        Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
        ArrayList<Attendance> returnVal= gson.fromJson(classString, object);
        return returnVal;
    }

    public Date getDate() {
        return Date;
    }

    public void setDate(java.util.Date date) {
        Date = date;
    }

    public Boolean getDidAttend() {
        return DidAttend;
    }

    public void setDidAttend(Boolean didAttend) {
        DidAttend = didAttend;
    }
}
