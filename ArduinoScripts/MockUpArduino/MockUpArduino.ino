long posIs = 360*10;
long posIsOverflow = 0;
int buttonA = 2, buttonB = 3;

void setup() {
  Serial.println(sizeof(posIs));
  // put your setup code here, to run once:
  Serial.begin(9600);
  //digitalWrite(buttonA, 1);
  //digitalWrite(buttonB, 1);
  pinMode(buttonA, INPUT_PULLUP);
  pinMode(buttonB, INPUT_PULLUP);
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

void loop() {
  if(digitalRead(buttonA)==0) {
    posIs+=5;
  } else if(digitalRead(buttonB)==0) {
    posIs-=5;
  }


  // begin sequence:
  size_t dataByte = 0xFF;
  sendDataByte(dataByte, true);
  dataByte = posIs % 256;
  sendDataByte(dataByte);
  dataByte = posIs / 256;
  sendDataByte(dataByte);
  Serial.println(sizeof(posIs));
  Serial.flush();

  delay(50);
}
