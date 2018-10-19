package com.helper.attendence.myapplication;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Boolean DEBUG = false;  //Debug for toggling seen before or new user
        // Get the app's shared preferences
        final SharedPreferences app_preferences =
                PreferenceManager.getDefaultSharedPreferences(this);

        // Get the too see if logged in before
        Boolean newDeviceFlag = app_preferences.getBoolean("deviceFlag", false);

        //Should show 4 boxes to enter the fname, lname, username, and CWID
        if (!newDeviceFlag) {
            setContentView(R.layout.info_logging);
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
                    displayInfo(); //displays this info on activity_main
                }
            });
        } else {
            if(DEBUG) {
                SharedPreferences.Editor editor = app_preferences.edit();
                editor.putBoolean("deviceFlag", false);
                editor.apply(); // Very important
            }
            TextView text = (TextView) findViewById(R.id.txtCount);
            text.setText("Welcome. Your info is stored.\n");
            displayInfo();
        }
        displayInfo();
        mainMenu();
    }

    //displays this fName, lName, userName, CWID, counter (for fun)  on activity_main
    public void displayInfo() {
        SharedPreferences app_preferences1 =
                PreferenceManager.getDefaultSharedPreferences(this);
        setContentView(R.layout.activity_main);
        int counter = app_preferences1.getInt("counter", 0);
        String fName = app_preferences1.getString("fName", "null");
        String lName= app_preferences1.getString("lName", "null");
        String userName = app_preferences1.getString("userName", "null");
        String CWID = app_preferences1.getString("CWID", "null");
        TextView text = (TextView) findViewById(R.id.txtCount);
        text.setText("This app has been started " + ++counter + " times." + "\nFirst name =" + fName + "\n Last name =" + lName + "\n Username =" + userName + "\n CWID =" + CWID + ". ");
    }

    public void mainMenu() {
         FloatingActionButton qrBtn = (FloatingActionButton) findViewById(R.id.qrScanner_button);
       qrBtn.setOnClickListener(new View.OnClickListener() {
            //Open qr library and go from there
            @Override
            public void onClick(View view) {
//              Snackbar.make(view, "Scan a QR code", Snackbar.LENGTH_LONG).setAction("Action", null).show();
                TextView text = (TextView) findViewById(R.id.txtCount);
                text.setText("This is the QR Code Section of the app.\n");
            }
        });

        FloatingActionButton settingsBtn = (FloatingActionButton) findViewById(R.id.settings_menu_button);
        settingsBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(MainActivity.this, Settings.class);
                startActivity(i);
            }
        });
//        displayInfo();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }


    public static String getUniqueIMEIId(Context context) {
        return "not_found";
    }

}