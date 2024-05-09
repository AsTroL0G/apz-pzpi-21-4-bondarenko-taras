// Presentation/HttpServer.h
#pragma once
#include <WiFiS3.h>
#include <Arduino.h>
#include <ArduinoHttpClient.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include "../DataAccess/ServerCommunication.h"

void sendIndexPage(WiFiClient client, float currentFlon, float currentFlat, float currentTemperature);
void sendSettingsPage(WiFiClient client, unsigned long measurementInterval, bool isAuthenticated);
void sendNotFoundResponse(WiFiClient client);
void processHttpRequest(WiFiClient client, String httpRequest, bool isAuthenticated, float currentFlon, float currentFlat, float currentTemperature, unsigned long measurementInterval);
void processLoginRequest(WiFiClient client, String httpRequest, float currentFlon, float currentFlat, float currentTemperature);
void sendRequest(WiFiClient client, const String postData);
bool checkPassword(const String& passwordLogin);
bool checkLoginName(const String& login);
String getValue(String data, String key, char separator);
