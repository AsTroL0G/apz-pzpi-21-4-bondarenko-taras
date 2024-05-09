#pragma once

#include <WiFiS3.h>
#include <Arduino.h>
#include "../BusinessLogic/SettingsManager.h"
#include "../Presentation/HttpServer.h"

void sendPostRequest(const char* server, int port, string sensorIdTemperature, float currentTemperature, float currentFlon, float currentFlat, string sensorIdGPS, string formattedTimestamp);