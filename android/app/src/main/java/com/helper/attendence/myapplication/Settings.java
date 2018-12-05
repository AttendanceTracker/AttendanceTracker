package com.helper.attendence.myapplication;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.w3c.dom.Text;

/* File created by: Zack Witherspoon
/  Description: This class is a simple class that shows the settings main view
/
*/


public class Settings extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.settings_main);
        setTitle("Settings");
//        FloatingActionButton backBtn = (FloatingActionButton) findViewById(R.id.backBtn);
//        backBtn.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Intent i = new Intent(Settings.this, MainActivity.class);
//                startActivity(i);
//            }
//        });

        //QR Button Commented out, unless we actually want it clickable from the settings menu.
/*
        FloatingActionButton qrBtn = (FloatingActionButton) findViewById(R.id.qrScanner_button);
        qrBtn.setOnClickListener(new View.OnClickListener() {
            //Open qr loibrary and go from there
            @Override
            public void onClick(View view) {
//                Snackbar.make(view, "Scan a QR code", Snackbar.LENGTH_LONG).setAction("Action", null).show();
                TextView text = (TextView) findViewById(R.id.txtCount);
                text.setText("Qr code has been picked boi!");
            }
        });
*/

        Button policyButton = (Button) findViewById(R.id.privacy_policy_button);
        policyButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(Settings.this, PrivacyPolicy.class);
                startActivity(i);
            }
        });

        Button editInformationButton = (Button) findViewById(R.id.edit_information_button);
        editInformationButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(Settings.this, EditUserInformation.class);
                startActivity(i);
            }
        });

        Button licenseButton = (Button) findViewById(R.id.licenses_button);
        licenseButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(Settings.this, License.class);
                startActivity(i);
            }
        });

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
