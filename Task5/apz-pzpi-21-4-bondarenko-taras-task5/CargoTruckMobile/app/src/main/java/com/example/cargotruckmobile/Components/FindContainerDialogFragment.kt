package com.example.cargotruckmobile.Activity.Components

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.DialogFragment
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class FindContainerDialogFragment : DialogFragment() {

    private lateinit var latitudeInput: EditText
    private lateinit var longitudeInput: EditText
    private lateinit var btnSubmitCoordinates: Button
    private lateinit var containerInfo: TextView

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.activity_find_container_dialog_fragment, container, false)

        latitudeInput = view.findViewById(R.id.latitudeInput)
        longitudeInput = view.findViewById(R.id.longitudeInput)
        btnSubmitCoordinates = view.findViewById(R.id.btn_submit_coordinates)
        containerInfo = view.findViewById(R.id.containerInfo)

        btnSubmitCoordinates.setOnClickListener {
            val latitude = latitudeInput.text.toString()
            val longitude = longitudeInput.text.toString()
            if (latitude.isNotEmpty() && longitude.isNotEmpty()) {
                fetchContainerInfo(latitude, longitude)
            } else {
                Toast.makeText(requireContext(), "Please enter latitude and longitude", Toast.LENGTH_SHORT).show()
            }
        }

        return view
    }

    private fun fetchContainerInfo(latitude: String, longitude: String) {
        lifecycleScope.launch(Dispatchers.IO) {
            try {
                val containerData = RetrofitClient.containerService.getContainerInfo(latitude, longitude)

                withContext(Dispatchers.Main) {
                    if (containerData != null) {
                        containerInfo.text = "Container ID: ${containerData.containerId}\n" +
                                "Coordinates: ${containerData.coordinates}\n" +
                                "Distance: ${containerData.distance}"
                    } else {
                        Toast.makeText(requireContext(), "The container could not be found", Toast.LENGTH_SHORT).show()
                    }
                }
            } catch (e: Exception) {
                withContext(Dispatchers.Main) {
                    Toast.makeText(requireContext(), "Error: ${e.message}", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }

}
