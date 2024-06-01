package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.City
import retrofit2.http.GET
import retrofit2.http.Path

interface CityService {

    @GET("city/get/name/{cityName}")
    suspend fun getCityByName(@Path("cityName") id: String): City

}