int servoAnalogInPin = A0;
int posIs;
int posIsDeg;

void setup(){
  Serial.begin(9600);
  delay(1000);
}

int a_zero = 240;
int a_ninety = 430;
int degToServo(int deg) {
  return -deg * ((a_ninety - a_zero)/90.0) + a_ninety;
}

int servoToDeg(int deg) {
  return -(90.0/(a_ninety - a_zero))*(deg - a_ninety);
}

int getAngle(int sensorAngle) {
  return (int)(180 * ((posIs%0xFC00 - 113) / (float)(469 - 113))) % 360;
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

void loop (){
  posIs = analogRead(servoAnalogInPin);
  posIsDeg = getAngle(posIs);
  //posIsDeg = (90.0/(240.0-430.0))*(posIs-430);
  //Serial.print("Position:");
  //Serial.println(posIs);
  // begin sequence:
  size_t dataByte = 0xFF;
  sendDataByte(dataByte, true);
  //Serial.flush();
  dataByte = posIsDeg % 256;
  sendDataByte(dataByte);
  //Serial.flush();
  dataByte = posIsDeg / 256;
  sendDataByte(dataByte);
  //Serial.println(servoToDeg(degToServo(posIsDeg)));
  delay(20);
}