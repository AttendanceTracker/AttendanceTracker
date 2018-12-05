package com.helper.attendence.myapplication;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ListView;

import java.util.ArrayList;

/* File created by: Zack Witherspoon
/  Description: This class is a simple class that inflates the layout of the attendance for the
/      student
*/

public class OuterList extends AppCompatActivity {
    @Override

    //Function created by: Zack Witherspoon
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.viewcontents_layout);
        setTitle(getIntent().getStringExtra("className"));
        String className= getIntent().getStringExtra("className");

        Long classId= getIntent().getLongExtra("classId", -1L);
        Long studentId = getIntent().getLongExtra("studentId", -1L);

        System.out.println("Classname: " + className + " id: " + classId);
        Attendance att = new Attendance();
        ArrayList<Attendance> uL= att.getAttendance(studentId, classId);
        AttendanceAdapter adapter =  new AttendanceAdapter(this,R.layout.list_adapter_view, uL);
        ListView listView = (ListView) findViewById(R.id.listView);
        listView.setAdapter(adapter);
        }

}