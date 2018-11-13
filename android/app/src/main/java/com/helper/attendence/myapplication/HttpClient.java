package com.helper.attendence.myapplication;

import android.util.Log;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.net.ssl.HttpsURLConnection;

import static android.support.constraint.Constraints.TAG;

public class HttpClient implements Serializable{

    private String baseURL = "https://attendancetracker.us";

    public String getCall(String apiString, String parameters) {
        String urlString = baseURL + apiString + "?" + parameters;
        System.out.println("StringBoi: " + urlString);
        String response = "";
        try {
            URL obj = new URL(urlString);
            HttpURLConnection con = (HttpURLConnection) obj.openConnection();
            con.setRequestMethod("GET");
            con.setRequestProperty("User-Agent", "Mozilla/5.0");
            int responseCode = con.getResponseCode();
            System.out.println("\nSending 'GET' request to URL : " + "https://attendancetracker.us/api/Student/Get");
            System.out.println("Response Code : " + responseCode);
            if(responseCode == 200) {
                Log.i(TAG, "getCall returned with 200 response code");
                BufferedReader in = new BufferedReader(
                        new InputStreamReader(con.getInputStream()));
                String inputLine;
                StringBuffer buff = new StringBuffer();
                while ((inputLine = in.readLine()) != null) {
                    buff.append(inputLine);
                }
                System.out.println(buff.toString());
                in.close();
                response = buff.toString();
            }
            else {
                Log.d(TAG, "ERROR, getCall returned with response code " + Integer.toString(responseCode));
                response = "Failed";
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        return response;
    }

    private String getPostDataString(HashMap<String, String> params) throws UnsupportedEncodingException{
        StringBuilder result = new StringBuilder();
        boolean first = true;
        for(Map.Entry<String, String> entry : params.entrySet()){
            if (first)
                first = false;
            else
                result.append("&");

            result.append(URLEncoder.encode(entry.getKey(), "UTF-8"));
            result.append("=");
            result.append(URLEncoder.encode(entry.getValue(), "UTF-8"));
        }
        return result.toString();
    }



    public String postCall(String apiString, HashMap<String, String> postDataParams) {
        URL url;
        String response = "";

        String urlString = baseURL + apiString;
        System.out.println("StringBoi= " + urlString);
        try {
            url = new URL(urlString);

            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setReadTimeout(15000);
            conn.setConnectTimeout(15000);
            conn.setRequestMethod("POST");
            conn.setDoInput(true);
            conn.setDoOutput(true);


            OutputStream os = conn.getOutputStream();
            BufferedWriter writer = new BufferedWriter(
                    new OutputStreamWriter(os, "UTF-8"));
            writer.write(getPostDataString(postDataParams));

            writer.flush();
            writer.close();
            os.close();
            int responseCode=conn.getResponseCode();

            if (responseCode == 200) {
                Log.i(TAG, "pstCall returned with 200 response code");
                String line;
                BufferedReader br=new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line=br.readLine()) != null) {
                    response+=line;
                }
            }
            else if(responseCode == 400) {
                response = "Already Exists";
            }
            else {
                Log.d(TAG, "ERROR, postCall returned with response code " + Integer.toString(responseCode));
                response = "Failed";

            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return response;
    }

    public String putCall(String apiString, String urlParameters, HashMap<String, String> putDataParams) {
        URL url;
        String response = "";

        String urlString = baseURL + apiString + "?" + urlParameters;
        System.out.println("StringBoi= " + urlString);
        try {
            url = new URL(urlString);

            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setReadTimeout(15000);
            conn.setConnectTimeout(15000);
            conn.setRequestMethod("PUT");
            conn.setDoInput(true);
            conn.setDoOutput(true);

            OutputStream os = conn.getOutputStream();
            BufferedWriter writer = new BufferedWriter(
                    new OutputStreamWriter(os, "UTF-8"));
            writer.write(getPostDataString(putDataParams));

            writer.flush();
            writer.close();
            os.close();
            int responseCode=conn.getResponseCode();
            System.out.println("Response code= " + responseCode);
            if (responseCode == 200) {
                Log.i(TAG, "getCall returned with 200 response code");
                String line;
                BufferedReader br=new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line=br.readLine()) != null) {
                    response+=line;
                }
            }
            else if(responseCode == 400) {
                response = "Already Exists";
            }
            else {
                Log.d(TAG, "ERROR, getCall returned with response code " + Integer.toString(responseCode));
                response = "Failed";
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return response;
    }
}
