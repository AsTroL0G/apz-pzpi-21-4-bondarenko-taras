package com.example.cargotruckmobile.Activity

import android.content.Intent
import android.os.Bundle
import android.widget.LinearLayout
import androidx.appcompat.app.AppCompatActivity
import com.example.cargotruckmobile.Activity.UserCargoActivity.CreateCargoActivity
import com.example.cargotruckmobile.Activity.UserCargoActivity.UserCargoReceiver
import com.example.cargotruckmobile.Activity.UserCargoActivity.UserCargos
import com.example.cargotruckmobile.R

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val userCargo: LinearLayout = findViewById(R.id.myCargo)
        val userCabinet: LinearLayout = findViewById(R.id.userCabinet)
        val userHelp: LinearLayout = findViewById(R.id.userHelp)
        val createDelivery: LinearLayout = findViewById(R.id.createDelivery)
        val userId = intent.getStringExtra("userId").toString()


        userCargo.setOnClickListener {

            val intent = Intent(this, UserCargos::class.java)
            intent.putExtra("userId", userId)
            startActivity(intent)
        }

        userCabinet.setOnClickListener {

            val intent = Intent(this, UserProfileActivity::class.java)
            intent.putExtra("userId", userId)
            startActivity(intent)
        }

        userHelp.setOnClickListener {

            val intent = Intent(this, UserCargoReceiver::class.java)
            intent.putExtra("userId", userId)
            startActivity(intent)
        }

        createDelivery.setOnClickListener {

            val intent = Intent(this, CreateCargoActivity::class.java)
            intent.putExtra("userId", userId)
            startActivity(intent)
        }
    }
}