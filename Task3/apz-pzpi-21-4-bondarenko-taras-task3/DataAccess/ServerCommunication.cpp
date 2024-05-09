#include "ServerCommunication.h"

void sendPostRequest(const char* server, int port, string sensorIdTemperature, float currentTemperature, float currentFlon, float currentFlat,string sensorIdGPS, string formattedTimestamp) {
    Serial.println("Sending POST requests...");

    WiFiClient client;
    if (client.connect(server, port)) {
        Serial.println("Connected to server");

        String postData = "{\"sensorId\":\"" + sensorIdTemperature + "\",\"values\":\"" + currentTemperature + "\",\"measurementTime\":\"" + formattedTimestamp + "\"}";
        sendRequest(client, postData);

        client.stop();

        // Повторне підключення
        if (client.connect(server, port)) {

            // Масив для зберігання форматованого рядка
            char flonStr[16];
            char flatStr[16];
            // Форматування числа з плаваючою крапкою в рядок з 6 знаками після коми
            dtostrf(currentFlon, 1, 6, flonStr);
            dtostrf(currentFlat, 1, 6, flatStr);

            postData = "{\"sensorId\":\"" + sensorIdGPS + "\",\"values\":\"" + flatStr + '/' + flonStr + "\",\"measurementTime\":\"" + formattedTimestamp + "\"}";
            sendRequest(client, postData);

            // Закриття підключення
            client.stop();
            Serial.println("POST requests sent");
        }
        else {
            Serial.println("Connection to server failed for the second POST request");
        }
    }
    else {
        Serial.println("Connection to server failed for the first POST request");
    }
}