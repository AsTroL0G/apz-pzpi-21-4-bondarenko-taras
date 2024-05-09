#include "SettingsManager.h"


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
		}
		else {
			// Якщо отримано некоректне значення для нової затримки
			client.println("HTTP/1.1 400 Bad Request");
			client.println("Content-Type: text/html");
			client.println("Connection: close");
			client.println();
			client.println("Invalid delay value");
		}
	}
	else {
		// Якщо не вдалося знайти позначення "newDelay="
		client.println("HTTP/1.1 400 Bad Request");
		client.println("Content-Type: text/html");
		client.println("Connection: close");
		client.println();
		client.println("Missing newDelay parameter");
	}

	client.stop();
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
	}
	else {
		useFahrenheit = false;
	}

	// Перенаправлення на сторінку Settings
	client.print("HTTP/1.1 302 Found\r\n");
	client.print("Location: /settings\r\n");
	client.print("\r\n");
}
