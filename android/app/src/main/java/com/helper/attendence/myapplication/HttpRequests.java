package com.helper.attendence.myapplication;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.Map;

public class HttpRequests {

//    private String baseUrl = "https://attendancetracker.us/";
//    private StringBuffer content = new StringBuffer();
//    private BufferedReader in = null;
//    private HttpURLConnection con = null;
//
//    public StringBuffer getCall(String apiUrl, Map<String, String> parameters) {
//        String finalUrl = baseUrl + apiURL;
//        URL url = new URL(finalUrl);
//        try {
//
//            con = (HttpURLConnection) url.openConnection();
//            con.setRequestMethod("GET");
//            con.setRequestProperty("Content-Type", "application/json");
//            con.setConnectTimeout(5000);
//
//            con.setDoOutput(true);
//            DataOutputStream out = new DataOutputStream(con.getOutputStream());
//            out.writeBytes(getParamsString(parameters));
//            out.flush();
//            out.close();
//
//            int status = con.getResponseCode();
//
//            in = new BufferedReader(
//                    new InputStreamReader(con.getInputStream()));
//            String inputLine;
//            while ((inputLine = in.readLine()) != null) {
//                content.append(inputLine);
//            }
//        }
//        catch (Exception e)
//        {
//            e.printStackTrace();
//            throw e;
//        }
//        finally
//        {
//            // close the reader; this can throw an exception too, so
//            // wrap it in another try/catch block.
//            if (in != null)
//            {
//                try
//                {
//                    in.close();
//                }
//                catch (IOException ioe)
//                {
//                    ioe.printStackTrace();
//                }
//            }
//        }
//
//        return content;
//    }
//
//    public static String getParamsString(Map<String, String> params)
//            throws UnsupportedEncodingException {
//        StringBuilder result = new StringBuilder();
//
//        for (Map.Entry<String, String> entry : params.entrySet()) {
//            result.append(URLEncoder.encode(entry.getKey(), "UTF-8"));
//            result.append("=");
//            result.append(URLEncoder.encode(entry.getValue(), "UTF-8"));
//            result.append("&");
//        }
//
//        String resultString = result.toString();
//        return resultString.length() > 0
//                ? resultString.substring(0, resultString.length() - 1)
//                : resultString;
//    }

}
