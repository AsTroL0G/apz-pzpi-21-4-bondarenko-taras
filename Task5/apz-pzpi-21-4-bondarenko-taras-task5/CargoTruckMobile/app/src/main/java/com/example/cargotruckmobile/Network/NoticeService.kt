package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.LoginResponse
import com.example.cargotruckmobile.Models.Notice
import com.example.cargotruckmobile.Models.CreateNotice
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path


interface NoticeService {

    @GET("notice/get/{noticeId}")
    suspend fun getNotice(@Path("noticeId") noticeId: String): Notice

    @POST("notice/create")
    suspend fun updateNotice(@Body createNotice: CreateNotice): retrofit2.Response<LoginResponse>

}
