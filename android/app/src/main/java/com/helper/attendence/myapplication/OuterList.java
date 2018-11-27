package com.helper.attendence.myapplication;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.widget.ListView;

import java.util.ArrayList;

/**
 * Created by Mitch on 2016-05-13.
 */
public class OuterList extends AppCompatActivity {
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.viewcontents_layout);

        String className= getIntent().getStringExtra("className");
        Long id= getIntent().getLongExtra("classId", -1L);
        System.out.println("Classname: " + className + " id: " + id);
        Attendance att = new Attendance();
        ArrayList<Attendance> uL= att.getAttendance("[  {    \"Date\": \"2018-11-12T00:00:00\",    \"DidAttend\": true  },  { \"Date\": \"2018-11-14T00:00:00\",    \"DidAttend\": false  },  {    \"Date\": \"2018-11-16T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-19T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-21T00:00:00\",    \"DidAttend\": true  },  { \"Date\": \"2018-11-23T00:00:00\",    \"DidAttend\": false  }, {    \"Date\": \"2018-11-25T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-28T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-30T00:00:00\",    \"DidAttend\": true  }]");
        AttendanceAdapter adapter =  new AttendanceAdapter(this,R.layout.list_adapter_view, uL);
        ListView listView = (ListView) findViewById(R.id.listView);
        listView.setAdapter(adapter);
        }

}