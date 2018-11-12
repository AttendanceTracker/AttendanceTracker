package com.helper.attendence.myapplication;

import android.Manifest;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.os.StrictMode;
import android.provider.MediaStore;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.FileProvider;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.util.SparseArray;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.vision.Frame;
import com.google.android.gms.vision.barcode.Barcode;
import com.google.android.gms.vision.barcode.BarcodeDetector;

import java.io.File;
import java.io.FileNotFoundException;

public class QRScanner extends AppCompatActivity {

    private static final String LOG_TAG = "Barcode Scanner API";

    private static final int PHOTO_REQUEST = 10;

    private TextView scanResults;
    private BarcodeDetector detector;
    private Uri imageUri;


    private static final int REQUEST_WRITE_PERMISSION = 20;
    private static final int REQUEST_FINE_LOCATION = 30;
    private static final String SAVED_INSTANCE_URI = "uri";
    private static final String SAVED_INSTANCE_RESULT = "result";

    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.qr_scanner);

        TextView text = (TextView) findViewById(R.id.txt_info);
        text.setText("This is the QR Code Section of the app.\n");

        Button button = (Button) findViewById(R.id.button);
        scanResults = (TextView) findViewById(R.id.scan_results);
        if (savedInstanceState != null) {
            imageUri = Uri.parse(savedInstanceState.getString(SAVED_INSTANCE_URI));
            scanResults.setText(savedInstanceState.getString(SAVED_INSTANCE_RESULT));
        }

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ActivityCompat.requestPermissions(QRScanner.this, new
                        String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE}, REQUEST_WRITE_PERMISSION);

                ActivityCompat.requestPermissions(QRScanner.this, new
                        String[]{Manifest.permission.ACCESS_FINE_LOCATION}, REQUEST_FINE_LOCATION);

            }

        });

        detector = new BarcodeDetector.Builder(getApplicationContext())
                .setBarcodeFormats(Barcode.DATA_MATRIX | Barcode.QR_CODE)
                .build();
        if (!detector.isOperational()) {
            scanResults.setText("Could not set up the detector!");
            return;
        }
        checkLocationPermission();
    }

    public boolean checkLocationPermission() {
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED) {


            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                    REQUEST_FINE_LOCATION);
            return false;
        } else {
            return true;
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        switch (requestCode) {
            case REQUEST_WRITE_PERMISSION:
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    takePicture();
                } else {
                    Toast.makeText(QRScanner.this, "Permission Denied!", Toast.LENGTH_SHORT).show();
                }
            case REQUEST_FINE_LOCATION:
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {

                    if (ContextCompat.checkSelfPermission(this,
                            Manifest.permission.ACCESS_FINE_LOCATION)
                            == PackageManager.PERMISSION_GRANTED) {

                    }

                }

        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        Log.d(LOG_TAG, "\n\t Entered OnActivityResult");
        if (requestCode == PHOTO_REQUEST && resultCode == RESULT_OK) {
            Log.d(LOG_TAG, "\t\t Launching MediaScanIntent");
            launchMediaScanIntent();
            try {
                Log.d(LOG_TAG, "\t\t\t Trying to Decode...");
                Bitmap bitmap = decodeBitmapUri(this, imageUri);
                if (detector.isOperational() && bitmap != null) {
                    Log.d(LOG_TAG, "\t\t\t\t Working detector, bitmap is not null");
                    Frame frame = new Frame.Builder().setBitmap(bitmap).build();
                    SparseArray<Barcode> barcodes = detector.detect(frame);
                    Log.d(LOG_TAG, "\t\t\t\t Frame created, barcode array created, size is: " + barcodes.size());
                    for (int index = 0; index < barcodes.size(); index++) {
                        Barcode code = barcodes.valueAt(index);
                        scanResults.setText(scanResults.getText() + code.displayValue + "\n");

                        //Required only if you need to extract the type of barcode
                        int type = barcodes.valueAt(index).valueFormat;
                        switch (type) {

                            case Barcode.TEXT:
                                Log.d(LOG_TAG, "\t\t\t\t\t case: Text");
                                Log.i(LOG_TAG, code.rawValue);

                                //call send check-in
                                checkIn(code.rawValue);
                                break;

                            default:
                                Log.d(LOG_TAG, "\t\t\t\t\t default case: Raw Value");
                                Log.i(LOG_TAG, code.rawValue);
                                break;
                        }
                    }
                    if (barcodes.size() == 0) {
                        scanResults.setText("Scan Failed: Found nothing to scan");
                    }
                } else {
                    scanResults.setText("Could not set up the detector!");
                }
            } catch (Exception e) {
                Toast.makeText(this, "Failed to load Image", Toast.LENGTH_SHORT)
                        .show();
                Log.e(LOG_TAG, e.toString());
            }
        }
    }

    private void takePicture() {

        //Protects photo from exposure to other apps
        StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
        StrictMode.setVmPolicy(builder.build());

        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);

        imageUri = Uri.fromFile(new File(Environment.getExternalStorageDirectory(),
                "tmp_avatar_" + String.valueOf(System.currentTimeMillis()) + ".jpg"));

        Log.d(LOG_TAG, "image URI created: " + imageUri.getPath());
        intent.putExtra(android.provider.MediaStore.EXTRA_OUTPUT, imageUri);
        intent.putExtra("return-data", true);
        Log.d(LOG_TAG, "finished putExtra, starting activity, requesting photo");
        startActivityForResult(intent, PHOTO_REQUEST);
    }

    private void checkIn(String rawValue){

        LocationManager lm = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }
        Location location = lm.getLastKnownLocation(LocationManager.GPS_PROVIDER);
        Double longitude = 78.9789;
        Double latitude = 87.9898;
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


    @Override
    protected void onSaveInstanceState(Bundle outState) {
        if (imageUri != null) {
            outState.putString(SAVED_INSTANCE_URI, imageUri.toString());
            outState.putString(SAVED_INSTANCE_RESULT, scanResults.getText().toString());
        }
        super.onSaveInstanceState(outState);
    }

    private void launchMediaScanIntent() {
        Intent mediaScanIntent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
        mediaScanIntent.setData(imageUri);
        this.sendBroadcast(mediaScanIntent);
    }

    private Bitmap decodeBitmapUri(Context ctx, Uri uri) throws FileNotFoundException {
        int targetW = 600;
        int targetH = 600;
        BitmapFactory.Options bmOptions = new BitmapFactory.Options();
        bmOptions.inJustDecodeBounds = true;
        BitmapFactory.decodeStream(ctx.getContentResolver().openInputStream(uri), null, bmOptions);
        int photoW = bmOptions.outWidth;
        int photoH = bmOptions.outHeight;

        int scaleFactor = Math.min(photoW / targetW, photoH / targetH);
        bmOptions.inJustDecodeBounds = false;
        bmOptions.inSampleSize = scaleFactor;

        return BitmapFactory.decodeStream(ctx.getContentResolver()
                .openInputStream(uri), null, bmOptions);
    }


}