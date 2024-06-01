package com.example.cargotruckmobile.Activity

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import com.example.cargotruckmobile.Models.Credentials
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import com.auth0.android.jwt.JWT
import com.example.cargotruckmobile.R

class AuthActivity : AppCompatActivity() {

    var isAuth = false;
    var userId  =""
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_auth)

        val savedUserId = getUser()
        if (savedUserId != null) {
            val intent = Intent(this, MainActivity::class.java)
            intent.putExtra("userId", userId)
            startActivity(intent)
        }

        val userEmail: EditText = findViewById(R.id.authEmail)
        val userPassword: EditText = findViewById(R.id.authPassword)
        val button: Button = findViewById(R.id.authButton)



        val linkoToRegistration: TextView = findViewById(R.id.linkToRegistration)

        linkoToRegistration.setOnClickListener{

            val intent = Intent(this, RegisterActivity::class.java)
            startActivity(intent)
        }

        button.setOnClickListener {
            val email = userEmail.text.toString().trim()
            val password = userPassword.text.toString().trim()

            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Not all fields are filled", Toast.LENGTH_LONG).show()
            } else {
                val credentials = Credentials(email, password)
                loginUser(credentials)

                if(isAuth){
                    val intent = Intent(this, MainActivity::class.java)
                    intent.putExtra("userId", userId)
                    startActivity(intent)

                }
            }
        }

    }
    private fun saveUserId(userId: String) {
        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)
        val editor = sharedPreferences.edit()
        editor.putString("userId", userId)
        editor.apply()
    }

    private fun getUser(): String? {
        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)
        return sharedPreferences.getString("userId", null)
    }


    private fun loginUser(credentials: Credentials) {
        GlobalScope.launch(Dispatchers.Main) {
            try {
                val response = RetrofitClient.userService.loginUser(credentials)
                if (response.isSuccessful) {
                    val loginResponse = response.body()
                    if (loginResponse != null) {
                        val accessToken = loginResponse.accessToken
                        val refreshToken = loginResponse.refreshToken

                        // Декодування токену
                        val jwt = JWT(accessToken)
                        userId = jwt.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").asString().toString()
                        val email = jwt.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").asString()
                        val userId = saveUserId(userId)
                        Toast.makeText(this@AuthActivity, "Success. Email: $email, UserId: $userId", Toast.LENGTH_LONG).show()
                        isAuth = true
                    }
                } else {
                    val errorMessage = response.errorBody()?.string() ?: "Unknown error occurred"
                    if (errorMessage.contains("System.ArgumentException: Invalid password")) {
                        Toast.makeText(this@AuthActivity, "Не правильно введений пароль", Toast.LENGTH_LONG).show()
                    } else if (errorMessage.contains("System.ArgumentException: User not find")) {
                        Toast.makeText(this@AuthActivity, "Користувача з даною поштою не знайдено", Toast.LENGTH_LONG).show()
                    } else {
                        Toast.makeText(this@AuthActivity, "Error: $errorMessage", Toast.LENGTH_LONG).show()
                    }
                }
            } catch (e: Exception) {
                Toast.makeText(this@AuthActivity, "Error: ${e.message}", Toast.LENGTH_LONG).show()
            }
        }
    }
}