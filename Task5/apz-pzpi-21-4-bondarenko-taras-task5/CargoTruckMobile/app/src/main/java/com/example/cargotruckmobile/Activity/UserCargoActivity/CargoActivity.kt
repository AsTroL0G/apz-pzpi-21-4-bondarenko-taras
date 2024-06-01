package com.example.cargotruckmobile.Activity.UserCargoActivity

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.Models.User
import com.example.cargotruckmobile.Network.RetrofitClient
import com.google.android.gms.maps.model.LatLng
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import android.widget.TextView
import android.widget.Toast
import com.example.cargotruckmobile.Activity.Components.MapActivity
import com.example.cargotruckmobile.R

class CargoActivity : AppCompatActivity() {

    private lateinit var parcelNumber: TextView
    private lateinit var senderCity: TextView
    private lateinit var senderAddress: TextView
    private lateinit var senderPhone: TextView
    private lateinit var senderName: TextView
    private lateinit var receiverCity: TextView
    private lateinit var receiverAddress: TextView
    private lateinit var receiverPhone: TextView
    private lateinit var receiverName: TextView
    private lateinit var packageWeight: TextView
    private lateinit var packageDescription: TextView
    private lateinit var deliveryCost: TextView
    private lateinit var dimensions: TextView

    private lateinit var cargo: Cargo
    private lateinit var sender: User
    private lateinit var receiver: User

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_cargo)

        // Ініціалізація елементів
        parcelNumber = findViewById(R.id.parcel_number)
        senderCity = findViewById(R.id.sender_city)
        senderAddress = findViewById(R.id.sender_address)
        senderPhone = findViewById(R.id.sender_phone)
        senderName = findViewById(R.id.sender_name)
        receiverCity = findViewById(R.id.receiver_city)
        receiverAddress = findViewById(R.id.receiver_address)
        receiverPhone = findViewById(R.id.receiver_phone)
        receiverName = findViewById(R.id.receiver_name)
        packageWeight = findViewById(R.id.package_weight)
        packageDescription = findViewById(R.id.package_description)
        deliveryCost = findViewById(R.id.delivery_cost)
        dimensions = findViewById(R.id.dimensions)

        val cargoId = intent.getStringExtra("cargoId").toString()

        // Завантаження даних та оновлення інтерфейсу
        lifecycleScope.launch(Dispatchers.Main) {
            fetchCargoData(cargoId)
            fetchUserData(cargo)
        }

        findViewById<Button>(R.id.btn_show_map).setOnClickListener {
            lifecycleScope.launch(Dispatchers.Main) {
                if(cargo.status=="Очікує на прийняття"){
                    Toast.makeText(this@CargoActivity, "The cargo was not accepted by the carrier", Toast.LENGTH_LONG).show()
                }else{
                    val coordinates = fetchCoordinates(cargo.noticeId.toString())
                    val intent = Intent(this@CargoActivity, MapActivity::class.java).apply {
                        putExtra("latitude", coordinates.latitude)
                        putExtra("longitude", coordinates.longitude)
                    }
                    startActivity(intent)
                }

            }
        }
    }

    private suspend fun fetchCargoData(id: String) {
        try {
            cargo = withContext(Dispatchers.IO) {
                RetrofitClient.cargoService.getCargo(id)
            }
            Log.d("Cargo", "Cargo data fetched successfully")
        } catch (e: Exception) {
            Log.e("Error", "Failed to fetch cargo: ${e.message}")
        }
    }

    private fun fetchUserData(cargo: Cargo) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                sender = withContext(Dispatchers.IO) {
                    RetrofitClient.userService.getUser(cargo.senderId)
                }
                receiver = withContext(Dispatchers.IO) {
                    RetrofitClient.userService.getUser(cargo.receiverId)
                }
                updateUI(cargo, sender, receiver)
                Log.d("Cargo", "Data fetched successfully")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch user: ${e.message}")
            }
        }
    }

    private suspend fun fetchCoordinates(noticeId: String): LatLng {
        var latitude: Double = 0.0
        var longitude: Double = 0.0

        try {
            val notice = withContext(Dispatchers.IO) {
                RetrofitClient.noticeService.getNotice(noticeId)
            }
            val sensorsGPS = withContext(Dispatchers.IO) {
                RetrofitClient.sensorService.getSensorByContainer(notice.containerId)
            }
            val valueList = withContext(Dispatchers.IO) {
                RetrofitClient.valueService.getBySensorId(sensorsGPS.sensorId)
            }
            val value = valueList.last()
            val coordinates = value.values.split("/")
            latitude = coordinates[0].toDouble()
            longitude = coordinates[1].toDouble()
            Log.d("Cargo", "Data fetched successfully")
        } catch (e: Exception) {
            Log.e("Error", "Failed to fetch coordinates: ${e.message}")
        }

        return LatLng(latitude, longitude)
    }

    private fun updateUI(cargo: Cargo, sender: User, receiver: User) {
        parcelNumber.text = cargo.id
        packageDescription.text = "Description of the package: ${cargo.name}"
        packageWeight.text = "Weight: ${cargo.weight} kg"
        dimensions.text = "Dimensions: ${cargo.length}×${cargo.height}×${cargo.width}"
        deliveryCost.text = "Delivery cost: ${cargo.shippingPrice} UAH"
        senderCity.text = "Sender City: ${cargo.citySenderId}"
        senderAddress.text = "Address: ${cargo.addressSenderId}"
        senderPhone.text = "Phone: ${sender.phoneNumber}"
        senderName.text = "First Name: ${sender.patronym} ${sender.lastName} ${sender.firstName}"

        receiverCity.text = "Receiver City: ${cargo.cityReceiverId}"
        receiverAddress.text = "Address: ${cargo.addressReceiverId}"
        receiverPhone.text = "Phone: ${receiver.phoneNumber}"
        receiverName.text = "First name: ${receiver.patronym} ${receiver.lastName} ${receiver.firstName}"
    }
}
