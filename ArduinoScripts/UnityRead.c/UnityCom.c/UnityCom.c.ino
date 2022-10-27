#include<Servo.h>

Servo myServo;

int servoAnalogInPin = A0;
int motorPosition;
char byteBuffer = 0;
int positionDesired;
int posIs;

void setup(){
  myServo.attach(3);
  Serial.begin(9600);
}

void sendDataByte(size_t dataByte, bool isFirst) {
  if(!isFirst && dataByte == 0xFF) {
    // make sure non-first byte is never 0xFF
    dataByte = 0xFE;
  }
  Serial.write(dataByte);
}

void sendDataByte(size_t dataByte) {
  sendDataByte(dataByte, false);
}

int getAngle(int sensorAngle) {
  return (int)(180 * ((sensorAngle%0xFC00 - 113) / (float)(469 - 113))) % 360;
}

void loop() {
  posIs = getAngle(analogRead(servoAnalogInPin));

  // begin sequence:
  size_t dataByte = 0xFF;
  sendDataByte(dataByte, true);
  dataByte = posIs % 256;
  sendDataByte(dataByte);
  dataByte = posIs / 256;
  sendDataByte(dataByte);
  Serial.flush();


  Serial.readBytes(&byteBuffer, 1);
  myServo.write(byteBuffer==1?0:360);
  
  delay(50);
}