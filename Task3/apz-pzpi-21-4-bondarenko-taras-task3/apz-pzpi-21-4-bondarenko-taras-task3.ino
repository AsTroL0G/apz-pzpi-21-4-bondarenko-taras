#include <TinyGPS.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include <TimeLib.h>
#include "Arduino_LED_Matrix.h"
#include "frames.h"
#include <ArduinoHttpClient.h>
#include <WiFiS3.h>
#include <DHT.h>

#define DHT_PIN 2
#define DHT_TYPE DHT22

TinyGPS gps;
SoftwareSerial ss(1, 0);

DHT DHT_Sensor(DHT_PIN, DHT_TYPE);
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

WiFiClient client;
unsigned long measurementInterval = 3000;  // Delay for the motion sensor in milliseconds

WiFiServer httpServer(80);

///////////////////////////////////////////////////////////////////////////////////////////

static void smartdelay(unsigned long ms);
static void print_float(float val, float invalid, int len, int prec);

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
            processHttpRequest(currentClient, httpRequest);
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
    sendPostRequest();
    previousMillis = currentMillis;
  }
}
void sendPostRequest() {
  Serial.println("Sending POST requests...");

  WiFiClient client;
  if (client.connect(server, port)) {
    Serial.println("Connected to server");

    String postData = "{\"sensorId\":\"" + sensorIdTemperature + "\",\"values\":\"" + currentTemperature + "\",\"measurementTime\":\"" + formattedTimestamp + "\"}";
    sendPostRequest(client, postData);

    client.stop();  

    // Повторне підключення
    if (client.connect(server, port)) {

      // Масив для зберігання форматованого рядка
      char flonStr[16];
      char flatStr[16]; 
      // Форматування числа з плаваючою крапкою в рядок з 6 знаками після коми
      dtostrf(currentFlon, 1, 6, flonStr); 
      dtostrf(currentFlat, 1, 6, flatStr); 

      postData = "{\"sensorId\":\"" + sensorIdGPS + "\",\"values\":\"" + String(flatStr)+'/'+String(flonStr) + "\",\"measurementTime\":\"" + formattedTimestamp + "\"}";
      sendPostRequest(client, postData);

    // Закриття підключення
      client.stop();  
      Serial.println("POST requests sent");
    } else {
      Serial.println("Connection to server failed for the second POST request");
    }
  } else {
    Serial.println("Connection to server failed for the first POST request");
  }
}
void processHttpRequest(WiFiClient& client, String httpRequest) {
  if (httpRequest.indexOf("GET / HTTP/1.1") != -1) {
    sendIndexPage(client);
  } else if (httpRequest.indexOf("GET /login?name") != -1 && httpRequest.indexOf("&password=") != -1) {
    processLoginRequest(client, httpRequest);
  } else if (httpRequest.indexOf("GET /settings HTTP/1.1") != -1) {
    sendSettingsPage(client);
  } else if (httpRequest.indexOf("POST /set-delay") != -1) {
    processSetDelayRequest(client, httpRequest);
  } else if (httpRequest.indexOf("POST /set-password") != -1) {
    processSetPasswordRequest(client, httpRequest);
  } else if (httpRequest.indexOf("POST /set-temperature-unit") != -1) {
    processSetTemperatureUnit(client, httpRequest);
  } else {
    sendNotFoundResponse(client);
  }
  
}


