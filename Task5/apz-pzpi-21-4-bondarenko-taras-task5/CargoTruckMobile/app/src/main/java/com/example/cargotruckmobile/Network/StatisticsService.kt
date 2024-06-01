package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.Container
import com.example.cargotruckmobile.Models.TripData
import retrofit2.http.GET
import retrofit2.http.Path

interface StatisticsService {

    @GET("statistics/delivery/container/{containerId}")
    suspend fun getContainerStatistics(@Path("containerId") containerId: String): Container

    @GET("statistics/trip/{tripId}")
    suspend fun getTripStatistics(@Path("tripId") tripId: String): TripData
}
