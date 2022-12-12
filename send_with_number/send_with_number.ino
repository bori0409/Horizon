// Load Wi-Fi library

#include <WiFi.h>
#include <WiFiUdp.h>
#include "M5Atom.h"


// Replace with your network credentials

const char* ssid = "Borislav";

const char* password = "borkoborko@432[fuck]";

 
 
//The udp library class

  WiFiUDP udp;

unsigned int localUdpPort = 4210;  // local port to listen on

char incomingPacket[255];  // buffer for incoming packets

 char *  replyPacket = "Hi there! Got the message :-)";  // a reply string to send back

 char * broadcastPacket = "I am here";


float ax, ay, az;
float dx, dy;
float x, y;
float t;

void setup() {

  Serial.begin(9600);
  Serial1.begin(115200);


  // Connect to Wi-Fi network with SSID and password

  Serial.print("Connecting to ");

  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {

    delay(500);

    Serial.println(".");

   

  }

  // Print local IP address

  Serial.println("");

  Serial.println("WiFi connected.");

  Serial.println("IP address: ");

  Serial.println(WiFi.localIP());



  udp.begin(localUdpPort);

  Serial.printf("Now listening at IP %s, UDP port %d\n", WiFi.localIP().toString().c_str(), localUdpPort);
  
 M5.begin(true, false, true);
  if (M5.IMU.Init() != 0) {
    Serial1.println("Failed to initialize IMU. Halting.");
    while (true) {}
  }

}



unsigned long timer = 5;



void loop() {


 M5.IMU.getAccelData(&ax, &ay, &az);
      Serial1.println("ax ");
      Serial1.println(ax);


  int packetSize = udp.parsePacket();

  /*if (packetSize)

  {

    // receive incoming UDP packets

    Serial.printf("Received %d bytes from %s, port %d\n", packetSize, udp.remoteIP().toString().c_str(), udp.remotePort());

    int len = udp.read(incomingPacket, 255);

    if (len > 0)

    {

      incomingPacket[len] = 0;

    }

    Serial.printf("UDP packet contents: %s\n", incomingPacket);



    // send back a reply, to the IP address and port we got the packet from

    udp.beginPacket(udp.remoteIP(), udp.remotePort());

    udp.print(replyPacket);

    udp.endPacket();

  }*/

  if (millis() - timer > 100) // send a broadcast msg every 10 seconds

  {
    //IPAddress ip(192, 168, 8, 187);
    //IPAddress ip(192, 168, 0, 6); home
    IPAddress ip(172, 20, 10, 4);
    timer = millis();

 




     udp.beginPacket(ip, 4210);

    udp.print(ax);

    udp.endPacket();
    Serial.println("Just broadcasted number");
delay(10);

  }


}
