package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.CreateTrip
import com.example.cargotruckmobile.Models.LoginResponse
import retrofit2.http.Body
import retrofit2.http.POST

interface TripService {

    @POST("trip/create")
    suspend fun createTrip(@Body createTrip: CreateTrip): retrofit2.Response<LoginResponse>

}