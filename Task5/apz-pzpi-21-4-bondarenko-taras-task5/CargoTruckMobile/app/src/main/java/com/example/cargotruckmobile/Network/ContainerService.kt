package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.FindContainer
import retrofit2.http.GET
import retrofit2.http.Path

interface ContainerService {

    @GET("container/find/{coordinatesX}/{coordinatesY}")
    suspend fun getContainerInfo(
        @Path("coordinatesX") coordinatesX: String,
        @Path("coordinatesY") coordinatesY: String
    ): FindContainer

}

