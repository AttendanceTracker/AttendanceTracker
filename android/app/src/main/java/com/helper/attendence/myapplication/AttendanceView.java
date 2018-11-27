package com.helper.attendence.myapplication;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.GridLayout;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import java.util.Date;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class AttendanceView extends AppCompatActivity {


        @Override
        protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.attendance_view);
            Activity activity;
            activity=this;
            String className= getIntent().getStringExtra("className");
            Long id= getIntent().getLongExtra("classId", -1L);
            System.out.println("Classname: " + className + " id: " + id);
            Attendance att = new Attendance();

            GridLayout tableLayout = (GridLayout)findViewById(R.id.table);
//            tableLayout.removeAllViews();
            ArrayList<Attendance> x = att.getAttendance("[  {    \"Date\": \"2018-11-12T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-14T00:00:00\",    \"DidAttend\": false  },  {    \"Date\": \"2018-11-16T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-19T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-21T00:00:00\",    \"DidAttend\": true  }]");
            System.out.println();
            SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            for(int i = 0; i < x.size(); i++) {
                System.out.println("On " + i + " with date: " + x.get(i).getDate() + " and attendance: " + x.get(i).getDidAttend());

                LinearLayout mContainerView = (LinearLayout)findViewById(R.id.attendanceLL);
                LayoutInflater inflater =(LayoutInflater)getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                View myView = inflater.inflate(R.layout.attendance_view, null);

                String strDate = formatter.format(x.get(i).getDate());
                System.out.println("Date: " + strDate);
                TextView text = (TextView) findViewById(R.id.textAttendanceID);
                text.setText(strDate);
                TextView texts = (TextView) findViewById(R.id.textAttendanceID);
                texts.setText(x.get(i).getDidAttend().toString());

                mContainerView.addView(myView);
            }

        }

        @Override
        public boolean onCreateOptionsMenu(Menu menu) {
            // Inflate the menu; this adds items to the action bar if it is present.
            getMenuInflater().inflate(R.menu.menu_main, menu);
            return true;
        }

        @Override
        public boolean onOptionsItemSelected(MenuItem item) {
            return super.onOptionsItemSelected(item);
        }

    }

