#pragma once

#include <Arduino.h>
#include <WiFiS3.h>

void processSetDelayRequest(WiFiClient client, String httpRequest);

void processSetPasswordRequest(WiFiClient client, String httpRequest);

void processSetTemperatureUnit(WiFiClient client, String httpRequest);