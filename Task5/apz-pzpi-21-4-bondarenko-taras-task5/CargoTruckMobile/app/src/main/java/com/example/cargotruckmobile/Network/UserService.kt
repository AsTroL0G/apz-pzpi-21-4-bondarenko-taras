package com.example.cargotruckmobile.Network

import com.example.cargotruckmobile.Models.Credentials
import com.example.cargotruckmobile.Models.LoginResponse
import com.example.cargotruckmobile.Models.User
import com.example.cargotruckmobile.Models.UserCreate
import com.example.cargotruckmobile.Models.UserDto
import com.example.cargotruckmobile.Models.UserListResponse
import com.example.cargotruckmobile.Models.UserUpdate
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

interface UserService {
    @POST("users/register")
    suspend fun registerUser(@Body user: UserCreate): retrofit2.Response<Void>

    @POST("users/login")
    suspend fun loginUser(@Body credentials: Credentials): retrofit2.Response<LoginResponse>


    @GET("users/{id}")
    suspend fun getUser(@Path("id") id: String): User

    @GET("users/Number/{numberPhone}")
    suspend fun getUserByNumberPhone(@Path("numberPhone") numberPhone: String): UserDto

    @PUT("users")
    suspend fun updateUser(@Body user: UserUpdate): retrofit2.Response<Void>

    @PUT("users/ban/{id}")
    suspend fun banUser(@Path("id") id: String): retrofit2.Response<Void>

    @PUT("users/unban/{id}")
    suspend fun unBanUser(@Path("id") id: String): retrofit2.Response<Void>

    @GET("users?pageNumber=1&pageSize=100")
    suspend fun getUser(): UserListResponse
}
