package com.example.cargotruckmobile.Activity

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Activity.AdminActivity.AdminActivity
import com.example.cargotruckmobile.Activity.LogistActivity.LogistActivity
import com.example.cargotruckmobile.Models.UserUpdate
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class UserProfileActivity : AppCompatActivity() {

    private lateinit var etFirstName: EditText
    private lateinit var etLastName: EditText
    private lateinit var etPhoneNumber: EditText
    private lateinit var etEmail: EditText
    private lateinit var etPassword: EditText
    private lateinit var etPatronym: EditText
    private lateinit var btnSave: Button
    private lateinit var btnExit: Button
    private lateinit var btnAdmin: Button
    private lateinit var btnLogist: Button



    private var userId: String = ""
    private var userRole: String = ""
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_user_profile)

        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)

        userId = sharedPreferences.getString("userId", null).toString()

        etFirstName = findViewById(R.id.et_first_name)
        etLastName = findViewById(R.id.et_last_name)
        etPatronym = findViewById(R.id.et_patronym)
        etPhoneNumber = findViewById(R.id.et_phone_number)
        etEmail = findViewById(R.id.et_email)
        etPassword = findViewById(R.id.et_password)
        btnSave = findViewById(R.id.btn_save)
        btnExit = findViewById(R.id.btn_exit)
        btnAdmin = findViewById(R.id.btn_admin)
        btnLogist = findViewById(R.id.btn_logist)
        loadUserData()


        btnSave.setOnClickListener {
            saveUserData()
        }

        btnLogist.setOnClickListener {
            val intent = Intent(this, LogistActivity::class.java)
            startActivity(intent)
        }

        btnAdmin.setOnClickListener {
            val intent = Intent(this, AdminActivity::class.java)
            startActivity(intent)
        }

        btnLogist.setOnClickListener {
            val intent = Intent(this, LogistActivity::class.java)
            startActivity(intent)
        }

        btnExit.setOnClickListener {
            logout()
        }
    }
    private fun logout() {
        // Очищення SharedPreferences
        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)
        val editor = sharedPreferences.edit()
        editor.clear()
        editor.apply()

        // Перехід на екран входу
        val intent = Intent(this, AuthActivity::class.java)
        intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
        startActivity(intent)
    }
    private fun loadUserData() {
        lifecycleScope.launch(Dispatchers.Main) {
            Log.d("CreateCargoActivity", "Inside lifecycleScope.launch")
            try {
                val user = RetrofitClient.userService.getUser(userId)
                userRole = user.roles[0].name;

                if (userRole == "Адміністратор") {
                    btnAdmin.visibility = View.VISIBLE
                } else {
                    btnAdmin.visibility = View.GONE
                }

                if (userRole == "Логіст") {
                    btnLogist.visibility = View.VISIBLE
                } else {
                    btnLogist.visibility = View.GONE
                }

                etFirstName.setText(user.firstName)
                etLastName.setText(user.lastName)
                etPatronym.setText(user.patronym)
                etPhoneNumber.setText(user.phoneNumber)
                etEmail.setText(user.email)
            } catch (e: Exception) {
                Toast.makeText(this@UserProfileActivity, "Failed to load user data", Toast.LENGTH_LONG).show()
                Log.e("UserProfileActivity", "Failed to load user data: ${e.message}")
            }
        }
    }

    private fun saveUserData() {
        val updatedUser = UserUpdate(
            id = userId,
            firstName = etFirstName.text.toString(),
            lastName = etLastName.text.toString(),
            patronym = etPatronym.text.toString(),
            phoneNumber = etPhoneNumber.text.toString(),
            email = etEmail.text.toString(),
            password = etPassword.text.toString()
        )

        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val response = RetrofitClient.userService.updateUser(updatedUser)

                if (response.isSuccessful) {
                    Toast.makeText(this@UserProfileActivity, "User data updated successfully", Toast.LENGTH_LONG).show()
                } else {
                    Toast.makeText(this@UserProfileActivity, "Failed to update user data", Toast.LENGTH_LONG).show()
                    Log.e("UserProfileActivity", "Failed to update user data: ${response.errorBody()?.string()}")
                }
            } catch (e: Exception) {
                Toast.makeText(this@UserProfileActivity, "Error: ${e.message}", Toast.LENGTH_LONG).show()
                Log.e("UserProfileActivity", "Error: ${e.message}")
            }
        }
    }

}
