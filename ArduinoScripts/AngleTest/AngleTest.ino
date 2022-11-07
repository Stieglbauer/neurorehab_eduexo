#include <Servo.h>
Servo myservo;

int positionDesired = 0x00;
int posIs;
int servoAnalogInPin = A0;

void setup(){
  Serial.begin(9600);
  //myservo.attach(3);
}

int getAngle(int sensorAngle) {
  return (int)(180 * ((posIs%0xFC00 - 113) / (float)(469 - 113))) % 360;
}

void loop(){
  positionDesired++;
  if(positionDesired > 360 || positionDesired < -360) {
    positionDesired = -positionDesired;
  }
  //myservo.write(positionDesired);
  posIs = analogRead(servoAnalogInPin);
  Serial.print(posIs);
  Serial.print(" -> ");
  posIs = getAngle(posIs);
  Serial.println(posIs);
  delay(100);
}