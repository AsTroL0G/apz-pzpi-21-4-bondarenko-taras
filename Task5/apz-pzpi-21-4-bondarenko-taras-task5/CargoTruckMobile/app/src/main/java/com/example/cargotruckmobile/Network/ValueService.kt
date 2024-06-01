package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.Value
import retrofit2.http.GET
import retrofit2.http.Path


interface ValueService {


    @GET("value/getBySensorId/{sensorId}")
    suspend fun getBySensorId(@Path("sensorId") numberPhone: String): List<Value>


}
