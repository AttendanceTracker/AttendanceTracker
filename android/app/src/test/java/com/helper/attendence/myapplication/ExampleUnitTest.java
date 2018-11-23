package com.helper.attendence.myapplication;

import org.junit.Test;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.reflect.TypeToken;
import java.lang.reflect.Type;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import static org.junit.Assert.*;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {
    @Test
    public void addition_isCorrect() {assertEquals(4, 2 + 2); }

    @Test
    public void testGetClasses() {
        Type x = new TypeToken<ArrayList<sClass>>(){}.getType();
        String testString = "[  {    \"ID\": 4,    \"Name\": \"Test Class\",    \"TeacherID\": 99999999  },  {    \"ID\": 5,    \"Name\": \"adf100\",    \"TeacherID\": 93847274  }]";
        Gson gson = new Gson();
        ArrayList<sClass> object = gson.fromJson(testString, x);

        assertEquals((Long)4L, object.get(0).getID());
        assertEquals("Test Class", object.get(0).getName());
        assertEquals((Long)99999999L, object.get(0).getTeacherID());
    }

    @Test
    public void testGetAttendance() {
        Type object = new TypeToken<ArrayList<Attendance>>(){}.getType();
        String testString = "[  {    \"Date\": \"2018-11-12T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-14T00:00:00\",    \"DidAttend\": false  },  {    \"Date\": \"2018-11-16T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-19T00:00:00\",    \"DidAttend\": true  },  {    \"Date\": \"2018-11-21T00:00:00\",    \"DidAttend\": true  }]";
        Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss").create();
        ArrayList<Attendance> returnVal= gson.fromJson(testString, object);
        String sDate1="2018-11-12T00:00:00";
        Date date1 = new Date();
        try {
            date1=new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss").parse(sDate1);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        System.out.println(returnVal.get(0).getDidAttend());
        System.out.println(returnVal.get(0).getDate());
        assertEquals(date1, returnVal.get(0).getDate());
        assertEquals(true, returnVal.get(0).getDidAttend());
    }
}