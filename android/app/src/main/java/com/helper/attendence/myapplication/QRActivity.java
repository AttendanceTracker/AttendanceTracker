package com.helper.attendence.myapplication;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.w3c.dom.Text;

public class QRActivity extends AppCompatActivity {

    TextView scanResults;
    Button btnMainMenu;
    String LOG_TAG = "QRActivity:";
    private boolean checkedIn = false;

    private static final int REQUEST_FINE_LOCATION = 30;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        final String intentdata = getIntent().getStringExtra("code");

        checkLocationPermission();

        super.onCreate(savedInstanceState);
        setContentView(R.layout.qr_activity);

        scanResults = findViewById(R.id.scan_results);
        scanResults.setText(intentdata);
        btnMainMenu = findViewById(R.id.btn_main);
        btnMainMenu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!checkedIn) {
                    checkIn(intentdata);
                    btnMainMenu.setText("Return to main menu.");
                    checkedIn = true;
                }
                else{

                    Intent i = new Intent(QRActivity.this, MainActivity.class);
                    startActivity(i);
                }
            }
        });

    }

    public void checkLocationPermission() {
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {

            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION},REQUEST_FINE_LOCATION);
        }
    }

    private void checkIn(String rawValue){

        LocationManager lm = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        Location location = lm.getLastKnownLocation(LocationManager.GPS_PROVIDER);
        Double longitude = location.getLongitude();
        Double latitude = location.getLatitude();
        Log.d(LOG_TAG,  latitude + " " + longitude);
        System.out.println(" Lat: " + latitude + " Long: " + longitude);

        SharedPreferences pref = this.getSharedPreferences("MAIN_ACTIVITY", Context.MODE_PRIVATE);

        //LATER CHECKIN WILL HAVE CLASSID PASSED IN
        Long cwid = pref.getLong("storedCwid", -1L);

        Student student = new Student();
        boolean success = student.checkIn(latitude,longitude, rawValue, cwid);
        if (success){
            scanResults.setText("Check-in successful!");
        }
        else{
            scanResults.setText("Check-in failed.!");
        }
    }

}
