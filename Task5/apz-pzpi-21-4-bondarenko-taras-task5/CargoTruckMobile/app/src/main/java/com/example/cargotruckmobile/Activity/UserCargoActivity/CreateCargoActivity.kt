package com.example.cargotruckmobile.Activity.UserCargoActivity

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Activity.MainActivity
import com.example.cargotruckmobile.Models.CargoCreateDto
import com.example.cargotruckmobile.Models.UserCreate
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch

class CreateCargoActivity : AppCompatActivity() {

    private lateinit var btnEnterSenderInfo: Button
    private lateinit var btnSave: Button

    private lateinit var etCitySenderId: EditText
    private lateinit var etCityReceiverId: EditText
    private var receiver: UserCreate = UserCreate(firstName = "", lastName = "", patronym = "", phoneNumber = "", email = "", password = "")
    private var idUser  = ""
    private var idCitySender  = ""
    private var idCityReceiver = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_cargo_create)
        btnEnterSenderInfo = findViewById(R.id.btn_enter_receiver_info)
        etCitySenderId = findViewById(R.id.et_city_sender_id)
        etCityReceiverId = findViewById(R.id.et_city_receiver_id)

        btnSave = findViewById(R.id.btn_save)

        btnEnterSenderInfo.setOnClickListener {
            showSenderInfoDialog()
        }

        btnSave.setOnClickListener {
            createCargo()
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)

        }

        etCitySenderId.setOnFocusChangeListener { _, hasFocus ->
            if (!hasFocus) {
                val name = etCitySenderId.text.toString()
                if (name.isNotEmpty()) {
                    fetchCityByName(name)
                }
            }
        }
        etCityReceiverId.setOnFocusChangeListener { _, hasFocus ->
            if (!hasFocus) {
                val name = etCityReceiverId.text.toString()
                if (name.isNotEmpty()) {
                    fetchCityByName(name)
                }
            }
        }
    }
    private fun createCargo() {
        Log.d("CreateCargoActivity", "createCargo() called")

        // Inflate main view elements
        val etName = findViewById<EditText>(R.id.et_name)
        val etWeight = findViewById<EditText>(R.id.et_weight)
        val etLength = findViewById<EditText>(R.id.et_length)
        val etHeight = findViewById<EditText>(R.id.et_height)
        val etWidth = findViewById<EditText>(R.id.et_width)
        val etAnnouncedPrice = findViewById<EditText>(R.id.et_announced_price)
        val etCitySenderId = findViewById<EditText>(R.id.et_city_sender_id)
        val etAddressSenderId = findViewById<EditText>(R.id.et_address_sender_id)
        val etCityReceiverId = findViewById<EditText>(R.id.et_city_receiver_id)
        val etAddressReceiverId = findViewById<EditText>(R.id.et_address_receiver_id)
        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)
        val senderId = sharedPreferences.getString("userId", null)
        Log.d("CreateCargoActivity", "View elements inflated")

        val cargo = CargoCreateDto(
            name = etName.text.toString(),
            weight = etWeight.text.toString().toFloat(),
            length = etLength.text.toString().toFloat(),
            height = etHeight.text.toString().toFloat(),
            width = etWidth.text.toString().toFloat(),
            announcedPrice = etAnnouncedPrice.text.toString().toFloat(),
            senderId = senderId.toString(),
            receiverId = idUser,
            citySenderId = idCitySender,
            addressSenderId = etAddressSenderId.text.toString(),
            cityReceiverId = idCityReceiver,
            addressReceiverId = etAddressReceiverId.text.toString()
        )

        Log.d("CreateCargoActivity", "CargoCreateDto created: $cargo")

        lifecycleScope.launch(Dispatchers.Main) {
            Log.d("CreateCargoActivity", "Inside lifecycleScope.launch")
            try {
                val response = RetrofitClient.cargoService.createCargo(cargo)
                if (response.isSuccessful) {
                    Toast.makeText(this@CreateCargoActivity, "Cargo created successfully", Toast.LENGTH_LONG).show()
                } else {
                    Toast.makeText(this@CreateCargoActivity, "Failed to create cargo", Toast.LENGTH_LONG).show()
                }
            } catch (e: Exception) {
                Toast.makeText(this@CreateCargoActivity, "Error: ${e.message}", Toast.LENGTH_LONG).show()
            }
        }
    }



    private fun showSenderInfoDialog() {
        val dialogView = LayoutInflater.from(this).inflate(R.layout.dialog_sender_info, null)
        val etSenderPhone = dialogView.findViewById<EditText>(R.id.et_sender_phone)
        val etSenderLastName = dialogView.findViewById<EditText>(R.id.et_sender_last_name)
        val etSenderFirstName = dialogView.findViewById<EditText>(R.id.et_sender_first_name)

        etSenderPhone.setOnFocusChangeListener { _, hasFocus ->
            if (!hasFocus) {
                val phoneNumber = etSenderPhone.text.toString()
                if (phoneNumber.isNotEmpty()) {
                    fetchUserInfoByPhone(phoneNumber, etSenderLastName, etSenderFirstName)
                }
            }
        }

        val dialog = AlertDialog.Builder(this)
            .setTitle("Enter Sender Info")
            .setView(dialogView)
            .setPositiveButton("OK") { _, _ ->
                val senderPhone = etSenderPhone.text.toString()
                val senderLastName = etSenderLastName.text.toString()
                val senderFirstName = etSenderFirstName.text.toString()
                if(idUser=="")
                {
                    receiver = UserCreate(firstName = senderFirstName, lastName = senderLastName, patronym = "", phoneNumber = senderPhone, email = "guest@gmail.com", password = "")
                    registerUser(receiver)
                    fetchUserInfoByPhone(receiver.phoneNumber, etSenderLastName, etSenderFirstName)
                }
            }
            .setNegativeButton("Cancel", null)
            .create()
        dialog.show()
    }
    private fun fetchUserInfoByPhone(phoneNumber: String, lastNameView: EditText, firstNameView: EditText) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val user = RetrofitClient.userService.getUserByNumberPhone(phoneNumber)
                lastNameView.setText(user.lastName)
                firstNameView.setText(user.firstName)
                if(user.lastName!=""){
                    idUser = user.id
                }
                Log.d("User", "User data fetched successfully")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch user data: ${e.message}")
            }
        }
    }

    private fun fetchCityByName(cityName: String) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val city = RetrofitClient.cityService.getCityByName(cityName)
                if(city== null){
                    Toast.makeText(this@CreateCargoActivity, "City not found", Toast.LENGTH_LONG).show()
                }else{
                    if(idCitySender!=""){
                        idCityReceiver = city.id
                        Toast.makeText(this@CreateCargoActivity, "Recipient city found", Toast.LENGTH_LONG).show()
                    }else{
                        idCitySender = city.id
                        Toast.makeText(this@CreateCargoActivity, "Shipping city found", Toast.LENGTH_LONG).show()
                    }
                }
                Log.d("User", "User data fetched successfully")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch user data: ${e.message}")
            }
        }
    }

    private fun registerUser(user: UserCreate) {

        GlobalScope.launch(Dispatchers.Main) {
            try {
                val response = RetrofitClient.userService.registerUser(user)
                if (response.isSuccessful) {
                    Log.d("User", "User data fetched successfully")
                }
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch user data: ${e.message}")
            }
        }
    }
}