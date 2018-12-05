package com.helper.attendence.myapplication;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

//https://developer.android.com/guide/topics/ui/declaring-layout#java

public class MainActivity extends AppCompatActivity {

    private static final int PERMISSIONS_REQUEST_READ_PHONE_STATE = 999;
    private TelephonyManager mTelephonyManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        String IMEINumber = "3015";
        Student std = new Student(Long.parseLong(IMEINumber));
        //CHECK TO SEE IF THIS DEVICE HAS BEEN SEEN HERE WITH A REST CALL.

        // Get the app's shared preference
        SharedPreferences pref = this.getSharedPreferences("MAIN_ACTIVITY", Context.MODE_PRIVATE);
        Boolean newDeviceFlag = pref.getBoolean("deviceFlag", true);
        SharedPreferences.Editor editor = pref.edit();
        editor.putLong("storedIMEINumber", Long.parseLong(IMEINumber)); // value to store
        editor.apply();

        if(newDeviceFlag) {
            Long cwid = std.getCwidFromDevice(std.getImei());
            System.out.println("Found CWID = " + cwid);
            if (cwid.equals(-1L)) //IMEI not attached to any student
            {
                System.out.println("In no cwid found for device");
                //switch to infoLogging and create new student
                // get info
                //check if student exists already
                    //if he do then register the device to him
                    //if he don't then create him then register device to him :)
                System.out.println("ENTERING INFOLOGGING()");
                Intent i = new Intent(MainActivity.this, InfoLogging.class);
                startActivity(i);
            }
            else { //get student from cwid and move on, don'go to infoLogging
                std.setCwid(cwid);
                std.setImei(Long.parseLong(IMEINumber));
                std.printAll();
                System.out.println("GOING INTO THE MATRIX");
                std = std.getStudent(std);
                System.out.println("JUST GOT OUT OF MATRIX");
                std.printAll();
            }
                //Save shared preferences off
                SharedPreferences.Editor edit = pref.edit();
                edit.putString("storedFirstName", std.getFname());
                edit.putString("storedLastName", std.getLname());
                edit.putString("storedEmail", std.getEmail());
                edit.putLong("storedCwid", cwid);
                edit.putBoolean("deviceFlag", true); //set's boolean to True bc user has been seen before
                edit.apply(); // Very important
        }

        String firstName = pref.getString("storedFirstName", "ERROR");
        String lastName = pref.getString("storedLastName", "ERROR");
        String email = pref.getString("storedEmail", "ERROR");
        Long cwid = pref.getLong("storedCwid", -1L);

        Student x = new Student(firstName, lastName, email, cwid, Long.parseLong(IMEINumber));
        x.printAll();

        //NEED TO IMPLEMENT GETCLASSES
        x.setClasses();
//        System.out.println("Final Value out of class: : " + x.getClasses().get(0).getID() + " " + x.getClasses().get(0).getName() + " " + x.getClasses().get(0).getTeacherID());
        mainMenu(x);

    }

    public void mainMenu(final Student x) {
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
        String[] stringArray;
        stringArray = new String [x.getClasses().size()];
        for(int i = 0; i < x.getClasses().size(); i++)  {
            System.out.println("Class name " + i + ": " + x.getClasses().get(i).getName() );
            stringArray[i] = x.getClasses().get(i).getName();
        }
        ArrayAdapter adapter = new ArrayAdapter<String>(this,
                R.layout.activity_listview, stringArray);

        ListView listView = (ListView) findViewById(R.id.mobile_list);
        listView.setAdapter(adapter);
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> adapter, View v, int position,
                                    long arg3)
            {
                String value = (String)adapter.getItemAtPosition(position);
                System.out.println("You clicked " + value + " at position: " + position);
                Intent i = new Intent(MainActivity.this,OuterList.class);
                i.putExtra("className", value);
                i.putExtra("classId", x.getClasses().get(position).getID());
                i.putExtra("studentId", x.getCwid());
                startActivity(i);
            }
              }
        );
    }


        public void onItemClick(AdapterView parent, View v, int position, long id) {
            // Do something in response to the click
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

    @Override
    public void onResume(){
        super.onResume();
        SharedPreferences pref = this.getSharedPreferences("MAIN_ACTIVITY", Context.MODE_PRIVATE);
        String firstName = pref.getString("storedFirstName", "ERROR");
        String lastName = pref.getString("storedLastName", "ERROR");
        String email = pref.getString("storedEmail", "ERROR");
        Long cwid = pref.getLong("storedCwid", -1L);

        Student x = new Student(firstName, lastName, email, cwid);
        x.printAll();

        //NEED TO IMPLEMENT GETCLASSES
        x.setClasses();
//        System.out.println("Final Value out of class: : " + x.getClasses().get(0).getID() + " " + x.getClasses().get(0).getName() + " " + x.getClasses().get(0).getTeacherID());
        mainMenu(x);

    }


}