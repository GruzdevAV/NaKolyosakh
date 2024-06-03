package com.example.drivingschoolappandroidclient.models.api

import androidx.lifecycle.MutableLiveData
import com.example.drivingschoolappandroidclient.models.models.LoginResponse
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory


class Controller {
    lateinit var api : ApiModel
    fun start() {
        val gson: Gson = GsonBuilder()
            .setLenient()
            // Это нужно, чтобы прочитать дату окончания срока действия токена аутентификации
            .setDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'")
            .create()
        val client = OkHttpClient().newBuilder()
            .followRedirects(true)
            .followSslRedirects(true)
            .build()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(client)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
        api = retrofit.create(ApiModel::class.java)
    }
    var loginResponse: MutableLiveData<LoginResponse?> = MutableLiveData(null)
    companion object{
//        const val BASE_URL = "https://gruzdevav.somee.com/api/"
        const val BASE_URL = "http://192.168.43.226:45455/api/"
    }
}