void sendPostRequest(WiFiClient& client, const String& postData) {
  client.println("POST /value/create HTTP/1.1");
  client.println("Host: " + String(server));
  client.println("Content-Type: application/json");
  client.println("Connection: close");
  client.print("Content-Length: ");
  client.println(postData.length());
  client.println();
  client.println(postData);

  while (client.connected()) {
    if (client.available()) {
      char c = client.read();
      Serial.print(c);
    }
  }

  Serial.println("\nRequest sent");
  // Add a short delay between the two POST requests
}
void sendIndexPage(WiFiClient client) {
  // Output response headers
  client.print("HTTP/1.1 200 OK\r\n");
  client.print("Content-Type: text/html\r\n");
  client.print("Connection: close\r\n");
  client.print("\r\n");

  // Output login form and JavaScript code for sending GET request
  client.print("<!DOCTYPE html><html><head><title>Login</title>");
  client.print("<style>");
  client.print("body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;}");
  client.print("header { background-color: #333; color: #fff; text-align: center; padding: 1em; }");
  client.print("form { max-width: 300px; margin: 2em auto; padding: 1em; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);}");
  client.print("label { display: block; margin-bottom: 0.5em; }");
  client.print("input { width: 100%; padding: 0.5em; margin-bottom: 1em; box-sizing: border-box; }");
  client.print("input[type='submit'] { background-color: #333; color: #fff; cursor: pointer;}");
  client.print("</style>");
  client.print("</head><body>");
  client.print("<header><h1>Login</h1></header>");
  client.print("<form method=\"get\" action=\"/login\">");  // Change method to GET
  client.print("<label for=\"name\">Login:</label>");
  client.print("<input type=\"text\" id=\"name\" name=\"name\" required><br>");
  client.print("<label for=\"password\">Password:</label>");
  client.print("<input type=\"password\" id=\"password\" name=\"password\" required><br>");
  client.print("<input type=\"submit\" value=\"Submit\">");
  client.print("</form>");
  client.print("<header><h1>Sensor</h1></header>");
  client.print("<form method=\"get\" action=\"/login\">");  // Change method to GET
  client.print("<label for=\"name\">Temperature:</label>");
  client.print("<label for=\"password\">"); 
  client.print(currentTemperature);  // Include the value of currentTemperature variable
  client.print("</label>");
  char flonStr1[16]; // Масив для зберігання форматованого рядка
  char flatStr1[16]; // Масив для зберігання форматованого рядка
  dtostrf(currentFlon, 1, 6, flonStr1); // Форматування числа з плаваючою крапкою в рядок з 6 знаками після коми
  dtostrf(currentFlat, 1, 6, flatStr1); // Форматування числа з плаваючою крапкою в рядок з 6 знаками після коми


  client.print("<label for=\"password\">GPS:</label>");
  client.print("<label for=\"password\">Flat: "); 
  client.print(String(flatStr1));  // Include the value of currentGPS variable
  client.print("</label>");
  client.print("<label for=\"password\">Flon: "); 
  client.print(String(flonStr1));  // Include the value of currentGPS variable
  client.print("</label>");
  client.print("</form>");

  // JavaScript code for sending GET request
  client.print("<script>");
  client.print("function sendGetRequest() {");
  client.print("var password = document.getElementById('password').value;");
  client.print("var login = document.getElementById('name').value;");
  client.print("window.location.href = encodeURIComponent(login) + '?password=' + encodeURIComponent(password);");
  client.print("}");
  client.print("</script>");
  client.print("</body></html>");
}


void sendNotFoundResponse(WiFiClient client) {
  String response = "HTTP/1.1 404 Not Found\r\n";
  response += "Content-type: text/html\r\n";
  response += "Connection: close\r\n";
  response += "\r\n";
  response += "<!DOCTYPE html><html><head><title>404 Not Found</title></head><body>";
  response += "<h1>404 Not Found</h1>";
  response += "</body></html>";

  client.print(response);
  client.flush();
  client.stop();
}

static void smartdelay(unsigned long ms)
{
  unsigned long start = millis();
  do 
  {
    while (ss.available())
      gps.encode(ss.read());
  } while (millis() - start < ms);
}

static void print_float(float val, float invalid, int len, int prec)
{
  if (val == invalid)
  {
    while (len-- > 1)
      Serial.print('*');
    Serial.print(' ');
  }
  else
  {

    Serial.println(val, prec);
    int vi = abs((int)val);
    int flen = prec + (val < 0.0 ? 2 : 1); // . and -
    flen += vi >= 1000 ? 4 : vi >= 100 ? 3 : vi >= 10 ? 2 : 1;
    for (int i=flen; i<len; ++i)
      Serial.print(' ');
  }
  smartdelay(0);
}

