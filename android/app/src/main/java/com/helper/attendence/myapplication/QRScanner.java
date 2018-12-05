package com.helper.attendence.myapplication;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.util.SparseArray;
import android.widget.Toast;
import android.view.SurfaceHolder;
import android.widget.TextView;
import android.view.SurfaceView;

import com.google.android.gms.vision.barcode.Barcode;
import com.google.android.gms.vision.barcode.BarcodeDetector;
import com.google.android.gms.vision.Detector;
import com.google.android.gms.vision.CameraSource;

import java.io.IOException;

public class QRScanner extends AppCompatActivity {

    SurfaceView surfaceView;
    TextView txtBarcodeValue;
    String intentData = "";
    TextView text;

    private CameraSource cameraSource;
    private BarcodeDetector detector;

    private static final String LOG_TAG = "Barcode Scanner API";
    private static final int REQUEST_PERMISSION_CAMERA = 333;


    @Override
    protected void onCreate(Bundle savedInstanceState) {



        super.onCreate(savedInstanceState);
        setContentView(R.layout.qr_scanner);
        setTitle("");

        initViews();
    }

    private void initViews() {
        txtBarcodeValue = findViewById(R.id.txt_barcode_value);

        if (intentData.length() > 0) {
            startActivity(new Intent(QRScanner.this, QRActivity.class).putExtra("code", intentData));
        }
        else{
            initialiseDetectorsAndSources();
        }
    }

    @Override
    protected void onPause() {
        super.onPause();
        cameraSource.release();
    }


    @Override
    protected void onResume() {
        super.onResume();
        initialiseDetectorsAndSources();
    }

    private void initialiseDetectorsAndSources() {

        surfaceView = findViewById(R.id.surfaceView);

        Toast.makeText(getApplicationContext(), "Barcode scanner started", Toast.LENGTH_SHORT).show();

        detector = new BarcodeDetector.Builder(this)
                .setBarcodeFormats(Barcode.ALL_FORMATS)
                .build();

        cameraSource = new CameraSource.Builder(this, detector)
                .setRequestedPreviewSize(1920, 1080)
                .setAutoFocusEnabled(true) //you should add this feature
                .build();
        Log.d(LOG_TAG, "INITIALIZED THE DETECTOR AND SOURCE");
        surfaceView.getHolder().addCallback(new SurfaceHolder.Callback() {
            @Override
            public void surfaceCreated(SurfaceHolder holder) {
                try {
                    if (ActivityCompat.checkSelfPermission(QRScanner.this, Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED) {
                        Log.d(LOG_TAG, "ALREAYD GOT CAMERA PERMISSIONS BIAAATCH");
                        cameraSource.start(surfaceView.getHolder());
                        Log.d(LOG_TAG, "CAMERA SOURCE STARTED");
                    } else {
                        ActivityCompat.requestPermissions(QRScanner.this, new
                                String[]{Manifest.permission.CAMERA}, REQUEST_PERMISSION_CAMERA);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {}
            @Override
            public void surfaceDestroyed(SurfaceHolder holder) {cameraSource.stop();}
        });

        detector.setProcessor(new Detector.Processor<Barcode>() {
            @Override
            public void release() {
                Toast.makeText(getApplicationContext(), "Barcode scanner has been stopped.", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void receiveDetections(Detector.Detections<Barcode> detections) {
                final SparseArray<Barcode> barcodes = detections.getDetectedItems();
                if (barcodes.size() != 0) {
                    txtBarcodeValue.post(new Runnable() {
                        @Override
                        public void run() {
                            Log.d(LOG_TAG, "BARCODE FOUND, DOING RUNNABLE");
                            txtBarcodeValue.removeCallbacks(null);
                            intentData = barcodes.valueAt(0).rawValue;
                            initViews();
                        }
                    });
                }
            }
        });
    }

}