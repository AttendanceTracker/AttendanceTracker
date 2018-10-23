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
            Intent i = new Intent(MainActivity.this, info_logging.class);
            startActivity(i);
        } else {
//            if(DEBUG) {
//                SharedPreferences.Editor editor = app_preferences.edit();
//                editor.putBoolean("deviceFlag", false);
//                editor.apply(); // Very important
//            }
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
        int counter = app_preferences.getInt("counter", 0);
        String fName = app_preferences.getString("fName", "null");
        String lName= app_preferences.getString("lName", "null");
        String userName = app_preferences.getString("userName", "null");
        String CWID = app_preferences.getString("CWID", "null");
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