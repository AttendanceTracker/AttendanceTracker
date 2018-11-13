package com.helper.attendence.myapplication;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.content.Context;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class InfoLogging extends Activity {

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.info_logging);
        System.out.println("Looking at shared preferences");
        final SharedPreferences pref = this.getSharedPreferences("MAIN_ACTIVITY", Context.MODE_PRIVATE);

        final Long imeiNumber = pref.getLong("storedIMEINumber", -1L);
        System.out.println("imeiNum =" + imeiNumber);
        Button clickButton = (Button) findViewById(R.id.submitBtn);
        clickButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String fn = "";
                String ln = "";
                String email = "";
                Long cwid = -1L;
                EditText mEdit = (EditText) findViewById(R.id.firstName);
                SharedPreferences.Editor editor = pref.edit();
                fn = mEdit.getText().toString();
                editor.putString("storedFirstName", fn);
                mEdit = (EditText) findViewById(R.id.lastName);
                ln = mEdit.getText().toString();
                editor.putString("storedLastName", ln);
                mEdit = (EditText) findViewById(R.id.email);
                email = mEdit.getText().toString();
                editor.putString("storedEmail", email);
                mEdit = (EditText) findViewById(R.id.CWID);
                cwid = Long.parseLong(mEdit.getText().toString());
                editor.putLong("storedCwid", cwid);
                editor.putBoolean("deviceFlag", false); //set's boolean to True bc user has been seen before
                editor.apply(); // Very important
                System.out.println("Checking getStudent");
                Student std = new Student(fn, ln, email, cwid, imeiNumber);
                System.out.println("Created my student");
                std.printAll();
                System.out.println("Getting student");
                std = std.getStudent(std);
                if(std.equals(null)) {
                    System.out.println("Dood is null ");
                }
                else {
                    System.out.println("Dood is not null");
                }
                std.printAll();
                std.getStudentDevice().registerDevice(std.getCwid(), imeiNumber);
                Intent i = new Intent(InfoLogging.this, MainActivity.class);
                startActivity(i);
            }
        });

    }
}
