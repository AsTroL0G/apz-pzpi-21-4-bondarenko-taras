package com.example.cargotruckmobile.Activity.LogistActivity

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity
import com.example.cargotruckmobile.Activity.Components.FindContainerDialogFragment
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Activity.Statistics.ContainerStatisticsActivity
import com.example.cargotruckmobile.Activity.Statistics.TripStatisticsActivity

class LogistActivity : AppCompatActivity() {

    private lateinit var btnViewContainerStatistics: Button
    private lateinit var btnViewTripStatistics: Button
    private lateinit var btnAcceptCargo: Button
    private lateinit var btnFindContainer: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_logist)

        // Ініціалізація кнопок
        btnViewContainerStatistics = findViewById(R.id.btn_view_container_statistics)
        btnViewTripStatistics = findViewById(R.id.btn_view_trip_statistics)
        btnAcceptCargo = findViewById(R.id.btn_accept_cargo)
        btnFindContainer = findViewById(R.id.btn_find_container)

        // Встановлення слухачів натискань для кнопок
        btnViewContainerStatistics.setOnClickListener {
            val intent = Intent(this, ContainerStatisticsActivity::class.java)
            startActivity(intent)
        }

        btnViewTripStatistics.setOnClickListener {
            val intent = Intent(this, TripStatisticsActivity::class.java)
            startActivity(intent)
        }

        btnFindContainer.setOnClickListener {
            val dialog = FindContainerDialogFragment()
            dialog.show(supportFragmentManager, "FindContainerDialog")
        }

        btnAcceptCargo.setOnClickListener {
            val intent = Intent(this, LogistCargoActivity::class.java)
            startActivity(intent)
        }
    }
}
