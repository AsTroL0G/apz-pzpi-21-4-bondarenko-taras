package com.example.cargotruckmobile.Network

import okhttp3.ResponseBody
import retrofit2.http.GET
import retrofit2.http.Path

interface ExportService {

    @GET("export/exportCVS/{backupId}")
    suspend fun downloadBackup(@Path("backupId") backupId: String): ResponseBody
}