void printWifiStatus() {
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());
  Serial.print("Signal strength (RSSI):");
  Serial.print(WiFi.RSSI());
  Serial.println(" dBm");
}
void sendSettingsPage(WiFiClient client) {

  if (!isAuthenticated) {
    client.print("HTTP/1.1 302 Found\r\n");
    client.print("Location: /\r\n");
    client.print("\r\n");
    return;
  }

  client.print("HTTP/1.1 200 OK\r\n");
  client.print("Content-Type: text/html\r\n");
  client.print("Connection: close\r\n");
  client.print("\r\n");

  // Output content of the Settings page
  client.print("<!DOCTYPE html><html><head><title>Settings</title>");
  client.print("<style>");
  client.print("body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;}");
  client.print("header { background-color: #333; color: #fff; text-align: center; padding: 1em; }");
  client.print("form { max-width: 300px; margin: 2em auto; padding: 1em; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);}");
  client.print("label { display: block; margin-bottom: 0.5em; }");
  client.print("input { width: calc(100% - 12px); padding: 0.5em; margin-bottom: 1em; box-sizing: border-box; }");
  client.print("input[type='submit'], input[type='button'] { background-color: #333; color: #fff; cursor: pointer;}");
  client.print("p { margin-bottom: 1em; }");
  client.print("form[id=\"temperatureUnitForm\"] label { display: inline; }");
  client.print("</style>");
  client.print("</head><body>");

  client.print("<header><h1>User settings</h1></header>");

  // Add HTML form to change password
  client.print("<form id=\"passwordForm\">");
  client.print("<label for=\"newPassword\">Change Password:</label>");
  client.print("<input type=\"password\" id=\"newPassword\" name=\"newPassword\" required><br>");
  client.print("<input type=\"button\" value=\"Change Password\" onclick=\"changePassword()\">");
  client.print("</form>");

  client.print("<header><h1>Sensor settings</h1></header>");

  // Add HTML form to enter new delay value
  client.print("<form id=\"delayForm\">");
  // Output current delay for motion sensor
  client.print("<p>Current measurement delay: ");
  client.print(measurementInterval);
  client.print(" milliseconds</p>");
  client.print("<label for=\"newDelay\">Change Measurement Delay (in milliseconds):</label>");
  client.print("<input type=\"number\" id=\"newDelay\" name=\"newDelay\" required><br>");
  client.print("<input type=\"button\" value=\"Change Delay\" onclick=\"changeDelay()\">");
  client.print("</form>");

  // Add HTML form to enter new equipmentId value
  client.print("<form id=\"temperatureUnitForm\">");
  client.print("<label>Select Temperature Unit:</label>");
  client.print("<input type=\"radio\" id=\"celsius\" name=\"temperatureUnit\" value=\"Celsius\" checked>");
  client.print("<label for=\"celsius\">Celsius</label>");
  client.print("<input type=\"radio\" id=\"fahrenheit\" name=\"temperatureUnit\" value=\"Fahrenheit\">");
  client.print("<label for=\"fahrenheit\">Fahrenheit</label><br>");
  client.print("<input type=\"button\" value=\"Change Temperature Unit\" onclick=\"changeTemperatureUnit()\">");
  client.print("</form>");

  // JavaScript code to send new temperatureUnit value to the server
  client.print("<script>");
  client.print("function changeTemperatureUnit() {");
  client.print("var selectedUnit = document.querySelector('input[name=\"temperatureUnit\"]:checked').value;");
  client.print("var xhr = new XMLHttpRequest();");
  client.print("xhr.open('POST', '/set-temperature-unit?selectedUnit=' + selectedUnit, true);");
  client.print("xhr.send();");
  client.print("}");
  client.print("</script>");

  // JavaScript code to send new delay value to the server
  client.print("<script>");
  client.print("function changeDelay() {");
  client.print("var newDelay = document.getElementById('newDelay').value;");
  client.print("var xhr = new XMLHttpRequest();");
  client.print("xhr.open('POST', '/set-delay?newDelay=' + newDelay, true);");
  client.print("xhr.send();");
  client.print("}");

  // JavaScript code to send new password to the server
  client.print("function changePassword() {");
  client.print("var newPassword = document.getElementById('newPassword').value;");
  client.print("var xhr = new XMLHttpRequest();");
  client.print("xhr.open('POST', '/set-password?newPassword=' + newPassword, true);");
  client.print("xhr.send();");
  client.print("}");
  client.print("</script>");

  delay(500);
  client.print("</body></html>");
}

bool checkPassword(const String& passwordLogin) {
  // Перевірка паролю (ваша логіка перевірки)
  return passwordLogin.equals(userPassword);
}
bool checkLoginName(const String& login) {
  // Перевірка паролю (ваша логіка перевірки)
  return login.equals(userLogin);
}

