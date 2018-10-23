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


public class EditUserInformation extends Activity{



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.edit_user_information);

        FloatingActionButton backBtn = (FloatingActionButton) findViewById(R.id.backBtn);
        backBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(EditUserInformation.this, Settings.class);
                startActivity(i);
            }
        });

        TextView prompt = (TextView) findViewById(R.id.txt_prompt);



        final SharedPreferences app_preferences =
                PreferenceManager.getDefaultSharedPreferences(this);
        @SuppressLint("CommitPrefEdits") SharedPreferences.Editor editor = app_preferences.edit();

        String fName = app_preferences.getString("fName", "null");
        String lName= app_preferences.getString("lName", "null");

        TextView previousValues = (TextView) findViewById(R.id.txt_prev_values);
        previousValues.setText("Current First Name: " + fName + "\nCurrent Last Name: " + lName + "\n");

        Button confirmButton = (Button) findViewById(R.id.confirm_button);
        confirmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SharedPreferences.Editor editor = app_preferences.edit();
                EditText mEdit;

                mEdit = (EditText) findViewById(R.id.firstName);
                editor.putString("fName", mEdit.getText().toString());

                mEdit = (EditText) findViewById(R.id.lastName);
                editor.putString("lName", mEdit.getText().toString());

                editor.apply();

                Intent i = new Intent(EditUserInformation.this, MainActivity.class);
                startActivity(i);
            }
        }) ;





    }



}