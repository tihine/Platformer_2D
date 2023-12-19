// The flag signals to the rest of the program an interrupt occured
static bool button_flag = false;
// Remember the state the dash in the Unity program is in
static bool dash_state = true;

//Bouton dash = Pin 2
//Ventilo = Pin ~10

// Interrupt handler, sets the flag for later processing
void buttonPress() {
  button_flag = true;
}

void setup() {
  int buttonPin = 2;
  
  pinMode(LED_BUILTIN, OUTPUT);
  // Internal pullup, no external resistor necessary
  pinMode(buttonPin,INPUT_PULLUP);
  // 115200 is a common baudrate : fast without being overwhelming
  Serial.begin(9600);

  // As the button is in pullup, detect a connection to ground
  attachInterrupt(digitalPinToInterrupt(buttonPin),buttonPress,FALLING);

  // Wait for a serial connection
  while (!Serial.availableForWrite());
  // In case the Unity project isn't synced with the boolean.
  Serial.println("notDash");
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
}

// Handles incoming messages
// Called by Arduino if any serial data has been received
void serialEvent()
{
  String message = Serial.readStringUntil('\n');
  if (message == "LED ON") {
    digitalWrite(13,HIGH);
  } else if (message == "LED OFF") {
    digitalWrite(13,LOW);
  }
}