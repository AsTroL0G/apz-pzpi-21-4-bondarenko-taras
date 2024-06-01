package com.example.cargotruckmobile.Models

class CreateTrip (
    val carId: String,
    val driverId: String
)

class TripData (
    val tripId: String,
    val containerName: String,
    val containerType: String,
    val totalWeight: Float,
    val averageSpeed: Double,
    val timeSpent: String
)