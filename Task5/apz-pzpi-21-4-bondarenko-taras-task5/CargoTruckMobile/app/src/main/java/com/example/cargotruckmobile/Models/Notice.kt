package com.example.cargotruckmobile.Models

data class Notice (
    val id: String,
    val containerId: String,
    val tripId: String
)

data class CreateNotice (
    val containerId: String,
    val tripId: String
)