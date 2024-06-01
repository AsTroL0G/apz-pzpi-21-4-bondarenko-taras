package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.Models.CargoCreateDto
import com.example.cargotruckmobile.Models.CargoListResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import retrofit2.http.Query

interface CargoService {

    @GET("cargo/get/sender{id}")
    suspend fun getCargoBySender(@Path("id") id: String): List<Cargo>

    @GET("cargo/get/receiver{id}")
    suspend fun getCargoByReceiver(@Path("id") id: String): List<Cargo>

    @GET("cargo?pageNumber=1&pageSize=100")
    suspend fun getCargoLogist(): CargoListResponse

    @GET("cargo/get/{id}")
    suspend fun getCargo(@Path("id") id: String): Cargo

    @POST("cargo/create")
    suspend fun createCargo(@Body cargo: CargoCreateDto): Response<Void>

    @PUT("cargo/update/notice")
    suspend fun updateNotice(
        @Query("cargoId") cargoId: String,
        @Query("newNoticeId") newNoticeId: String
    ): Response<Void>

    @GET("cargo/get?pageNumber=1&pageSize=100")
    suspend fun getCargoPage(): CargoListResponse


}