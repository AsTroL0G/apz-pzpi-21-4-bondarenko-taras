package com.example.cargotruckmobile.Activity

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.cargotruckmobile.Models.UserCreate
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch

class RegisterActivity : AppCompatActivity() {

    var isAuth = false;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)

        val userEmail: EditText = findViewById(R.id.registerEmail)
        val userPassword: EditText = findViewById(R.id.registerPassword)
        val userFirstName: EditText = findViewById(R.id.registerFirstName)
        val userLastName: EditText = findViewById(R.id.registerLastName)
        val button: Button = findViewById(R.id.registerButton)
        val linkoToRegistration: TextView = findViewById(R.id.userLogin)

        button.setOnClickListener {
            val email = userEmail.text.toString().trim()
            val password = userPassword.text.toString().trim()
            val firstName = userFirstName.text.toString().trim()
            val lastName = userLastName.text.toString().trim()

            if (lastName.isEmpty() || firstName.isEmpty() || email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Не всі поля заповнені", Toast.LENGTH_LONG).show()
            } else {
                val user = UserCreate(firstName, lastName, "", "", email, password)
                registerUser(user)
                if(isAuth){
                    val intent = Intent(this, AuthActivity::class.java)
                    startActivity(intent)
                }
            }
        }

        linkoToRegistration.setOnClickListener{

            val intent = Intent(this, AuthActivity::class.java)
            startActivity(intent)
        }
    }

    private fun registerUser(user: UserCreate) {

        GlobalScope.launch(Dispatchers.Main) {
            try {
                val response = RetrofitClient.userService.registerUser(user)
                if (response.isSuccessful) {
                    Toast.makeText(this@RegisterActivity, "Success", Toast.LENGTH_LONG).show()
                    isAuth = true;
                } else {
                    val errorMessage = response.errorBody()?.string() ?: "Unknown error occurred"

                    // Перевірка, чи помилка містить "Invalid email"
                    if (errorMessage.contains("System.ArgumentException: Invalid email")) {
                        Toast.makeText(this@RegisterActivity, "The mail was entered in the wrong format", Toast.LENGTH_LONG).show()
                    } else {
                        Toast.makeText(this@RegisterActivity, "Error: $errorMessage", Toast.LENGTH_LONG).show()
                    }
                }
            } catch (e: Exception) {
                Toast.makeText(this@RegisterActivity, "Error: ${e.message}", Toast.LENGTH_LONG).show()
            }
        }
    }
}
