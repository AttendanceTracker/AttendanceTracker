package com.helper.attendence.myapplication;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import static android.content.Context.*;


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


        final SharedPreferences app_preferences = this.getSharedPreferences("MAIN_ACTIVITY", Context.MODE_PRIVATE);
        @SuppressLint("CommitPrefEdits") SharedPreferences.Editor editor = app_preferences.edit();

        String firstName = app_preferences.getString("storedFirstName", "ERROR");
        String lastName = app_preferences.getString("storedLastName", "ERROR");
        String email = app_preferences.getString("storedEmail", "ERROR");
        Long cwid = app_preferences.getLong("storedCwid", -1L);

        final Student x = new Student(firstName, lastName, email, cwid);

        TextView previousValues = (TextView) findViewById(R.id.txt_prev_values);
        previousValues.setText("Current First Name: " + firstName + "\nCurrent Last Name: " + lastName + "\n");

        Button confirmButton = (Button) findViewById(R.id.confirm_button);
        confirmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                SharedPreferences.Editor editor = app_preferences.edit();
                EditText mEdit;
                EditText mEdit2;

                mEdit = (EditText) findViewById(R.id.firstName);
                editor.putString("storedFirstName", mEdit.getText().toString());
                System.out.println(mEdit.toString());
                mEdit2 = (EditText) findViewById(R.id.lastName);
                System.out.println(mEdit2.toString());
                editor.putString("storedLastName", mEdit.getText().toString());
                System.out.println("Changing stoof....");
                if(x.updateStudent(x, mEdit.getText().toString(), mEdit2.getText().toString(), x.getEmail(), x.getCwid())) {
                    editor.apply();
                    TextView changedValues = (TextView) findViewById(R.id.txt_prev_values);
                    changedValues.setText("Current First Name: " + x.getFname() + "\nCurrent Last Name: " + x.getLname() + "\n");
                }
                else
                {
                    TextView previousValues = (TextView) findViewById(R.id.txt_prompt);
                    previousValues.setText("Error, please change try again.");
                }


                Intent i = new Intent(EditUserInformation.this, MainActivity.class);
                startActivity(i);
            }
        }) ;





    }



}