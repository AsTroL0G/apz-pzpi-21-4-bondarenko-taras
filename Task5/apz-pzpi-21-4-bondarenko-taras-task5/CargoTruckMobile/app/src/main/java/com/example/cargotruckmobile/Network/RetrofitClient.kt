package com.example.cargotruckmobile.Network

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object RetrofitClient {
    private const val BASE_URL = "http://192.168.0.105:7064/"

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    val cargoService: CargoService by lazy {
        retrofit.create(CargoService::class.java)
    }

    val containerService: ContainerService by lazy {
        retrofit.create(ContainerService::class.java)
    }

    val exportService: ExportService by lazy {
        retrofit.create(ExportService::class.java)
    }

    val tripService: TripService by lazy {
        retrofit.create(TripService::class.java)
    }


    val userService: UserService by lazy {
        retrofit.create(UserService::class.java)
    }

    val cityService: CityService by lazy {
        retrofit.create(CityService::class.java)
    }

    val sensorService: SensorService by lazy {
        retrofit.create(SensorService::class.java)
    }

    val valueService: ValueService by lazy {
        retrofit.create(ValueService::class.java)
    }

    val noticeService: NoticeService by lazy {
        retrofit.create(NoticeService::class.java)
    }
    val statisticsService: StatisticsService by lazy {
        retrofit.create(StatisticsService::class.java)
    }

}

