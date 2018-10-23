package com.helper.attendence.myapplication;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class info_logging extends Activity {

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.info_logging);
        final SharedPreferences app_preferences =
                PreferenceManager.getDefaultSharedPreferences(this);
        Button clickButton = (Button) findViewById(R.id.button2);
        clickButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                EditText mEdit = (EditText) findViewById(R.id.firstName);
                SharedPreferences.Editor editor = app_preferences.edit();
                editor.putString("fName", mEdit.getText().toString());
                mEdit = (EditText) findViewById(R.id.lastName);
                editor.putString("lName", mEdit.getText().toString());
                mEdit = (EditText) findViewById(R.id.username);
                editor.putString("userName", mEdit.getText().toString());
                mEdit = (EditText) findViewById(R.id.CWID);
                editor.putString("CWID", mEdit.getText().toString());
                editor.putBoolean("deviceFlag", true); //set's boolean to True bc user has been seen before
                editor.apply(); // Very important
//                    displayInfo(app_preferences); //displays this info on activity_main
                Intent i = new Intent(info_logging.this, MainActivity.class);
                startActivity(i);
            }
        });
    }
}
