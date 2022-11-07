#include<Servo.h>

Servo myServo;

int emgAnalogInPin = A4;
int emgSignal = 0;
int motorPosition;

void setup(){
  myServo.attach(3);
  Serial.begin(9600);
}

void loop() {
  emgSignal = analogRead(emgAnalogInPin);
  motorPosition = map(emgSignal, 400, 600, 90, 160);
  myServo.write(motorPosition);
  Serial.println(emgSignal);
  delay(15);
}