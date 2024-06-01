package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.SensorsDto
import retrofit2.http.GET
import retrofit2.http.Path

interface SensorService {

    @GET("sensors/get/GPS/container/{containerId}")
    suspend fun getSensorByContainer(@Path("containerId") numberPhone: String): SensorsDto

}
