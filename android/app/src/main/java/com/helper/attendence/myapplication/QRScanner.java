package com.helper.attendence.myapplication;

import android.app.Activity;
import android.os.Bundle;
import android.widget.TextView;

public class QRScanner extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.qr_scanner);

        TextView text = (TextView) findViewById(R.id.txt_info);
        text.setText("This is the QR Code Section of the app.\n");

    }

}
