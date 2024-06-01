package com.example.cargotruckmobile.Models

class Container(
    val containerId: String,
    val containerName: String,
    val containerType: String,
    val numberTrips: Int,
    val averageLoadCapacity: Double,
    val volumetricWeight: Double
)
class FindContainer(
    val containerId: String,
    val coordinates: String,
    val distance: String
)