#include <TinyGPS.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include <TimeLib.h>
#include <ArduinoHttpClient.h>
#include <WiFiS3.h>
#include <DHT.h>
#include "BusinessLogic/SettingsManager.h"
#include "DataAccess/ServerCommunication.h"
#include "Presentation/HttpServer.h"

TinyGPS gps;
SoftwareSerial ss(1, 0);
DHT DHT_Sensor(2, DHT22);

bool isAuthenticated = false;
const char wifiSSID[] = "Quail";
const char wifiPassword[] = "senoninim";
char server[] = "192.168.0.105";
const int port = 7064;

String userLogin = "taras";
String userPassword = "1111";
String sensorIdTemperature = "662ab1fb0e5151644a4bc08f";
String sensorIdGPS = "662ab1ce0e5151644a4bc08b";

int wifiStatus = WL_IDLE_STATUS;
bool useFahrenheit = false;
char formattedTimestamp[30];
float currentFlat;
float currentFlon;
float currentTemperature;
time_t currentTimestamp;
unsigned long previousMillis = 0;
unsigned long measurementInterval = 3000;  // Delay for the motion sensor in milliseconds

WiFiClient client;
WiFiServer httpServer(80);

void setup() {
  Serial.begin(115200);
  ss.begin(9600);
  DHT_Sensor.begin();

  String firmwareVersion = WiFi.firmwareVersion();
  if (firmwareVersion < WIFI_FIRMWARE_LATEST_VERSION)
    Serial.println("Please upgrade the firmware");

  while (wifiStatus != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(wifiSSID);
    wifiStatus = WiFi.begin(wifiSSID, wifiPassword);
    delay(10000);
  }
  httpServer.begin();
  printWifiStatus();
  setTime(14, 57, 4, 13, 12, 2023);
}

void loop() {
  unsigned long currentMillis = millis();

  // Перевірка наявності нового клієнта
  WiFiClient currentClient = httpServer.available();
  if (currentClient) {
    Serial.println("New Client.");
    String currentLine = "";
    String httpRequest = "";
    // Очікування з'єднання клієнта
    while (currentClient.connected()) {
      if (currentClient.available()) {
        char c = currentClient.read();
        Serial.write(c);
        currentLine += c;
        if (c == '\n') {
          if (currentLine.length() == 2) {
            processHttpRequest(client, httpRequest, isAuthenticated, currentFlon, currentFlat, currentTemperature, measurementInterval);
            break;
          }
          httpRequest += currentLine;
          currentLine = "";
        }
      }
    }
    // Закриття з'єднання з клієнтом
    currentClient.stop();
    Serial.println("Client disconnected.");
    Serial.println();
  }
    
  if (currentMillis - previousMillis >= measurementInterval) {
    // Отримання значень координат з датчика NEO-6M
    gps.f_get_position(&currentFlat, &currentFlon);
    //Отримання значень температури з датчика DHT22
    currentTemperature = DHT_Sensor.readTemperature(useFahrenheit);
    // Отримання поточного часу
    time_t t = now();
    // Форматування часу у строку для використання в HTTP-     запиті
    sprintf(formattedTimestamp, "%04d-%02d-%02dT%02d:%02d:%02d.717Z",
            year(t), month(t), day(t), hour(t), minute(t), second(t));
    // Вивід даних в консоль
    Serial.println("-----------------------------");
    Serial.print("GPS: ");
    Serial.print("Flat: ");
    print_float(currentFlat, TinyGPS::GPS_INVALID_F_ANGLE, 10, 6);
    Serial.print(" Flon: ");
    print_float(currentFlon, TinyGPS::GPS_INVALID_F_ANGLE, 11, 6);
    Serial.print("Temperature: ");
    Serial.println(currentTemperature);
    Serial.print("MeasurementInterval: ");
    Serial.println(measurementInterval);
    Serial.print("useFahrenheit: ");
    Serial.println(useFahrenheit);
    Serial.println("-----------------------------");
    sendPostRequest(server, port,sensorIdTemperature, currentTemperature, currentFlon, currentFlat); 
    previousMillis = currentMillis;
  }
}






