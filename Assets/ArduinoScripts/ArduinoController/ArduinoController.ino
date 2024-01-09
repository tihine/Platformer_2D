#include <Wire.h>

// The flag signals to the rest of the program an interrupt occured
static bool button_flag = false;
// Remember the state the dash in the Unity program is in
static bool dash_state = true;

int pressionPin = A5;

//Bouton dash = Pin 2
//Ventilo = Pin ~10
//Pression = Pin A5
//LED = Pin 4

// Interrupt handler, sets the flag for later processing
void buttonPress() {
  button_flag = true;
}

void setup() {
  int buttonPin = 2;
  int ventiloPin = 10;
  
  
  pinMode(LED_BUILTIN, OUTPUT);
  // Internal pullup, no external resistor necessary
  pinMode(buttonPin,INPUT_PULLUP);
  pinMode(pressionPin,INPUT);
  // 115200 is a common baudrate : fast without being overwhelming
  Serial.begin(9600);

  // As the button is in pullup, detect a connection to ground
  attachInterrupt(digitalPinToInterrupt(buttonPin),buttonPress,FALLING);


  // Wait for a serial connection
  while (!Serial.availableForWrite());
  // In case the Unity project isn't synced with the boolean.
  Serial.println("notSync");
}

// Processes button input
void loop() {
  // Slows reaction down a bit
  // but prevents _most_ button press misdetections
  delay(200);
  
  //Press button to dash
  if (button_flag) {
    Serial.println("dash");
    button_flag = false;
  }

  //Press Captor to jump
  int pressionOnButton = analogRead(pressionPin);
  if(pressionOnButton > 100) {
    Serial.println("jump");
    delay(500);
  }


}

// Handles incoming messages
// Called by Arduino if any serial data has been received

void read_message(char *buffer, const uint8_t match_len) {
  uint8_t read_bytes = 0;
  do {
    Wire.readBytesUntil('\n',buffer,match_len);
    

  } while(read_bytes < match_len || Serial.peek() == '\n');
  if (Serial.peek() == '\n')
    Serial.read(); // Consume the delimiter if it is there.
}

void serialEvent()
{
  char buff[64];
  int read_bytes = 0;
     do {
    read_bytes += Serial.readBytesUntil('\n',buff,64);
  } while(read_bytes < 64); //was while(read_bytes < 64 || Serial.peek() == '\n'); before
  if (Serial.peek() == '\n')
    Serial.read(); // Consume the delimiter if it is there.
  if (!strncmp(buff, "LED ON", 6)) {
    digitalWrite(4,HIGH);
  } else if (!strncmp(buff, "LED OFF", 7)) {
    digitalWrite(4,LOW);
  }

  if(!strncmp(buff, "VENTIL_ON", 9)) {
    analogWrite(10,255);
  }
  else if(!strncmp(buff, "VENTIL_OFF", 10)) {
    analogWrite(10,0);
  }
}