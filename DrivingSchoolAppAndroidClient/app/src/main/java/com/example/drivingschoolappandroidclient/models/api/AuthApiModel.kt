package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.LoginModel
import com.example.drivingschoolappandroidclient.models.models.LoginResponse
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST

interface AuthApiModel {
    @POST("Login")
    fun login(@Body model: LoginModel) : Call<MyResponse<LoginResponse?>>
    @POST("Ping")
    fun ping() : Call<MyResponse<Any?>>
}