package com.helper.attendence.myapplication;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class sClass {
    @SerializedName("ID")
    @Expose
    private Long ID;
    @SerializedName("Name")
    @Expose
    private String Name;
    @SerializedName("TeacherID")
    @Expose
    private Long TeacherID;


    public Long getID() {
        return ID;
    }

    public void setID(Long ID) {
        this.ID = ID;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public Long getTeacherID() {
        return TeacherID;
    }

    public void setTeacherID(Long teacherID) {
        TeacherID = teacherID;
    }

}