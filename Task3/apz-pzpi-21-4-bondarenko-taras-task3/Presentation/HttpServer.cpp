#include "HttpServer.h"
#include "../BusinessLogic/SettingsManager.h"

void sendIndexPage(WiFiClient client, float currentFlon, float currentFlat, float currentTemperature){
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
    // Array for saving formatted rows
    char flonStr1[16]; 
    char flatStr1[16];
    // Formatting a number with a floating dot in a row with 6 signs after the komi
    dtostrf(currentFlon, 1, 6, flonStr1);
    dtostrf(currentFlat, 1, 6, flatStr1); 


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

void sendSettingsPage(WiFiClient client, unsigned long measurementInterval, bool isAuthenticated) {

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

void processHttpRequest(WiFiClient client, String httpRequest, bool isAuthenticated, float currentFlon, float currentFlat, float currentTemperature, unsigned long measurementInterval) {
    if (httpRequest.indexOf("GET / HTTP/1.1") != -1) {
        sendIndexPage(client, currentFlon, currentFlat, currentTemperature);
    }
    else if (httpRequest.indexOf("GET /login?name") != -1 && httpRequest.indexOf("&password=") != -1) {
        processLoginRequest(client, httpRequest);
    }
    else if (httpRequest.indexOf("GET /settings HTTP/1.1") != -1) {
        sendSettingsPage(client, measurementInterval, isAuthenticated);
    }
    else if (httpRequest.indexOf("POST /set-delay") != -1) {
        processSetDelayRequest(client, httpRequest);
    }
    else if (httpRequest.indexOf("POST /set-password") != -1) {
        processSetPasswordRequest(client, httpRequest);
    }
    else if (httpRequest.indexOf("POST /set-temperature-unit") != -1) {
        processSetTemperatureUnit(client, httpRequest);
    }
    else {
        sendNotFoundResponse(client);
    }

}

void sendRequest(WiFiClient client, const String postData) {
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
}

void processLoginRequest(WiFiClient client, String httpRequest, float currentFlon, float currentFlat, float currentTemperature) {
    // Various parameters are requested from the URL
    String logParam = getValue(httpRequest, "name", '&');
    String passwordParam = getValue(httpRequest, "password", '&');


    // We know the index of the first space in a row
    int spaceIndexLog = logParam.indexOf(' ');
    int spaceIndexPassword = passwordParam.indexOf(' ');


    // We extend the row up to the first space (inclusive)
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
    // Checking the password
    if (checkPassword(passwordParam) && checkLoginName(logParam)) {
        isAuthenticated = true;
        client.print("HTTP/1.1 302 Found\r\n");
        client.print("Location: /settings\r\n");
        client.print("\r\n");
    }
    else {
        
        sendIndexPage(client, currentFlon, currentFlat, currentTemperature);
        client.print("<p style=\"color: red;\">Incorrect password. Try again.</p>");
    }
}

bool checkPassword(const char* passwordLogin) {

    return passwordLogin.equals(userPassword);
}
bool checkLoginName(const char* login) {

    return login.equals(userLogin);
}

const char* getValue(const char* data, const char* key, char separator) {
  int keyIndex = data.indexOf(key);
  if (keyIndex == -1) return "";
  // if the key is not found, we turn the empty row
  int separatorIndex = data.indexOf(separator, keyIndex);
  if (separatorIndex == -1) separatorIndex = data.length();  
  return data.substring(keyIndex + key.length() + 1, separatorIndex);  
}
