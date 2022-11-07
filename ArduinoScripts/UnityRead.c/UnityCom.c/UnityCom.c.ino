#include<Servo.h>

Servo myServo;

int servoAnalogInPinAngle = A0;
int servoAnalogInPinForce = A3;
int emgAnalogInPin = A5;
int emgSignal = 0;
int motorPosition;
char byteBuffer = 0;
int positionDesired;
int posIs;
int forceSensor;

int useemg = 0;

void setup(){
  myServo.attach(3);
  Serial.begin(9600);
  myServo.write(0);
  motorPosition = 0;
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
  posIs = getAngle(analogRead(servoAnalogInPinAngle));  
  forceSensor = analogRead(servoAnalogInPinForce);

if(useemg == 0) {
  if(forceSensor > 455) {
    //myServo.detach();
    //delay(1000);
    //myServo.attach(3);
    myServo.write(0);
    motorPosition = 0;
  } else if(forceSensor < 400) {
    //myServo.detach();
    //delay(1000);
    //myServo.attach(3);
    myServo.write(180);
    motorPosition = 180;
  }
} else {
  if(emgSignal > 250) {
myServo.write(0);
motorPosition = 0;
  } else if(emgSignal < 150) {
myServo.write(180);
motorPosition = 180;
  }
}

  
  emgSignal = analogRead(emgAnalogInPin);
  //motorPosition = map(emgSignal, 400, 600, 90, 160);
  //myServo.write(motorPosition);
  //Serial.println(emgSignal);

  // begin sequence:
  size_t dataByte = 0xFF;
  sendDataByte(dataByte, true);
  dataByte = posIs % 256;
  sendDataByte(dataByte);
  dataByte = posIs / 256;
  sendDataByte(dataByte);
  dataByte = forceSensor % 256;
  sendDataByte(dataByte);
  dataByte = forceSensor / 256;
  sendDataByte(dataByte);
  dataByte = emgSignal % 256;
  sendDataByte(dataByte);
  dataByte = emgSignal / 256;
  sendDataByte(dataByte);
  dataByte = motorPosition % 256;
  sendDataByte(dataByte);
  dataByte = motorPosition / 256;
  sendDataByte(dataByte);
  //Serial.println(posIs);
  Serial.flush();


  //Serial.readBytes(&byteBuffer, 1);
  //myServo.write(byteBuffer==1?0:360);
  
  delay(10);
}