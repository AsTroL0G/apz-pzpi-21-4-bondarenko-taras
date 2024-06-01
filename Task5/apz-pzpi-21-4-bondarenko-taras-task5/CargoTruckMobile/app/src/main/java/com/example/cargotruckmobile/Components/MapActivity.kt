package com.example.cargotruckmobile.Activity.Components

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.cargotruckmobile.R
import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.MarkerOptions

class MapActivity : AppCompatActivity(), OnMapReadyCallback {

    private lateinit var map: GoogleMap

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_map)

        // Ініціалізація фрагмента карти та налаштування колбека
        val mapFragment = supportFragmentManager.findFragmentById(R.id.map) as SupportMapFragment
        mapFragment.getMapAsync(this)
    }

    override fun onMapReady(googleMap: GoogleMap) {
        map = googleMap

        // Отримання координат посилки з інтенції
        val latitude = intent.getDoubleExtra("latitude", 0.0)
        val longitude = intent.getDoubleExtra("longitude", 0.0)

        // Додавання маркера на місці посилки та переміщення камери
        val parcelLocation = LatLng(latitude, longitude)
        map.addMarker(MarkerOptions().position(parcelLocation).title("Location of the parcel"))
        map.moveCamera(CameraUpdateFactory.newLatLngZoom(parcelLocation, 9f))
    }
}
