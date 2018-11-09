package com.helper.attendence.myapplication;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import java.io.IOException;

import static java.util.logging.Level.FINEST;

public class MainActivity extends AppCompatActivity {

    private static final int PERMISSIONS_REQUEST_READ_PHONE_STATE = 999;
    private TelephonyManager mTelephonyManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


//        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
//        StrictMode.setThreadPolicy(policy);
//        Student x = new Student();
//        try {
//            x = x.getStudent(54321L);
//        } catch (IOException e) {
//            e.printStackTrace();
//        }
//        x.printAll();
//
//        Student dick = new Student();
//        dick = dick.registerStudent("Dick", "VanDyke", "xboob@boob.ua.edu", 100L);
//        dick.printAll();

//        Retrieving device IMEI to make sure it's not a new device
        String IMEINumber = "-1";
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (checkSelfPermission(Manifest.permission.READ_PHONE_STATE)
                    != PackageManager.PERMISSION_GRANTED) {
                requestPermissions(new String[]{Manifest.permission.READ_PHONE_STATE},
                        PERMISSIONS_REQUEST_READ_PHONE_STATE);
            } else {
                IMEINumber = getDeviceImei();
                Log.d("msg", "Final IMEI = " + IMEINumber);
            }
        }
        else {
            Log.d("msg", "ERROR, NOT HIGH ENOUGH API VERSION!");
        }
        //CHECK TO SEE IF THIS DEVICE HAS BEEN SEEN HERE WITH A REST CALL.

        // Get the app's shared preferences
        final SharedPreferences app_preferences =
                PreferenceManager.getDefaultSharedPreferences(this);

        // See if device has been seen before based off of shared Pref. Can prob take this out
        // after IMEI checks are finalized.
        Boolean newDeviceFlag = app_preferences.getBoolean("deviceFlag", false);

        //Should show 4 boxes to enter the fname, lname, username, and CWID
        if (!newDeviceFlag) {
            Intent i = new Intent(MainActivity.this, InfoLogging.class);
            startActivity(i);
        } else {
            TextView text = (TextView) findViewById(R.id.txtCount);
            text.setText("Welcome. Your info is stored.\n");
            displayInfo(app_preferences);
        }
        displayInfo(app_preferences);
        mainMenu();
    }

    //displays this fName, lName, userName, CWID, counter (for fun)  on activity_main
    public void displayInfo(SharedPreferences app_preferences) {
        setContentView(R.layout.activity_main);

        String fName = app_preferences.getString("fName", "null");
        String lName= app_preferences.getString("lName", "null");
        String userName = app_preferences.getString("userName", "null");
        String CWID = app_preferences.getString("CWID", "null");
        TextView text = (TextView) findViewById(R.id.txtCount);
        text.setText("\nFirst name =" + fName + "\n Last name =" + lName + "\n Username =" + userName + "\n CWID =" + CWID + ". ");
    }

    public void mainMenu() {

         FloatingActionButton qrBtn = (FloatingActionButton) findViewById(R.id.qrScanner_button);
       qrBtn.setOnClickListener(new View.OnClickListener() {
            //Open qr library and go from there
            @Override
            public void onClick(View view) {
              Intent i = new Intent(MainActivity.this, QRScanner.class);
              startActivity(i);
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
    public void onRequestPermissionsResult(int requestCode, String[] permissions,
                                           int[] grantResults) {
        if (requestCode == PERMISSIONS_REQUEST_READ_PHONE_STATE
                && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            getDeviceImei();
        }
    }

        @SuppressLint("MissingPermission")
    private String getDeviceImei() {
        Log.d("msg", "MSG: About to find the telephony stoof");
        mTelephonyManager = (TelephonyManager) getSystemService(Context.TELEPHONY_SERVICE);
        String deviceid = null;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O) {
            deviceid = mTelephonyManager.getImei();
        }
        Log.d("msg", "DeviceImei " + deviceid);
        return deviceid;
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

}