String getValue(String data, String key, char separator) {
  int keyIndex = data.indexOf(key);
  if (keyIndex == -1) return "";  // якщо ключ не знайдено, повертаємо порожню рядок
  int separatorIndex = data.indexOf(separator, keyIndex);
  if (separatorIndex == -1) separatorIndex = data.length();            // якщо роздільник не знайдено, беремо кінець рядка
  return data.substring(keyIndex + key.length() + 1, separatorIndex);  // +1 для ігнорування знаку '='
}
void processLoginRequest(WiFiClient client, String httpRequest) {
  // Витягуємо параметри запиту з URL
  String logParam = getValue(httpRequest, "name", '&');
  String passwordParam = getValue(httpRequest, "password", '&');

  // Знаходимо індекс першого пробілу в рядку
  int spaceIndexLog = logParam.indexOf(' ');
  int spaceIndexPassword = passwordParam.indexOf(' ');

  // Відсікаємо рядок до першого пробілу (включно)
  if (spaceIndexLog != -1) {
    logParam = logParam.substring(0, spaceIndexLog);
  }

  if (spaceIndexPassword != -1) {
    passwordParam = passwordParam.substring(0, spaceIndexPassword);
  }

  Serial.println("_________________");
  Serial.println("Login: " + logParam);
  Serial.println("Password: " + passwordParam);
  Serial.println("_________________");
  // Перевірка паролю
  if (checkPassword(passwordParam) && checkLoginName(logParam)) {
    // Якщо пароль вірний, можна вважати, що користувач автентифікований
    isAuthenticated = true;

    // Перенаправлення на сторінку Settings
    client.print("HTTP/1.1 302 Found\r\n");
    client.print("Location: /settings\r\n");
    client.print("\r\n");
  } else {
    // Якщо пароль невірний, виводимо повідомлення про помилку
    sendIndexPage(client);
    client.print("<p style=\"color: red;\">Incorrect password. Try again.</p>");
  }
}
void processSetDelayRequest(WiFiClient client, String httpRequest) {
  int newDelayIndex = httpRequest.indexOf("newDelay=");

  if (newDelayIndex != -1) {
    // Знаходження позначення '&' після "newDelay="
    int ampersandIndex = httpRequest.indexOf('&', newDelayIndex);

    // Витягнення значення "newDelay" із рядка
    String newDelayString = httpRequest.substring(newDelayIndex + 9, ampersandIndex);

    // Перетворення рядка у ціле число
    int newDelay = newDelayString.toInt();

    if (newDelay > 0) {
      // Змінюємо затримку
      measurementInterval = newDelay;

      // Повертаємо пусту відповідь, так як це POST-запит
      client.println("HTTP/1.1 200 OK");
      client.println("Content-Type: text/html");
      client.println("Connection: close");
      client.println();

      // Виводимо JavaScript-код для оновлення сторінки
      client.println("<html><head><script>");
      client.println("window.location.href = window.location.href;");
      client.println("</script></head></html>");
    } else {
      // Якщо отримано некоректне значення для нової затримки
      client.println("HTTP/1.1 400 Bad Request");
      client.println("Content-Type: text/html");
      client.println("Connection: close");
      client.println();
      client.println("Invalid delay value");
    }
  } else {
    // Якщо не вдалося знайти позначення "newDelay="
    client.println("HTTP/1.1 400 Bad Request");
    client.println("Content-Type: text/html");
    client.println("Connection: close");
    client.println();
    client.println("Missing newDelay parameter");
  }

  client.stop();
}
void processSetTemperatureUnit(WiFiClient client, String httpRequest) {
  String unitParam = getValue(httpRequest, "selectedUnit", '&');

  // Знаходимо індекс першого пробілу в рядку
  int spaceIndex = unitParam.indexOf(' ');
  Serial.println(unitParam);

  // Відсікаємо рядок до першого пробілу (включно)
  if (spaceIndex != -1) {
    unitParam = unitParam.substring(0, spaceIndex);
  }
  Serial.println(unitParam);

  // Знаходимо індекс "дорівнює"
  int equalsIndex = unitParam.indexOf('=');

  // Відсікаємо рядок до "дорівнює" (включно)
  if (equalsIndex != -1) {
    unitParam = unitParam.substring(equalsIndex + 1);
  }

  Serial.println(unitParam);
  if (unitParam == "Fahrenheit") {
    useFahrenheit = true;
  } else {
    useFahrenheit = false;
  }

  // Перенаправлення на сторінку Settings
  client.print("HTTP/1.1 302 Found\r\n");
  client.print("Location: /settings\r\n");
  client.print("\r\n");
}

void processSetPasswordRequest(WiFiClient client, String httpRequest) {
  // Витягуємо параметри запиту з URL
  String passwordParam = getValue(httpRequest, "password", '&');

  // Знаходимо індекс першого пробілу в рядку
  int spaceIndex = passwordParam.indexOf(' ');
  Serial.println(passwordParam);

  // Відсікаємо рядок до першого пробілу (включно)
  if (spaceIndex != -1) {
    passwordParam = passwordParam.substring(0, spaceIndex);
  }
  Serial.println(passwordParam);

  // Знаходимо індекс "дорівнює"
  int equalsIndex = passwordParam.indexOf('=');

  // Відсікаємо рядок до "дорівнює" (включно)
  if (equalsIndex != -1) {
    passwordParam = passwordParam.substring(equalsIndex + 1);
  }

  // Змінюємо пароль
  userPassword = passwordParam;

  // Перенаправлення на сторінку Settings
  client.print("HTTP/1.1 302 Found\r\n");
  client.print("Location: /settings\r\n");
  client.print("\r\n");
}