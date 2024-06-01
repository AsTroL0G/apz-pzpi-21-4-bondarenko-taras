package com.example.cargotruckmobile.Activity.LogistActivity

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.core.content.ContentProviderCompat.requireContext
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Models.CreateTrip
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class CreateSheduleDialog {

    private lateinit var carIdInput: EditText
    private lateinit var driverIdInput: EditText
    private lateinit var btnSubmitJourney: Button
    private lateinit var journeyInfo: TextView

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.activity_create_trip, container, false)

        carIdInput = view.findViewById(R.id.carIdInput)
        driverIdInput = view.findViewById(R.id.driverIdInput)
        btnSubmitJourney = view.findViewById(R.id.btnSubmitJourney)
        journeyInfo = view.findViewById(R.id.journeyInfo)

        btnSubmitJourney.setOnClickListener {
            val carId = carIdInput.text.toString()
            val driverId = driverIdInput.text.toString()
            if (carId.isNotEmpty() && driverId.isNotEmpty()) {
                createJourney(carId, driverId)
            } else {
                Toast.makeText(requireContext(), "Please enter Car ID and Driver ID", Toast.LENGTH_SHORT).show()
            }
        }

        return view
    }

    private fun createJourney(carId: String, driverId: String) {
        lifecycleScope.launch(Dispatchers.IO) {
            val createShedule = CreateTrip(carId, driverId)
            RetrofitClient.sheduleService.createShedule(createShedule)
        }
    }
}