package com.example.myzhou.cpbalancer;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorManager;
import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.view.MotionEvent;
import android.view.View;
import android.widget.TextView;
import android.widget.Button;
import com.unity3d.player.UnityPlayer;

public class MainActivity extends AppCompatActivity {

    private SensorManager mSensorManager = (SensorManager)getSystemService(Context.SENSOR_SERVICE);
    private Sensor mSensor = mSensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE);

    // Create a constant to convert nanoseconds to seconds.
    private static final float NS2S = 1.0f / 1000000000.0f;
    private final float[] deltaRotationVector = new float[4];
    private float timestamp;
    TextView thisTextView;
    Button startButton;

    private void defineStartButton(){
        startButton.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                if(event.getAction() == MotionEvent.ACTION_DOWN){
                    //Action Down
                    deltaRotationVector[0] = (float) 0.00000000;
                    deltaRotationVector[1] = (float) 0.00000000;
                    deltaRotationVector[2] = (float) 0.00000000;
                    MainActivity.this.updateTextViewStatus();
                }
                return false;
            }
        });
    }

    public void onSensorChanged(SensorEvent event) {
        // This timestep's delta rotation to be multiplied by the current rotation
        // after computing it from the gyro sample data.
        if (timestamp != 0) {
            final float dT = (event.timestamp - timestamp) * NS2S;
        // Axis of the rotation sample, not normalized yet.
        float axisX = event.values[0];
        float axisY = event.values[1];
        float axisZ = event.values[2];

        // Calculate the angular speed of the sample
        float omegaMagnitude = (float)(Math.sqrt(axisX*axisX + axisY*axisY + axisZ*axisZ));

        // Normalize the rotation vector if it's big enough to get the axis
        // (that is, EPSILON should represent your maximum allowable margin of error)
        if (omegaMagnitude > 1) {
            axisX /= omegaMagnitude;
            axisY /= omegaMagnitude;
            axisZ /= omegaMagnitude;
        }

        // Integrate around this axis with the angular speed by the timestep
        // in order to get a delta rotation from this sample over the timestep
        // We will convert this axis-angle representation of the delta rotation
        // into a quaternion before turning it into the rotation matrix.
        float thetaOverTwo = omegaMagnitude * dT / 2.0f;
        float sinThetaOverTwo = (float)Math.sin(thetaOverTwo);
        float cosThetaOverTwo = (float)Math.cos(thetaOverTwo);
        deltaRotationVector[0] = sinThetaOverTwo * axisX;
        deltaRotationVector[1] = sinThetaOverTwo * axisY;
        deltaRotationVector[2] = sinThetaOverTwo * axisZ;
        deltaRotationVector[3] = cosThetaOverTwo;
        updateTextViewStatus();
    }
    timestamp = event.timestamp;
    if (timestamp > (float) 60.0) {
        UnitySendMessage("Main Camera", "Main Camera", "(x,y,z) rotation vectors: " + deltaRotationVector[0]
                + " " + deltaRotationVector[1] + " " + deltaRotationVector[2]);
        finish();
    }
    float[] deltaRotationMatrix = new float[9];
    SensorManager.getRotationMatrixFromVector(deltaRotationMatrix, deltaRotationVector);

    }
    public void updateTextViewStatus() {
        this.thisTextView.setText(("X-Skew:  " + this.deltaRotationVector[0] + "\nY-Skew:  "
                + this.deltaRotationVector[1] + "\nZ-Skew:  " + this.deltaRotationVector[2]).toString());
    }
}
